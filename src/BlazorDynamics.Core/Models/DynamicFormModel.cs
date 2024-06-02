using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Models.ParameterModels;
using Newtonsoft.Json;

namespace BlazorDynamics.Core.Models;

public class DynamicFormModel
{
    private DynamicFormModelOptions? _dynamicFormModelOptions;

    public DynamicFormModel()
    {

    }

    public DynamicFormModel(TypeName componentType)
    {
        DynamicType = new ComponentSelectionKey(componentType);
    }
    [JsonConstructor]
    public DynamicFormModel(TypeName componentType, string typeDefinionName)
    {
        DynamicType = new ComponentSelectionKey(componentType, typeDefinionName);
    }
    public DynamicFormModel(ComponentSelectionKey componentSelectionKey)
    {
        DynamicType = componentSelectionKey;
    }

   
    public ComponentSelectionKey DynamicType { get; set; } = new ComponentSelectionKey(TypeName.Default);

   
    public ParameterList Parameters { get; set; } = new();

    public List<DynamicFormModel> SubElements { get; set; } = new();

    public DynamicFormModelOptions DynamicFormModelOptions
    {
        get => _dynamicFormModelOptions;
        set
        {
            _dynamicFormModelOptions = value;
        }
    }

    public List<DynamicFormModelRule>? Rules { get; set; }
}