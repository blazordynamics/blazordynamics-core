using BlazorDynamics.UISchema.Enums;

namespace BlazorDynamics.UISchema.Models;

/// <summary>
/// Categorization uses the Categorization type and can only contain elements of type Category
/// </summary>
public class CategorizationDescriptionItem : ILayoutDescriptionItem
{
    public LayoutType Type => LayoutType.Categorization;

    public List<CategoryDescriptionItem> Elements { get; set; }

    public RuleItem? Rule { get; set; }
}