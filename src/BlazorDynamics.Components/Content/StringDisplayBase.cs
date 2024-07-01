using BlazorDynamics.Common.Helpers;
using BlazorDynamics.Forms.Commons.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorDynamics.Forms.Components.Content
{
    public class StringDisplayBase : DisplayFormComponent
    {
        [Parameter]
        public string Label { get; set; } = string.Empty;   

        public override string ValidationString => TokenReplacer.ReplaceTokens(InvalidMessage, this);

        public string StringValue { get { return Convert.ToString(GetValue() ?? "") ?? string.Empty; } }
    }
}