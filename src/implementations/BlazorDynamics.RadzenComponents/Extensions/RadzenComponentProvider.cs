using BlazorDynamics.RadzenComponents.Actions;
using BlazorDynamics.RadzenComponents.Content;
using BlazorDynamics.RadzenComponents.Input;
using BlazorDynamics.RadzenComponents.Layout;
using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Contracts;
using BlazorDynamics.Core.Models;

namespace BlazorDynamics.RadzenComponents.Extensions
{
    public class RadzenComponentProvider : IComponentProvider
    {
        public ComponentsList GetComponents()
        {
            return new ComponentsList {
                {new ComponentSelectionKey(ComponentType.String) , typeof(StringComponent) },
                {new ComponentSelectionKey(ComponentType.DateTime) , typeof(DateTimeComponent) },
                {new ComponentSelectionKey(ComponentType.HorizontalLayout), typeof(HorizontalLayout) },
                {new ComponentSelectionKey(ComponentType.VerticalLayout), typeof(VerticalLayout) },
                {new ComponentSelectionKey(ComponentType.Number), typeof(NumberComponent) },
                {new ComponentSelectionKey(ComponentType.Int), typeof(IntComponent) },
                {new ComponentSelectionKey(ComponentType.Int,"starRating"), typeof(IntStarComponent) },
                {new ComponentSelectionKey(ComponentType.Boolean), typeof(BooleanComponent) },
                {new ComponentSelectionKey(ComponentType.GroupLayout), typeof(GroupLayout) },
                {new ComponentSelectionKey(ComponentType.Dropdown), typeof(DropDownComponent) },
                {new ComponentSelectionKey(ComponentType.List), typeof(ListComponent) },
                {new ComponentSelectionKey(ComponentType.DeleteAction), typeof(DeleteAction) },
                {new ComponentSelectionKey(ComponentType.AddAction), typeof(AddAction) },
                {new ComponentSelectionKey(ComponentType.SubmitAction), typeof(SubmitAction) },
                {new ComponentSelectionKey(ComponentType.NumberDisplay), typeof(NumberDisplay) },
                {new ComponentSelectionKey(ComponentType.StringDisplay), typeof(StringDisplay) }
            };
        }
    }
}
