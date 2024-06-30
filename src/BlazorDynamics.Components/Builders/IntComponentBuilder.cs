using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Core.Models.ParameterModels;
using BlazorDynamics.Forms.Components.Input;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class IntComponentBuilder : FormComponentBuilder<IntComponentBase, IntComponentBuilder>
    {
        public IntComponentBuilder WithLabel(string label)
        {
            _component.Label = label;
            return this;
        }

        public IntComponentBuilder WithMinimum(int minimum)
        {
            _component.Minimum = minimum;
            return this;
        }

        public IntComponentBuilder WithMaximum(int maximum)
        {
            _component.Maximum = maximum;
            return this;
        }

        public new DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(TypeName.Int);
            model.Parameters.Add(ParameterNames.Label, _component.Label);
            model.Parameters.Add(ParameterNames.Minimum, _component.Minimum);
            model.Parameters.Add(ParameterNames.Maximum, _component.Maximum);
            return model;
        }
    }

}
