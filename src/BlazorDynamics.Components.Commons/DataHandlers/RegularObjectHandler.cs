using Microsoft.AspNetCore.Components;
using System.Collections;
using System.Reflection;

namespace BlazorDynamics.Forms.Commons.DataHandlers
{
    public class RegularObjectHandler : IValueHandler
    {
        public object GetValue(string path, object obj)
        {
            ValidateInput(path, obj);

            var segments = NormalizePath(path).Split('.');

            foreach (var segment in segments)
            {
                if (IsIndexedProperty(segment))
                {
                    obj = GetIndexedPropertyValue(obj, segment);
                }
                else
                {
                    obj = GetPropertyValue(obj, segment);
                }

                if (obj == null) return null;
            }

            return obj;
        }

        public static Dictionary<string, object> GetParameters(Type componentType, Dictionary<string, object> parameters, List<KeyValuePair<string, object>> completeListOfParameters)
        {
            var validParameters = new Dictionary<string, object>();
            IEnumerable<string> componentProperties = GetPropertiesWithParameterAnnotation(componentType);

            completeListOfParameters.ForEach(p =>
            {
                if (componentProperties.Contains(p.Key))
                {
                    validParameters.Add(p.Key, p.Value);
                }
            }
            );


            return validParameters;
        }
        public void SetValue(string path, object obj, object value)
        {
            ValidateInput(path, obj);

            var segments = NormalizePath(path).Split('.');

            TraverseAndSet(segments, obj, value, path);
        }

        public void RemoveValue(string path, object obj)
        {
            ValidateInput(path, obj);

            var segments = NormalizePath(path).Split('.');

            TraverseAndRemove(segments, obj, path);
        }

        public int GetCounterValue(string path, object valueObject)
        {
            var obj = GetValue(path, valueObject);

            // Check if obj is null
            if (obj == null)
            {
                return 0;
            }

            // Check if the object is a collection
            if (obj is IEnumerable && !(obj is string))
            {
                int count = 0;

                // Count the number of items in the collection
                foreach (var item in (IEnumerable)obj)
                {
                    count++;
                }

                return count;
            }

            // If obj is not a collection, return 0 or some other default value
            return 0;

        }
        public static List<string> GetPropertiesWithParameterAnnotation(Type componentType)
        {
            return componentType.GetProperties()
                .Where(p => p.GetCustomAttribute<ParameterAttribute>() != null)
                .Select(p => p.Name)
                .ToList();
        }

        private static void ValidateInput(string path, object obj)
        {
            if (obj == null) { return; }
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException(nameof(path));
        }

        private static string NormalizePath(string path)
        {
            return path.TrimStart('$', '.');
        }

        private static bool IsIndexedProperty(string segment)
        {
            return segment.EndsWith("]") && segment.Contains("[");
        }

        private static object GetIndexedPropertyValue(object obj, string segment)
        {
            var (propertyName, index) = ExtractPropertyAndIndex(segment);

            var listObj = GetPropertyValue(obj, propertyName) as IList;

            ValidateListAndIndex(listObj, propertyName, index, segment);
            if (index > listObj.Count - 1 || index < 0)
            {
                return null; //TODO, THE RENDERING IS FAULTY IT WILL RUN MULTIPLE TIMES ALSO WITH OLDER DATA
            }
            return listObj[index];
        }

        private static (string PropertyName, int Index) ExtractPropertyAndIndex(string segment)
        {
            var indexStartPos = segment.IndexOf('[');
            var propertyName = segment.Substring(0, indexStartPos);
            var indexPart = segment.Substring(indexStartPos + 1, segment.Length - indexStartPos - 2);

            if (!int.TryParse(indexPart, out int index))
            {
                throw new Exception($"Invalid index format in segment {segment}");
            }

            return (propertyName, index);
        }

        private static void ValidateListAndIndex(IList list, string propertyName, int index, string segment)
        {
            if (list == null || index >= list.Count)
            {
                //TODO, throw new Exception($"Property {propertyName} is not a list or index is out of range in segment {segment}.");
            }
        }

        private static object GetPropertyValue(object obj, string propertyName)
        {
            var propInfo = obj.GetType().GetProperty(propertyName);

            if (propInfo == null)
            {
                //todo use logger : throw new Exception($"Property {propertyName} not found on {obj.GetType().Name}");
                return null;
            }

            return propInfo.GetValue(obj);
        }

        private void TraverseAndSet(string[] segments, object currentObj, object value, string path)
        {
            for (int i = 0; i < segments.Length; i++)
            {
                var segment = segments[i];
                bool isLastSegment = i == segments.Length - 1;

                if (isLastSegment)
                {
                    SetValueToTarget(currentObj, segment, value, path);
                    return;
                }
                else
                {
                    currentObj = GetSegmentValue(currentObj, segment);

                    if (currentObj == null)
                        throw new NullReferenceException($"Property {segment} was null and could not traverse to {segments[i + 1]}");
                }
            }
        }

        private void SetValueToTarget(object targetObj, string segment, object value, string path)
        {
            if (IsIndexedProperty(segment))
            {
                SetIndexedPropertyValue(targetObj, segment, value);
            }
            else
            {
                SetPropertyValue(targetObj, segment, value);
            }
        }

        private static void SetPropertyValue(object obj, string propertyName, object value)
        {
            var propInfo = GetPropertyInfo(obj, propertyName);
            if (propInfo != null)
            {
                propInfo.SetValue(obj, value);
            }
        }

        private static void SetIndexedPropertyValue(object obj, string segment, object value)
        {
            var (propertyName, index) = ExtractPropertyAndIndex(segment);
            var listObj = GetPropertyValue(obj, propertyName) as IList;

            ValidateListAndIndex(listObj, propertyName, index, segment);

            listObj[index] = value;
        }

        private static PropertyInfo? GetPropertyInfo(object obj, string propertyName)
        {
            if (obj == null) { return null; }
            var propInfo = obj.GetType().GetProperty(propertyName);
            if (propInfo == null)
            {
                return null;
                // todo use logger throw new Exception($"Property {propertyName} not found on {obj.GetType().Name}");
            }
            return propInfo;
        }

        private void TraverseAndRemove(string[] segments, object currentObj, string path)
        {
            for (int i = 0; i < segments.Length; i++)
            {
                var segment = segments[i];
                bool isLastSegment = i == segments.Length - 1;

                if (isLastSegment)
                {
                    RemoveValueFromTarget(currentObj, segment);
                }
                else
                {
                    currentObj = GetSegmentValue(currentObj, segment);

                    if (currentObj == null)
                        throw new Exception($"Property {segment} was null and could not traverse to {segments[i + 1]}");
                }
            }
        }

        private void RemoveValueFromTarget(object targetObj, string segment)
        {
            if (IsIndexedProperty(segment))
            {
                RemoveIndexedValue(targetObj, segment);
                return;
            }

            RemoveNonIndexedValue(targetObj, segment);
        }

        private void RemoveNonIndexedValue(object targetObj, string segment)
        {
            var propertyInfo = targetObj.GetType().GetProperty(segment);
            if (propertyInfo == null)
            {
                throw new Exception($"Property {segment} does not exist on the target object.");
            }

            SetDefaultValue(targetObj, propertyInfo);
        }

        private void SetDefaultValue(object targetObj, PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.IsValueType && Nullable.GetUnderlyingType(propertyInfo.PropertyType) == null)
            {
                // Handle non-nullable value types with their default value
                propertyInfo.SetValue(targetObj, Activator.CreateInstance(propertyInfo.PropertyType));
            }
            else
            {
                // Set reference types or nullable value types to null
                propertyInfo.SetValue(targetObj, null);
            }
        }

        private static void RemoveIndexedValue(object obj, string segment)
        {
            var (propertyName, index) = ExtractPropertyAndIndex(segment);
            var listObj = GetPropertyValue(obj, propertyName) as IList;

            ValidateListAndIndex(listObj, propertyName, index, segment);

            listObj.RemoveAt(index);
        }

        public void AddValue(string path, object obj, object defaultValue)
        {
            ValidateInput(path, obj);

            var segments = NormalizePath(path).Split('.');

            TraverseAndAdd(segments, obj, defaultValue, path);
        }

        private void TraverseAndAdd(string[] segments, object currentObj, object defaultValue, string path)
        {
            var previousObj = currentObj;
            for (int i = 0; i < segments.Length; i++)
            {
                var segment = segments[i];
                bool isLastSegment = i == segments.Length - 1;
                var parentObj = currentObj;

                currentObj = GetSegmentValue(currentObj, segment);

                if (currentObj == null)
                    throw new NullReferenceException($"Property {segment} was null and could not traverse to {segments[i + 1]}");

                if (isLastSegment)
                    AddValueToTarget(currentObj, parentObj, segment, defaultValue, path);
            }
        }

        private object GetSegmentValue(object obj, string segment)
        {
            return IsIndexedProperty(segment) ? GetIndexedPropertyValue(obj, segment) : GetPropertyValue(obj, segment);
        }

        private void AddValueToTarget(object targetObj, object rootObj, string segment, object defaultValue, string path)
        {
            if (targetObj.GetType().IsArray)
            {
                ExpandArray(targetObj, rootObj, segment, defaultValue, path);
            }
            else if (targetObj is IList list)
            {
                list.Add(defaultValue);
            }
            else
            {
                throw new InvalidOperationException($"Expected a list or array at path '{path}', but found type {targetObj.GetType().Name}.");
            }
        }

        private void ExpandArray(object arrayObj, object rootObj, string segment, object newValue, string path)
        {
            var array = (Array)arrayObj;
            var elementType = array.GetType().GetElementType();
            var newArray = Array.CreateInstance(elementType, array.Length + 1);
            Array.Copy(array, newArray, array.Length);
            newArray.SetValue(newValue, array.Length);
            SetPropertyValue(rootObj, segment, newArray); // Replace the old array with the new array
        }
    }
}
