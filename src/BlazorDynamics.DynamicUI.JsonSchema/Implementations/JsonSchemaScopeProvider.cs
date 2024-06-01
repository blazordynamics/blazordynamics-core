using BlazorDynamics.Contracts;
using BlazorDynamics.DynamicUI.JsonSchema.Contracts;
using Newtonsoft.Json.Linq;

namespace BlazorDynamics.DynamicUI.JsonSchema.Implementations;

public class JsonSchemaScopeProvider : IScopeProvider
{
    private readonly ISchemaReader _schemaReader;
    private readonly JObject _schema;

    public JsonSchemaScopeProvider(ISchemaReader schemaReader, JObject schema)
    {
        _schemaReader = schemaReader;
        _schema = schema;
    }
    
    public Dictionary<string, object> GetDataFromScope(string scope)
    {
        return _schemaReader.GetTypeFromPath(_schema, scope).ItemMetadata;
    }
}