using BlazorDynamics.Common.Enums;

namespace BlazorDynamics.DynamicUI.JsonSchema.Models;

public class ArraySchemaItem : SchemaItem
{
    public int? MinimumItems { get; set; }
    public int? MaximumItems { get; set; }
    public bool? UniqueItems { get; set; }

    public ArraySchemaItem(string path) : base(TypeName.Array, path)
    {
    }
}