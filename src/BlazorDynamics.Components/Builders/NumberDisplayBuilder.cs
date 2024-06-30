using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Core.Models.ParameterModels;
using BlazorDynamics.Forms.Components.Content;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class NumberDisplayBuilder : FormComponentBuilder<NumberDisplayBase, NumberDisplayBuilder>
    {
        public NumberDisplayBuilder WithLabel(string label)
        {
            _component.Label = label;
            return this;
        }

        public new DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(TypeName.NumberDisplay);
            model.Parameters.Add(ParameterNames.Label, _component.Label);
            return model;
        }
    }
}
