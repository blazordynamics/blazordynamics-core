using BlazorDynamics.Core.Helpers;
using BlazorDynamics.Core.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorDynamics.Forms.Commons
{
    public partial class BDForm : Components.LayoutFormComponent
    {
         public override string ValidationString => TokenReplacer.ReplaceTokens(InvalidMessage, this);
   }
}
