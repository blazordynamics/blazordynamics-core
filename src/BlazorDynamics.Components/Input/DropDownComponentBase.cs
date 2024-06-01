using BlazorDynamics.Core.Helpers;
using BlazorDynamics.Forms.Commons.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorDynamics.Forms.Components.Input
{
    public class DropDownComponentBase : InputFormComponent
    {
        [Parameter]
        public string Label { get; set; }

        [Parameter]
        public Dictionary<object, string> Options { get; set; } = new Dictionary<object, string>();

        private Dictionary<string, object> _mapping = new Dictionary<string, object>();
        private string _selectedValueId = string.Empty;

        public override string ValidationString => TokenReplacer.ReplaceTokens(InvalidMessage, this);

        public object SelectedValue
        {
            get => _mapping.TryGetValue(_selectedValueId, out var value) ? value : null;
            set
            {
                _selectedValueId = _mapping.FirstOrDefault(x => x.Value.Equals(value)).Key;
                ValueHandler.UpdateValue(this, value);
            }
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            InitializeMapping();
            object currentValue = null;
            try
            {
                currentValue = GetValue();
            }
            catch (Exception)
            {
                IsValid = false;
            }
            if (currentValue != null)
            {
                _selectedValueId = _mapping.FirstOrDefault(x => x.Value.Equals(currentValue)).Key;
            }
        }

        private void InitializeMapping()
        {
            _mapping.Clear();
            foreach (var option in Options)
            {
                var uniqueId = GenerateUniqueId(option.Key);
                _mapping[uniqueId] = option.Key;
            }
        }

        private string GenerateUniqueId(object obj)
        {
            // Using hash code for simplicity; ensure this is unique enough for your use case
            return "unique_" + obj.GetHashCode();
        }

        public void HandleChange(ChangeEventArgs e)
        {
            _selectedValueId = e.Value?.ToString();
            var selectedObject = _mapping.TryGetValue(_selectedValueId, out var value) ? value : null;
            SelectedValue = selectedObject; // This will call UpdateValue
        }

        public string GetUniqueId(object key)
        {
            return _mapping.FirstOrDefault(x => x.Value.Equals(key)).Key;
        }
    }
}
