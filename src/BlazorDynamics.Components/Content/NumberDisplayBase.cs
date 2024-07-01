using BlazorDynamics.Common.Helpers;
using BlazorDynamics.Forms.Commons.Components;
using BlazorDynamics.Forms.Commons.DataHandlers;
using Microsoft.AspNetCore.Components;

namespace BlazorDynamics.Forms.Components.Content
{
    public class NumberDisplayBase : DisplayFormComponent
    {
        [Parameter]
        public string Label { get; set; } = string.Empty;

        // public int _numberValue = 0;
        public int NumberValue { get { return CalculateCounter(); } }

        public override string ValidationString => TokenReplacer.ReplaceTokens(InvalidMessage, this);

        private int CalculateCounter()
        {
            ;
            return DataObjectHelper.GetCounterValue(GetInstancePath(Path), Value);
        }
    }
}