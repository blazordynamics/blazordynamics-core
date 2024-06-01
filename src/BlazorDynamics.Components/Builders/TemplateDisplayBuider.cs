using BlazorDynamics.Forms.Components.Content;
using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Models;

namespace BlazorDynamics.Forms.Components.Builders
{
    public class TemplateDisplayBuider : FormComponentBuilder<TemplateDisplayBase, TemplateDisplayBuider>
    {
        public TemplateDisplayBuider Path(string path)
        {
            _component.Path = path;
            return this;
        }

        public new DynamicFormModel Build()
        {
            var model = base.Build();
            model.DynamicType = new ComponentSelectionKey(ComponentType.TemplateContent,typeDefinitionName);
            return model;
        }
    }
}
