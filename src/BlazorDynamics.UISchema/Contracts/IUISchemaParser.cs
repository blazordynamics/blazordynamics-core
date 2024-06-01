using BlazorDynamics.UISchema.Models;
using Newtonsoft.Json.Linq;

namespace BlazorDynamics.UISchema.Contracts;

public interface IUISchemaParser
{
    public IEnumerable<ILayoutDescriptionItem> ParseUISchema(JToken UISchema);
}