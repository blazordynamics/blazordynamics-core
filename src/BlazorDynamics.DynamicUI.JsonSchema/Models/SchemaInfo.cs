using BlazorDynamics.Common.Enums;

namespace BlazorDynamics.DynamicUI.JsonSchema.Models;

public interface ISchemaItem
{
    public ComponentType Type { get; }
    
    public Dictionary<string, object> ItemMetadata { get; }
}