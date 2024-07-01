using BlazorDynamics.Common.Helpers;

using BlazorDynamics.Forms.Commons.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorDynamics.Forms.Components.Input
{
    public class BooleanComponentBase : InputFormComponent
    {
        [Parameter]
        public string Label { get; set; }  = String.Empty;

        [Parameter]
        public bool? NeedsToBeChecked { get; set; }

        public override string ValidationString => TokenReplacer.ReplaceTokens(InvalidMessage, this);

        public bool? _booleanValue = false;
        public bool? BooleanValue { get { return _booleanValue; } set { _booleanValue = value; ValueHandler.UpdateValue(this, _booleanValue); } }
        protected override void OnParametersSet()
        {
            try
            {
                _booleanValue = Convert.ToBoolean(GetValue());
            }
            catch (Exception)
            {
                _booleanValue = false;
                IsValid = false;
            }
        }


        public override void Validate()
        {

            if (!_booleanValue ?? false && (NeedsToBeChecked ?? false))
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