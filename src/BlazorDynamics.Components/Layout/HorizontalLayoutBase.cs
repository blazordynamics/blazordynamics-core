using BlazorDynamics.Common.Helpers;
using BlazorDynamics.Forms.Commons.Components;

namespace BlazorDynamics.Forms.Components.Layout
{
    public class HorizontalLayoutBase : LayoutFormComponent
    {
        public override string ValidationString => TokenReplacer.ReplaceTokens(InvalidMessage, this);

    }
}