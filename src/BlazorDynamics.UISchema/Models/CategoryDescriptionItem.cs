using BlazorDynamics.UISchema.Enums;
using System.Runtime.CompilerServices;

namespace BlazorDynamics.UISchema.Models;

public class CategoryDescriptionItem : ILayoutDescriptionItem
{
    public LayoutType Type => LayoutType.Category;
    /// <summary>
    /// Defines an additional string that is used to describe the elements of the group
    /// </summary>
    public string Label { get; set; } = string.Empty;

    public IEnumerable<ILayoutDescriptionItem> Elements { get; set; } = Enumerable.Empty<ILayoutDescriptionItem>();

    public RuleItem? Rule { get; set; }
}