using BlazorDynamics.UISchema.Enums;

namespace BlazorDynamics.UISchema.Models;

public class ControlDescriptionItem : ILayoutDescriptionItem
{
    public string Scope { get; set; } = string.Empty;   
    public string Label { get; set; } = string.Empty;
    public bool ShowLabel { get; set; } = true;
    public ControlOptions? Options { get; set; }
    public LayoutType Type => LayoutType.Control;

    public Dictionary<string, object>? ScopeMetadata { get; set; }

    public RuleItem? Rule { get; set; }
}