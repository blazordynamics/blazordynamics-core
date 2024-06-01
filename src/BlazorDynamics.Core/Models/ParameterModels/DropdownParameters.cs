namespace BlazorDynamics.Core.Models.ParameterModels
{
    public class DropdownParameters
    {
        public static ParameterList Set(string label, Dictionary<object, string> options, string path)
        {
            return new ParameterList()
            {
                { ParameterNames.Label , label },
                { ParameterNames.Options, options},
                { ParameterNames.Path, path},
            };
        }
    }
}
