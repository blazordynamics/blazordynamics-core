using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BlazorDynamics.Common.Parser
{
    public class ModelParser<T>
    {
        private readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> {
                new OptionsDictionaryConverter(),
                new ParameterListConverter(),
                new StringEnumConverter()
                },
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new SkipEmptyCollectionsContractResolver<T>()
        };

        public string Serialize(T dynamicFormModel)
        {
            return JsonConvert.SerializeObject(dynamicFormModel, settings);
        }

        public T? Deserialize(string jsonModel)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonModel, settings) ?? default;
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
