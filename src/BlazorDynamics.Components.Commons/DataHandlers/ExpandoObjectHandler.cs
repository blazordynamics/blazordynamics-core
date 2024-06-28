using System.Dynamic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BlazorDynamics.Forms.Commons.DataHandlers
{
    public class ExpandoObjectHandler : IValueHandler
    {
        public void AddValue(string path, object obj, object defaultValue)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            if (!(obj is IDictionary<string, object> expando))
                throw new ArgumentException("Object must be of type ExpandoObject", nameof(obj));

            var defaultExpando = ConvertToExpando(defaultValue);

            path = path.TrimStart('$').TrimStart('.');
            var segments = path.Split('.');

            AddValueForSegemnts(path, expando, defaultExpando, segments);
        }

        private static void AddValueForSegemnts(string path, IDictionary<string, object> expando, ExpandoObject defaultExpando, string[] segments)
        {
            for (int i = 0; i < segments.Length; i++)
            {
                expando = AddValuePerSegment(path, expando, defaultExpando, segments, i);
            }
        }

        private static IDictionary<string, object> AddValuePerSegment(string path, IDictionary<string, object> expando, ExpandoObject defaultExpando, string[] segments, int i)
        {
            var segment = segments[i];

            if (i == segments.Length - 1)
            {
                AddValueForLastSegment(path, expando, defaultExpando, segment);
            }

            else
            {
                GetNextSegment(ref expando, segment);
            }

            return expando;
        }

        private static void AddValueForLastSegment(string path, IDictionary<string, object> expando, ExpandoObject defaultExpando, string segment)
        {
            if (expando.TryGetValue(segment, out var currentValue))
            {
                // Handle List<object>
                if (currentValue is List<object> currentList)
                {
                    AddValueToList(defaultExpando, currentList);
                }
                // Handle Array
                else if (currentValue.GetType().IsArray)
                {
                    AddValueToArray(expando, defaultExpando, segment, currentValue);
                }
                else
                {
                    // Not a list or array
                    throw new InvalidOperationException($"Expected a list or array at path '{path}', but found a different type.");
                }
            }
            else
            {
                AddValueToNewList(expando, defaultExpando, segment);
            }
        }

        private static void AddValueToNewList(IDictionary<string, object> expando, ExpandoObject defaultExpando, string segment)
        {
            // Segment not found, create new list
            var newList = new List<object> { defaultExpando };
            expando[segment] = newList;
        }

        private static void AddValueToList(ExpandoObject defaultExpando, List<object> currentList)
        {
            currentList.Add(defaultExpando);
        }

        private static void AddValueToArray(IDictionary<string, object> expando, ExpandoObject defaultExpando, string segment, object currentValue)
        {
            var array = (Array)currentValue;
            var elementType = array.GetType().GetElementType();
            var newArray = Array.CreateInstance(elementType, array.Length + 1);
            Array.Copy(array, newArray, array.Length);
            newArray.SetValue(defaultExpando, array.Length);
            expando[segment] = newArray; // Replace old array with new array
        }

        private static object GetNextSegment(ref IDictionary<string, object> expando, string segment)
        {
            object obj;
            // Check if segment has an array index notation, e.g., "cars[0]"
            var isArrayIndex = segment.EndsWith("]") && segment.Contains("[");

            if (isArrayIndex)
            {
                var arrayIndex = -1;
                // Extract array name and index from segment
                var arrayName = segment.Substring(0, segment.IndexOf('['));
                var indexStr = segment.Substring(segment.IndexOf('[') + 1, segment.Length - arrayName.Length - 2);
                if (!int.TryParse(indexStr, out arrayIndex))
                {
                    throw new ArgumentException("Invalid array index in path.", nameof(segment));
                }
                obj = ProcessSegmentWithArrayIndex(ref expando, arrayIndex, arrayName);
            }
            else
            {
                // Process regular segment
                if (!expando.TryGetValue(segment, out obj) || !(obj is IDictionary<string, object> nextExpando))
                {
                    nextExpando = new ExpandoObject();
                    expando[segment] = nextExpando;
                }
                expando = nextExpando;
            }

            return obj;
        }

        private static object ProcessSegmentWithArrayIndex(ref IDictionary<string, object> expando, int arrayIndex, string arrayName)
        {
            object obj;

            if (!expando.TryGetValue(arrayName, out obj) || !(obj is IList<object> currentList))
            {
                currentList = new List<object>();
                expando[arrayName] = currentList;
            }

            while (currentList.Count <= arrayIndex)
            {
                currentList.Add(new ExpandoObject());
            }

            obj = currentList[arrayIndex];
            if (!(obj is IDictionary<string, object> nextExpando))
            {
                nextExpando = new ExpandoObject();
                currentList[arrayIndex] = nextExpando;
            }
            expando = nextExpando;
            return obj;
        }

        public ExpandoObject ConvertToExpando(object obj)
        {
            if (obj is ExpandoObject expando)
            {
                // The object is already an ExpandoObject
                return expando;
            }
            else
            {
                // Convert to ExpandoObject
                var expandoObject = new ExpandoObject();
                var dictionary = (IDictionary<string, object>)expandoObject;
                if (obj != null)
                {
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        if (prop.CanRead)
                        {
                            dictionary[prop.Name] = prop.GetValue(obj);
                        }
                    }
                }
                return expandoObject;
            }
        }

        public int GetCounterValue(string path, object obj)
        {
            var pathValue = GetValue(path, obj);
            if (pathValue == null) { return 0; }
            // Check if the value is a collection and return its count
            if (pathValue is List<object> currentList)
            {
                return currentList.Count();
            }
            // Handle Array
            else if (pathValue.GetType().IsArray)
            {
                return ((Array)pathValue).Length;
            }
            return 0;
        }

        public object GetValue(string path, object obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            if (!(obj is IDictionary<string, object> expando))
                throw new ArgumentException("Object must be of type ExpandoObject", nameof(obj));

            path = path.TrimStart('$');
            path = path.TrimStart('.');

            var segments = path.Split('.');

            foreach (var originalSegment in segments)
            {
                // Use a separate variable for the potentially modified segment
                var segment = originalSegment;

                // Check if the segment has an array indexer
                var arrayIndex = -1; // Default to -1 to indicate no array index
                var arrayPattern = GetRegexPatternForCollection();
                var match = arrayPattern.Match(segment);
                if (match.Success)
                {
                    segment = match.Groups[1].Value; // The name part of the segment
                    arrayIndex = int.Parse(match.Groups[2].Value); // The index part of the segment
                }

                if (!expando.TryGetValue(segment, out obj))
                {
                    obj = null;
                    break;
                }

                // If the segment has an array indexer, attempt to get the specified element
                if (arrayIndex >= 0)
                {
                    if (obj is IList<object> list && arrayIndex < list.Count)
                    {
                        obj = list[arrayIndex];
                    }
                    else
                    {
                        obj = null;
                        break;
                    }
                }

                // If the value is another ExpandoObject, update our expando reference
                if (obj is IDictionary<string, object> nextExpando)
                {
                    expando = nextExpando;
                }
            }

            return obj;
        }



        public void RemoveValue(string path, object value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            if (!(value is IDictionary<string, object> expando))
                throw new ArgumentException("Object must be of type ExpandoObject", nameof(value));

            path = path.TrimStart('$').TrimStart('.');
            var segments = path.Split('.');
            removeValueWithSegments(path, expando, segments);
        }

        private static void removeValueWithSegments(string path, IDictionary<string, object> expando, string[] segments)
        {
            for (int i = 0; i < segments.Length; i++)
            {
                var segment = segments[i];
                var arrayPattern = GetRegexPatternForCollection();
                var match = arrayPattern.Match(segment);

                if (i == segments.Length - 1)
                {
                    if (match.Success)
                    {
                        RemoveCollection(path, expando, match);
                    }
                    else
                    {
                        RemoveKeyForNonCollectionSegment(expando, segment);
                    }
                }
                else
                {
                    GetNextSegment(ref expando, segment);
                }
            }
        }

        private static void RemoveKeyForNonCollectionSegment(IDictionary<string, object> expando, string segment)
        {
            // Remove the key for non-list/array segment
            if (expando.ContainsKey(segment))
            {
                expando.Remove(segment);
            }
            else
            {
                throw new KeyNotFoundException($"Key '{segment}' not found in ExpandoObject.");
            }
        }

        private static void RemoveCollection(string path, IDictionary<string, object> expando, Match match)
        {
            // Handle array/list removal
            string segment = match.Groups[1].Value;
            int arrayIndex = int.Parse(match.Groups[2].Value);

            if (expando.TryGetValue(segment, out var listObj) && listObj is IList<object> list)
            {
                if (arrayIndex >= 0 && arrayIndex < list.Count)
                {
                    list.RemoveAt(arrayIndex);
                }
                else
                {
                    throw new IndexOutOfRangeException($"Index {arrayIndex} is out of range for list at '{path}'.");
                }
            }
            else
            {
                throw new KeyNotFoundException($"List key '{segment}' not found in ExpandoObject.");
            }
        }

        public void SetValue(string path, object obj, object value)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));

            if (!(obj is IDictionary<string, object> expando))
                throw new ArgumentException("Object must be of type ExpandoObject", nameof(obj));

            path = path.TrimStart('$');
            path = path.TrimStart('.');

            var segments = path.Split('.');
            SetValueForSegmentCollection(ref obj, value, ref expando, segments);
        }

        private static void SetValueForSegmentCollection(ref object obj, object value, ref IDictionary<string, object> expando, string[] segments)
        {
            for (int i = 0; i < segments.Length; i++)
            {
                var segment = segments[i];

                // Check for array indexers
                var arrayIndex = -1;
                Regex arrayPattern = GetRegexPatternForCollection();
                var match = arrayPattern.Match(segment);
                if (match.Success)
                {
                    segment = match.Groups[1].Value;
                    arrayIndex = int.Parse(match.Groups[2].Value);
                }

                if (i == segments.Length - 1)
                {
                    SetValueForArraySegement(value, expando, segment, arrayIndex);
                }
                else
                {
                    obj = getNextObjectForSegment(ref expando, segment, arrayIndex);
                }
            }
        }

        private static object getNextObjectForSegment(ref IDictionary<string, object> expando, string segment, int arrayIndex)
        {
            object obj;
            if (!expando.TryGetValue(segment, out obj))
            {
                var nextExpando = new ExpandoObject();
                expando[segment] = nextExpando;
                obj = nextExpando;
            }

            if (arrayIndex >= 0)
            {
                // Navigate into the array
                if (obj is IList<object> list && arrayIndex < list.Count)
                {
                    obj = list[arrayIndex];
                }
                else
                {
                    obj = AddSegmentWhenNotExists(expando, segment, arrayIndex, obj);
                }
            }

            if (!(obj is IDictionary<string, object> nextExpandoDict))
            {
                nextExpandoDict = new ExpandoObject();
                expando[segment] = nextExpandoDict;
            }
            expando = nextExpandoDict;
            return obj;
        }

        private static object AddSegmentWhenNotExists(IDictionary<string, object> expando, string segment, int arrayIndex, object obj)
        {
            // Handle case where the array or index doesn't exist
            var nextExpando = new ExpandoObject();
            if (obj is IList<object> existingList && arrayIndex == existingList.Count)
            {
                // Append new expando to the list
                existingList.Add(nextExpando);
            }
            else
            {
                // Replace or create a new list with the expando
                var newList = new List<object> { nextExpando };
                expando[segment] = newList;
            }
            obj = nextExpando;
            return obj;
        }

        private static void SetValueForArraySegement(object value, IDictionary<string, object> expando, string segment, int arrayIndex)
        {
            // Handle setting value for an array element
            if (arrayIndex >= 0 && expando[segment] is IList<object> list && arrayIndex < list.Count)
            {
                list[arrayIndex] = value;
            }
            else
            {
                expando[segment] = value;
            }
        }

        private static Regex GetRegexPatternForCollection()
        {
            return new Regex(@"(\w+)\[(\d+)\]", RegexOptions.None, TimeSpan.FromMilliseconds(100));
        }
    }
}
