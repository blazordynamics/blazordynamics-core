using BlazorDynamics.Common.Enums;
using Newtonsoft.Json;

namespace BlazorDynamics.Core.Models;

public class ComponentSelectionKey
{


    public TypeName TypeName { get; }

    [JsonProperty("variationName")]
    public string? VariationName { get; } = null;

    [JsonConstructor]
    public ComponentSelectionKey(TypeName typeName, string? variationName = default!)
    {
        TypeName = typeName;
        VariationName = variationName ?? null;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        ComponentSelectionKey other = (ComponentSelectionKey)obj;
        return TypeName == other.TypeName && VariationName == other.VariationName;
    }

    public override int GetHashCode()
    {
        return (TypeName, VariationName).GetHashCode();
    }
}
