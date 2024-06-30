﻿using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Contracts;
using BlazorDynamics.Core.Models;
using BlazorDynamics.HTMLComponents.Actions;
using BlazorDynamics.HTMLComponents.Content;
using BlazorDynamics.HTMLComponents.Input;
using BlazorDynamics.HTMLComponents.Layout;

namespace BlazorDynamics.HTMLComponents.Extensions
{
    public class HtmlComponentsProvider : IComponentProvider
    {


        public ComponentsList GetComponents()
        {
            return new ComponentsList{
                {new ComponentSelectionKey(TypeName.String) , typeof(StringComponent) },
                {new ComponentSelectionKey(TypeName.DateTime) , typeof(DateTimeComponent) },
                {new ComponentSelectionKey(TypeName.HorizontalLayout), typeof(HorizontalLayout) },
                {new ComponentSelectionKey(TypeName.VerticalLayout), typeof(VerticalLayout) },
                {new ComponentSelectionKey(TypeName.Number), typeof(NumberComponent) },
                {new ComponentSelectionKey(TypeName.Int), typeof(IntComponent) },
                {new ComponentSelectionKey(TypeName.Boolean), typeof(BooleanComponent) },
                {new ComponentSelectionKey(TypeName.GroupLayout), typeof(GroupLayout) },
                {new ComponentSelectionKey(TypeName.Dropdown), typeof(DropDownComponent) },
                {new ComponentSelectionKey(TypeName.List), typeof(ListComponent) },
                {new ComponentSelectionKey(TypeName.DeleteAction), typeof(DeleteAction) },
                {new ComponentSelectionKey(TypeName.AddAction), typeof(AddAction) },
                {new ComponentSelectionKey(TypeName.SubmitAction), typeof(SubmitAction) },
                {new ComponentSelectionKey(TypeName.NumberDisplay), typeof(NumberDisplay) },
                {new ComponentSelectionKey(TypeName.StringDisplay), typeof(StringDisplay) }
                };
        }
    }
}
