using BlazorDynamics.Common.Enums;

namespace BlazorDynamics.DynamicUI.JsonSchema.Models;

public class DefaultSchemaItem : SchemaItem
{
    public DefaultSchemaItem(string path) : base(ComponentType.Default, path)
    {
    }
}