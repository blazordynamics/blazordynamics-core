using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;
using BlazorDynamics.Core.Models.ParameterModels;


namespace BlazorDynamics.Core.Parser;

public class SkipEmptyCollectionsContractResolver<T> : CamelCasePropertyNamesContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        JsonProperty property = base.CreateProperty(member, memberSerialization);

        if (property.PropertyType == typeof(ParameterList))
        {
            property.ShouldSerialize = instance =>
            {
                var prop = member as PropertyInfo;
                var parameterList = prop?.GetValue(instance) as ParameterList;
                return parameterList != null && parameterList.Entries.Any();
            };
        }
        else if (property.PropertyType == typeof(List<T>))
        {
            property.ShouldSerialize = instance =>
            {
                var prop = member as PropertyInfo;
                var elements = prop?.GetValue(instance) as List<T>;
                return elements != null && elements.Any();
            };
        }

        return property;
    }
}
