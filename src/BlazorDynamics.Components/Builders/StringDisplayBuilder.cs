using BlazorDynamics.Forms.Components.Content;
using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Core.Models.ParameterModels;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class StringDisplayBuilder : FormComponentBuilder<StringDisplayBase, StringDisplayBuilder>
    {
        public StringDisplayBuilder WithLabel(string label)
        {
            _component.Label = label;
            return this;
        }


        public new DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(TypeName.StringDisplay);
            model.Parameters.Add(ParameterNames.Label, _component.Label);
            return model;
        }
    }
}
