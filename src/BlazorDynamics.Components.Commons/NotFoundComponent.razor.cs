using BlazorDynamics.Core.Helpers;
using BlazorDynamics.Forms.Commons.Components;

namespace BlazorDynamics.Forms.Commons
{
    public partial class NotFoundComponent : DisplayFormComponent
    {
        public override string ValidationString => TokenReplacer.ReplaceTokens(InvalidMessage, this);

    }
}
