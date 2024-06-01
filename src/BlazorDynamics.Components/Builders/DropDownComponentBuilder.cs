using BlazorDynamics.Common.Enums;
using BlazorDynamics.Forms.Components.Input;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Core.Models.ParameterModels;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class DropDownComponentBuilder : FormComponentBuilder<DropDownComponentBase, DropDownComponentBuilder>
    {
        public DropDownComponentBuilder WithLabel(string label)
        {
            _component.Label = label;
            return this;
        }

        public DropDownComponentBuilder WithOptions(Dictionary<object, string> options)
        {
            _component.Options = options;
            return this;
        }

        public DropDownComponentBuilder WithSelectedValue(object selectedValue)
        {
            _component.SelectedValue = selectedValue;
            return this;
        }

        public new DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(ComponentType.Dropdown);
            model.Parameters.Add(ParameterNames.Label, _component.Label);
            model.Parameters.Add(ParameterNames.Options, _component.Options);
            model.Parameters.Add(ParameterNames.SelectedValue, _component.SelectedValue);
            return model;
        }
    }
}
