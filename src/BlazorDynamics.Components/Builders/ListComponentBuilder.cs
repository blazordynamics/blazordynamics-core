using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Core.Models.ParameterModels;
using BlazorDynamics.Forms.Components.Layout;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class ListComponentBuilder : FormComponentBuilder<ListComponentBase, ListComponentBuilder>
    {
        public ListComponentBuilder WithLabel(string label)
        {
            _component.Label = label;
            return this;
        }

        public ListComponentBuilder WithListValue(List<object> listValue)
        {
            _component.ListValue = listValue;
            return this;
        }

        public new DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(TypeName.List);
            model.Parameters.Add(ParameterNames.Label, _component.Label);
            model.Parameters.Add(ParameterNames.ListValue, _component.ListValue);
            return model;
        }
    }

}
