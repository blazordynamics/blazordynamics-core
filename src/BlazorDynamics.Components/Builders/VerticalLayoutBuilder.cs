using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Forms.Components.Layout;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class VerticalLayoutBuilder : FormComponentBuilder<VerticalLayoutBase, VerticalLayoutBuilder>
    {
        public new DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(TypeName.VerticalLayout);
            return model;
        }
    }
}