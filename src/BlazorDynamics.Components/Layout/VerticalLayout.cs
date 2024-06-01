using BlazorDynamics.Common.Helpers;

using BlazorDynamics.Forms.Commons.Components;

namespace BlazorDynamics.Forms.Components.Layout
{
    public class VerticalLayoutBase : InputFormComponent
    {
        public override string ValidationString => TokenReplacer.ReplaceTokens(InvalidMessage, this);

    }
}