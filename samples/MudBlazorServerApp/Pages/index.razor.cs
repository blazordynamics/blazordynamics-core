using BlazorDynamics.Core.Models;
using BlazorDynamics.Forms.Components.Factories;
using Microsoft.AspNetCore.Components;

using System.Dynamic;
using System.Text.Json;

namespace MudBlazorServerApp.Pages
{
    public partial class Index : ComponentBase
    {
        private string result = string.Empty;

        private void update()
        {
            result = JsonSerializer.Serialize(data);
            StateHasChanged();
        }

        private ExpandoObject data = new ExpandoObject();

        private DynamicFormModel model = FormFactory.GroupLayout(group =>
            group
               .WithParameter("Collapsed", false)
               .WithParameter("HeaderModel", HeaderForm())
               .WithSubElements(DetailForm())
            ).WithVariationName("Collapsable");


        private static DynamicFormModel HeaderForm()
        {
            return FormFactory.HorizontalLayout(layout =>
                layout.WithSubElements(ReadForm())
            );
        }

        private static DynamicFormModel DetailForm()
        {
            return FormFactory.VerticalLayout(
                FormFactory.StringComponent("Name", "$.name"),
                FormFactory.StringComponent("City", "$.city")
                );
        }
        private static DynamicFormModel ReadForm()
        {
            return FormFactory.HorizontalLayout(
                FormFactory.StringDisplay("$.name"),
                FormFactory.StringDisplay("$.city")
                );
        }
    }
}
