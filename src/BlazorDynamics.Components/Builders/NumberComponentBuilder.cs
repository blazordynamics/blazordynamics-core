using BlazorDynamics.Common.Enums;
using BlazorDynamics.Forms.Components.Input;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Core.Models.ParameterModels;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class NumberComponentBuilder : FormComponentBuilder<NumberComponentBase, NumberComponentBuilder>
    {
        public NumberComponentBuilder WithLabel(string label)
        {
            _component.Label = label;
            return this;
        }

        public NumberComponentBuilder WithMinimum(double minimum)
        {
            _component.Minimum = minimum;
            return this;
        }

        public NumberComponentBuilder WithMaximum(double maximum)
        {
            _component.Maximum = maximum;
            return this;
        }

        public new DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(TypeName.Number);
            model.Parameters.Add(ParameterNames.Label, _component.Label);
            model.Parameters.Add(ParameterNames.Minimum, _component.Minimum);
            model.Parameters.Add(ParameterNames.Maximum, _component.Maximum);
            return model;
        }
    }
}
