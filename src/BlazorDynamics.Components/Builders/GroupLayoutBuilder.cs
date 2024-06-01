using BlazorDynamics.Common.Enums;
using BlazorDynamics.Forms.Components.Layout;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Core.Models.ParameterModels;
using BlazorDynamics.Forms.Components.Factories;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class GroupLayoutBuilder : FormComponentBuilder<GroupLayoutBase, GroupLayoutBuilder>
    {
        public GroupLayoutBuilder WithLabel(string label)
        {
            _component.Label = label;
            return this;
        }

        public new DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(ComponentType.GroupLayout);
            model.Parameters.Add(ParameterNames.Label, _component.Label);
            return model;
        }

      
    }
}