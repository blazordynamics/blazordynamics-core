using BlazorDynamics.Common.Models;

namespace BlazorDynamics.Core.Models.ParameterModels
{
    public class BooleanParameters : BaseParameters
    {
        public static ParameterList Set(string label, string path)
        {
            return new ParameterList
            {
                { ParameterNames.Label , label },
                { ParameterNames.Path, path}
            };
        }
    }
}
