

namespace BlazorDynamics.Forms.Components.Factories
{
    public static partial class DataFactory
    {
        public static Dictionary<object, string> SelectionList<T>(List<T> items)
        {
            var result = new Dictionary<object, string>();
            foreach (var item in items)
            {
                result.Add(item, item?.ToString() ?? "not set");
            }
            return result;
        }
    }
}
