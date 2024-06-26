﻿using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Core.Models.ParameterModels;
using BlazorDynamics.Forms.Components.Input;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class DateTimeComponentBuilder : FormComponentBuilder<DateTimeComponentBase, DateTimeComponentBuilder>
    {
        public DateTimeComponentBuilder WithLabel(string label)
        {
            _component.Label = label;
            return this;
        }

        public DateTimeComponentBuilder WithFormat(string format)
        {
            _component.Format = format;
            return this;
        }

        public DateTimeComponentBuilder WithMinimumDateTime(DateTime? minimumDateTime)
        {
            _component.MinimumDateTime = minimumDateTime;
            return this;
        }

        public DateTimeComponentBuilder WithMaximumDateTime(DateTime? maximumDateTime)
        {
            _component.MaximumDateTime = maximumDateTime;
            return this;
        }

        public new DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(TypeName.DateTime);
            model.Parameters.Add(ParameterNames.Label, _component.Label);
            model.Parameters.Add(ParameterNames.MinimumDateTime, _component.MinimumDateTime);
            model.Parameters.Add(ParameterNames.MaximumDateTime, _component.MaximumDateTime);
            return model;
        }
    }

}
