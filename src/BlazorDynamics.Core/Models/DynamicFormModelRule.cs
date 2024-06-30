using BlazorDynamics.Common.Enums;

namespace BlazorDynamics.Core.Models;

public class DynamicFormModelRule
{
    public RuleEffect Effect { get; }

    public DynamicFormModelRuleCondition Condition { get; }

    public DynamicFormModelRule(RuleEffect? effect, DynamicFormModelRuleCondition condition)
    {
        Effect = effect ?? RuleEffect.SHOW;
        Condition = condition;
    }
}
