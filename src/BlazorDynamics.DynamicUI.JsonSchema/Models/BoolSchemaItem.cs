using BlazorDynamics.Common.Enums;

namespace BlazorDynamics.DynamicUI.JsonSchema.Models;

public class BoolSchemaItem : SchemaItem
{
    public BoolSchemaItem(string path) : base(TypeName.Boolean, path)
    {
    }
}