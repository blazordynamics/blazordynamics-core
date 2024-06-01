using BlazorDynamics.Common.Helpers;
using BlazorDynamics.Forms.Commons.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorDynamics.Forms.Components.Layout
{
    public class GroupLayoutBase : LayoutFormComponent
    {
        [Parameter]
        public string Label { get; set; }

        public override string ValidationString => TokenReplacer.ReplaceTokens(InvalidMessage, this);

    }
}