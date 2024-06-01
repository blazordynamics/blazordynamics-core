//using BlazorDynamics.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BlazorDynamics.Core.Parser
{
    public class DynamicFormModelParser
    {
        private JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> {
                new OptionsDictionaryConverter(),
                new ParameterListConverter(),
                new StringEnumConverter()
                },
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new SkipEmptyCollectionsContractResolver()
        };

        public string Serialize(DynamicFormModel dynamicFormModel)
        {
            return JsonConvert.SerializeObject(dynamicFormModel, settings);
        }

        public DynamicFormModel Deserialize(string jsonModel)
        {
            try
            {
                return JsonConvert.DeserializeObject<DynamicFormModel>(jsonModel, settings);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
