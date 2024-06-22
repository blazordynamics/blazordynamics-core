using System.Text.RegularExpressions;
using BlazorDynamics.DynamicUI.JsonSchema.Contracts;
using BlazorDynamics.DynamicUI.JsonSchema.Helpers;
using BlazorDynamics.DynamicUI.JsonSchema.Models;
using Newtonsoft.Json.Linq;

namespace BlazorDynamics.DynamicUI.JsonSchema.Implementations;

public class SchemaReader : ISchemaReader
{
    public ISchemaItem GetTypeFromPath(JObject schema, string schemaPath)
    {
        if (!schemaPath.StartsWith("#/"))
            return null;

        string[] parts = SplitSchemaPath(schemaPath);

        JToken currentToken = NavigateSchemaPath(schema, parts);

        return currentToken != null ? JsonSchemaHelper.ExtractSchemaInfo(currentToken) : null;
    }
    
    private string[] SplitSchemaPath(string schemaPath)
    {
        return schemaPath.Substring(2).Split('/');
    }

    private JToken NavigateSchemaPath(JObject schema, string[] parts)
    {
        JToken currentToken = schema;

        for (int i = 0; i < parts.Length; i++)
        {
            string part = parts[i];

            if (part == "properties")
                continue;

            if (IsArrayIndex(part))
            {
                currentToken = NavigateArrayPath(currentToken, part);
            }
            else if (part == "items")
            {
                currentToken = currentToken["items"];
            }
            else
            {
                currentToken = NavigatePropertyPath(currentToken, part);
            }

            if (currentToken == null)
                return null;
        }

        return currentToken;
    }

    private JToken NavigatePropertyPath(JToken currentToken, string part)
    {
        JToken token = currentToken["properties"]?[part];
        return token;
    }

    private JToken NavigateArrayPath(JToken currentToken, string part)
    {
        string propertyName = part.Substring(0, part.IndexOf('['));
        currentToken = currentToken["properties"]?[propertyName] ?? JValue.CreateNull(); ;
      
        if (currentToken.Type.Equals(JTokenType.Null )) { return JValue.CreateNull(); }

        int index = ExtractArrayIndex(part);

        if (currentToken["items"]?.Type == JTokenType.Array)  // Handle tuple scenario
        {
            return currentToken["items"]?[index] ?? JValue.CreateNull(); ;
        }
        else  // Handle standard array of single type scenario
        {
            return currentToken["items"] ??  JValue.CreateNull(); ;
        }
    }

    private static int ExtractArrayIndex(string part)
    {
        return int.Parse(Regex.Match(part, @"\[(\d+)\]").Groups[1].Value);
    }

    private static bool IsArrayIndex(string part)
    {
        return part.EndsWith("]") && part.Contains("[");
    }
}