using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Core.Models.ParameterModels;
using BlazorDynamics.Forms.Components.Input;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class BooleanComponentBuilder : FormComponentBuilder<BooleanComponentBase, BooleanComponentBuilder>
    {
        public BooleanComponentBuilder WithLabel(string label)
        {
            _component.Label = label;
            return this;
        }

        public BooleanComponentBuilder WithNeedsToBeChecked(bool needsToBeChecked)
        {
            _component.NeedsToBeChecked = needsToBeChecked;
            return this;
        }

        public new DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(TypeName.Boolean);
            model.Parameters.Add(ParameterNames.Label, _component.Label);
            model.Parameters.Add(ParameterNames.NeedsToBeChecked, _component.NeedsToBeChecked);
            return model;
        }
    }
}
