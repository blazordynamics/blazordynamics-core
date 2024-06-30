using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Core.Models.ParameterModels;
using BlazorDynamics.Forms.Components.Input;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class StringComponentBuilder : FormComponentBuilder<StringComponentBase, StringComponentBuilder>
    {
        public StringComponentBuilder WithLabel(string label)
        {
            _component.Label = label;
            return this;
        }
        public StringComponentBuilder WithMinimumLength(long? minimumLength)
        {
            _component.MinimumLength = minimumLength;
            return this;
        }

        public StringComponentBuilder WithFormat(string format)
        {
            _component.Format = format;
            return this;
        }

        public StringComponentBuilder WithMaximumLength(long? maximumLength)
        {
            _component.MaximumLength = maximumLength;
            return this;
        }

        public StringComponentBuilder WithPattern(string? pattern)
        {
            _component.Pattern = pattern;
            return this;
        }

        public new DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(TypeName.String);
            model.Parameters.Add(ParameterNames.Label, _component.Label);
            model.Parameters.Add(ParameterNames.MinimumLength, _component.MinimumLength);
            model.Parameters.Add(ParameterNames.MaximumLength, _component.MaximumLength);
            model.Parameters.Add(ParameterNames.Pattern, _component.Pattern);
            return model;
        }
    }
}
