using BlazorDynamics.Core.Contracts;
using BlazorDynamics.Core.Models;
using BlazorDynamics.DynamicUI.JsonSchema.Implementations;
using BlazorDynamics.UISchema.Implementations;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;


namespace BlazorDynamics.Forms.Commons
{
    public partial class BlazorDynamicsForm : ComponentBase
    {
        [Parameter]
        public JObject Schema { get; set; }

        [Parameter]
        public JToken UISchema { get; set; }

        [Parameter]
        public object Data { get; set; }

        [Parameter]
        public EventCallback<object?> ValueChanged { get; set; }

        [Parameter]
        public ComponentsList? Components { get; set; }

        private DynamicFormModel model;

        protected override Task OnParametersSetAsync()
        {
            if (UISchema != null)
            {
                var creator = new DynamicFormModelCreator(new UISchemaParser(new JsonSchemaScopeProvider(new SchemaReader(), Schema)));
                model = creator.GenerateModels(UISchema).First();
            }
            return base.OnParametersSetAsync();
        }
    }
}
