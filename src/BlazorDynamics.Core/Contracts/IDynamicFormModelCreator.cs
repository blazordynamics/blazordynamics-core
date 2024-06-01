using BlazorDynamics.Core.Models;
using Newtonsoft.Json.Linq;

namespace BlazorDynamics.Core.Contracts;

public interface IDynamicFormModelCreator
{
    public IEnumerable<DynamicFormModel> GenerateModels(JToken uiSchema);
}