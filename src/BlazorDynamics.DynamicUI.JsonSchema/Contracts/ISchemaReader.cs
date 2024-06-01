using BlazorDynamics.DynamicUI.JsonSchema.Models;
using Newtonsoft.Json.Linq;

namespace BlazorDynamics.DynamicUI.JsonSchema.Contracts;

public interface ISchemaReader
{
    ISchemaItem GetTypeFromPath(JObject schema, string schemaPath);
}