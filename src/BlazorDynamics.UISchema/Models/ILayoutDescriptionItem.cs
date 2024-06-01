using BlazorDynamics.UISchema.Enums;

namespace BlazorDynamics.UISchema.Models;

public interface ILayoutDescriptionItem
{
    public LayoutType Type { get; }

    public RuleItem? Rule { get; }
}