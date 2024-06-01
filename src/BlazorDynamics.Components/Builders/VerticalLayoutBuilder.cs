using BlazorDynamics.Common.Enums;
using BlazorDynamics.Forms.Components.Layout;
using BlazorDynamics.Core.Models;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class VerticalLayoutBuilder : FormComponentBuilder<VerticalLayoutBase, VerticalLayoutBuilder>
    {
        public new  DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(ComponentType.VerticalLayout);
            return model;
        }
    }
}