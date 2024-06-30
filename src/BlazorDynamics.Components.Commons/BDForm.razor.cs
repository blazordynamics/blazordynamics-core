using BlazorDynamics.Common.Helpers;

namespace BlazorDynamics.Forms.Commons
{
    public partial class BDForm : Components.LayoutFormComponent
    {
        public override string ValidationString => TokenReplacer.ReplaceTokens(InvalidMessage, this);
    }
}
