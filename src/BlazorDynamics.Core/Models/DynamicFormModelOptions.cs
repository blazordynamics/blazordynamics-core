namespace BlazorDynamics.Core.Models;

public class DynamicFormModelOptions
{
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// defines the way the items in the array are gonna be presented as Radio, 
    /// </summary>
    public bool RadioGroup { get; set; } = false;

    public bool Readonly { get; set; } = false;

    /// <summary>
    /// toggle additional buttons that allow chaging the order of the items in the array
    /// </summary>
    public bool ShowSortButtons { get; set; }

    /// <summary>
    /// when the itme is collapesed this will indicate with property is going to be used as the description for that element
    /// </summary>
    public string ElementLabelProperty { get; set; }
}