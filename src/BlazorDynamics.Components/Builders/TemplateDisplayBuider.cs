using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Forms.Components.Content;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class TemplateDisplayBuider : FormComponentBuilder<TemplateDisplayBase, TemplateDisplayBuider>
    {
        public TemplateDisplayBuider Path(string path)
        {
            _component.SetPath(path);
            return this;
        }

        public new DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(TypeName.TemplateContent, typeDefinitionName);
            return model;
        }
    }
}
