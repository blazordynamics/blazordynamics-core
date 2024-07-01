using BlazorDynamics.Forms.Commons.DataHandlers;

namespace BlazorDynamics.Forms.Commons.Components
{
    public class ValueHandler
    {
        protected ValueHandler()
        {
            
        }

        public static void UpdateValue(FormComponentBase component, object? propertyValue)
        {
            if(component == null) return;   

            if(propertyValue == null) {
                DataObjectHelper.SetValue(component.GetInstancePath(component.Path), component.Value, null);
            }

            if (propertyValue != DataObjectHelper.GetValue(component.Path, component.Value))
            {
                DataObjectHelper.SetValue(component.GetInstancePath(component.Path), component.Value, propertyValue);
                component.ValueChanged.InvokeAsync(propertyValue);
                component.Validate();
            }
        }
    }
}
