using BlazorDynamics.Common.Enums;

namespace BlazorDynamics.DynamicUI.JsonSchema.Models;

public class ObjectSchemaItem : SchemaItem
{
    public List<string>? RequiredElements { get; set; }
    public int? MaximumProperties { get; set; }
    public int? MinimumProperties { get; set; }
    
    public ObjectSchemaItem(string path) : base(ComponentType.Object, path)
    {
    }
}