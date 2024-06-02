using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Models;
using BlazorDynamics.UISchema.Contracts;
using BlazorDynamics.UISchema.Enums;
using BlazorDynamics.UISchema.Models;
using BlazorDynamics.Core.Models.ParameterModels;
using Newtonsoft.Json.Linq;

namespace BlazorDynamics.Core.Contracts;

public class DynamicFormModelCreator : IDynamicFormModelCreator
{
    private readonly IUISchemaParser _schemaParser;

    public DynamicFormModelCreator(IUISchemaParser schemaParser)
    {
        _schemaParser = schemaParser;
    }

    public IEnumerable<DynamicFormModel> GenerateModels(JToken uiSchema)
    {
        var result = _schemaParser.ParseUISchema(uiSchema);

        var dynamicFormModels = new List<DynamicFormModel>();
        GenerateModelsInternal(result, dynamicFormModels);
        return dynamicFormModels;
    }

    private List<DynamicFormModel> GenerateModelsInternal(IEnumerable<ILayoutDescriptionItem> descriptionItems, List<DynamicFormModel> result)
    {
        foreach (var item in descriptionItems)
        {
            switch (item.Type)
            {
                case LayoutType.HorizontalLayout:
                    var currentItem = item as HorizontalLayoutDescriptionItem;

                    var horizontalDynamicFormModel = new DynamicFormModel
                    {
                        DynamicType = new ComponentSelectionKey(TypeName.HorizontalLayout),
                        SubElements = GenerateModelsInternal(currentItem.Elements, new List<DynamicFormModel>()),
                    };

                    if (currentItem.Rule != null)
                    {
                        horizontalDynamicFormModel.Rules.Add(new DynamicFormModelRule(currentItem.Rule.Effect,
                        new DynamicFormModelRuleCondition(currentItem.Rule.Condition.Scope, currentItem.Rule.Condition.Schema)));
                    }

                    result.Add(horizontalDynamicFormModel);
                    break;
                case LayoutType.VerticalLayout:
                    var verticalLayout = item as VerticalLayoutDescriptionItem;
                    var verticalDynamicFormModel = new DynamicFormModel
                    {
                        DynamicType = new ComponentSelectionKey(TypeName.VerticalLayout),
                        SubElements = GenerateModelsInternal(verticalLayout.Elements, new List<DynamicFormModel>()),
                        Rules = new List<DynamicFormModelRule> { new DynamicFormModelRule(verticalLayout.Rule.Effect,
                        new DynamicFormModelRuleCondition(verticalLayout.Rule.Condition.Scope, verticalLayout.Rule.Condition.Schema)) }
                    };
                    result.Add(verticalDynamicFormModel);

                    if (verticalLayout.Rule != null)
                    {
                        verticalDynamicFormModel.Rules.Add(new DynamicFormModelRule(verticalLayout.Rule.Effect,
                        new DynamicFormModelRuleCondition(verticalLayout.Rule.Condition.Scope, verticalLayout.Rule.Condition.Schema)));
                    }
                    break;
                case LayoutType.Control:
                    var controlItem = item as ControlDescriptionItem;
                    var controlOptions = ControlOptions.Default();
                    // controlItem.ScopeMetadata.Add("Label");

                    var dynamicFormModel = new DynamicFormModel
                    {
                        DynamicType = new ComponentSelectionKey(Enum.Parse<TypeName>(controlItem.ScopeMetadata["Type"].ToString()), controlOptions.Format),
                        Parameters = new ParameterList(controlItem.ScopeMetadata),
                        DynamicFormModelOptions = new DynamicFormModelOptions()
                        {
                            Label = controlItem.ShowLabel == false ? string.Empty : controlItem.Label,
                            Readonly = controlOptions.Readonly,
                            ShowSortButtons = controlOptions.ShowSortButtons,
                            ElementLabelProperty = controlOptions.ElementLabelProperty
                        },

                    };

                    if (controlItem.Rule != null)
                    {
                        dynamicFormModel.Rules = new List<DynamicFormModelRule>{ new DynamicFormModelRule(controlItem.Rule.Effect,
                        new DynamicFormModelRuleCondition(controlItem.Rule.Condition.Scope, controlItem.Rule.Condition.Schema)) };
                    }

                    result.Add(dynamicFormModel);
                    break;
                case LayoutType.Categorization:
                    var categorization = item as CategorizationDescriptionItem;

                    var categorizationDynamic = new DynamicFormModel
                    {
                        DynamicType = new ComponentSelectionKey(TypeName.Categorization),
                        SubElements = GenerateModelsInternal(categorization.Elements, new List<DynamicFormModel>()),
                        Rules = new List<DynamicFormModelRule> { new DynamicFormModelRule(categorization.Rule.Effect,
                        new DynamicFormModelRuleCondition(categorization.Rule.Condition.Scope, categorization.Rule.Condition.Schema)) }
                    };
                    if (categorization.Rule != null)
                    {
                        categorizationDynamic.Rules.Add(new DynamicFormModelRule(categorization.Rule.Effect,
                        new DynamicFormModelRuleCondition(categorization.Rule.Condition.Scope, categorization.Rule.Condition.Schema)));
                    }

                    result.Add(categorizationDynamic);
                    break;
                case LayoutType.Category:
                    var category = item as CategoryDescriptionItem;
                    var categoryDynamicFormModel = new DynamicFormModel
                    {
                        DynamicType = new ComponentSelectionKey(TypeName.Category),
                        SubElements = GenerateModelsInternal(category.Elements, new List<DynamicFormModel>()),
                        DynamicFormModelOptions = new DynamicFormModelOptions()
                        {
                            Label = category.Label
                        }
                    };
                    if (category.Rule != null)
                    {
                        categoryDynamicFormModel.Rules.Add(new DynamicFormModelRule(category.Rule.Effect,
                        new DynamicFormModelRuleCondition(category.Rule.Condition.Scope, category.Rule.Condition.Schema)));
                    }
                    result.Add(categoryDynamicFormModel);

                    break;
                case LayoutType.Group:
                    var group = item as GroupDescriptionItem;

                    var groupDynamicFormModel = new DynamicFormModel
                    {
                        DynamicType = new ComponentSelectionKey(TypeName.GroupLayout),
                        SubElements = GenerateModelsInternal(group.Elements, new List<DynamicFormModel>()),
                        DynamicFormModelOptions = new DynamicFormModelOptions()
                        {
                            Label = group.Label
                        },
                        Rules = new List<DynamicFormModelRule>{ new DynamicFormModelRule(group.Rule.Effect,
                        new DynamicFormModelRuleCondition(group.Rule.Condition.Scope, group.Rule.Condition.Schema)) }
                    };

                    if (group.Rule != null)
                    {
                        groupDynamicFormModel.Rules.Add(new DynamicFormModelRule(group.Rule.Effect,
                        new DynamicFormModelRuleCondition(group.Rule.Condition.Scope, group.Rule.Condition.Schema)));
                    }

                    result.Add(groupDynamicFormModel);

                    break;
                default:
                    return result;
            }
        }
        return result;
    }
}