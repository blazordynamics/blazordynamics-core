using BlazorDynamics.Forms.Commons.ObjectHandlers;

namespace BlazorDynamics.Forms.Commons.Components
{
    public class ValueHandler
    {
        public static void UpdateValue(FormComponentBase component, object propertyValue)
        {
            if (propertyValue != DataObjectHelper.GetValue(component.Path, component.Value))
            {
                DataObjectHelper.SetValue(component.GetInstancePath(component.Path), component.Value, propertyValue);
                component.ValueChanged.InvokeAsync(propertyValue);
                component.Validate();
            }
        }
    }
}
