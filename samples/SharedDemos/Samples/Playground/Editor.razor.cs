using BlazorDynamics.Core.Models;
using BlazorDynamics.Forms.Components.Factories;
using System.Dynamic;

namespace SharedDemos.Samples.Playground
{
    public partial class Editor : SampleBasePage
    {
        private bool EditModeIndicator = true;
        private void update()
        {
            StateHasChanged();
        }

        public ExpandoObject data = new ExpandoObject();

        public DynamicFormModel model = FormFactory.GroupLayout(
            FormFactory.BoolComponent("Check", "$.check"),
            FormFactory.StringComponent("Text", "$.text")
        );
    }
}
