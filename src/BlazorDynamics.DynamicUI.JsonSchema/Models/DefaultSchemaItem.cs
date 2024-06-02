using BlazorDynamics.Common.Enums;

namespace BlazorDynamics.DynamicUI.JsonSchema.Models;

public class DefaultSchemaItem : SchemaItem
{
    public DefaultSchemaItem(string path) : base(TypeName.Default, path)
    {
    }
}