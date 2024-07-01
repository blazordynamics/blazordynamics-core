using BlazorDynamics.Common.Helpers;
using BlazorDynamics.Forms.Commons.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorDynamics.Forms.Components.Input
{
    public class DateTimeComponentBase : InputFormComponent
    {
        [Parameter]
        public string Label { get; set; } = string.Empty;
        [Parameter]
        public DateTime? MinimumDateTime { get; set; } = DateTime.MinValue;
        [Parameter]
        public DateTime? MaximumDateTime { get; set; } = DateTime.MaxValue;
        [Parameter]
        public string Format { get; set; } = "";


        public DateTime? _localDate = null;
        public DateTime? DateTimeValue { get { return _localDate; } set { _localDate = value; ValueHandler.UpdateValue(this, _localDate); } }

        public override string ValidationString => TokenReplacer.ReplaceTokens(InvalidMessage, this);



        protected override void OnParametersSet()
        {

            try
            {
                _localDate = Convert.ToDateTime(GetValue());
            }
            catch (Exception)
            {
                _localDate = null;
                IsValid = false;
            }
        }

        public override void Validate()
        {

            if (_localDate < MinimumDateTime || _localDate > MaximumDateTime)
            {
                IsValid = false;

            }
            else
            {
                IsValid = true;
            }

            base.Validate();
        }

    }
}