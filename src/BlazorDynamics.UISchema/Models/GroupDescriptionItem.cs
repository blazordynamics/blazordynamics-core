using BlazorDynamics.UISchema.Enums;

namespace BlazorDynamics.UISchema.Models;

public class GroupDescriptionItem : ILayoutDescriptionItem
{
    public LayoutType Type => LayoutType.Group;

    public List<ILayoutDescriptionItem> Elements { get; set; } = new List<ILayoutDescriptionItem>();    

    /// <summary>
    /// Defines an additional string that is used to describe the elements of the group
    /// </summary>
    public string Label { get; set; } = string.Empty;   

    public RuleItem? Rule { get; set; }
}