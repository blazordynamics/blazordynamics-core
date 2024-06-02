using BlazorDynamics.Common.Enums;
using BlazorDynamics.Forms.Components.Layout;
using BlazorDynamics.Core.Models;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class HorizontalLayoutBuilder : FormComponentBuilder<HorizontalLayoutBase, HorizontalLayoutBuilder>
    {
        public new DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(TypeName.HorizontalLayout);
            return model;
        }
    }
}