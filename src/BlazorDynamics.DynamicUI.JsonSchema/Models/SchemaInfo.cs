using BlazorDynamics.Common.Enums;

namespace BlazorDynamics.DynamicUI.JsonSchema.Models;

public interface ISchemaItem
{
    public TypeName Type { get; }
    
    public Dictionary<string, object> ItemMetadata { get; }
}