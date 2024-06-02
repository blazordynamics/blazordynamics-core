using BlazorDynamics.Forms.Commons.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorDynamics.Forms.Components
{
    public partial class BDFormComponentWrapper: ComponentBase
    {
        private string hoverClass = string.Empty;
        [Parameter] public string Class { get; set; } 
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public FormComponentBase FormComponent { get; set; }
        [Parameter] public bool ShowValidation { get; set; } = true;
        [Parameter] public bool EditMode { get; set; } = false;
       

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            EditMode = FormComponent?.EditMode ?? false; 
        }
 
    }
}