using BlazorDynamics.Common.Enums;
using BlazorDynamics.Common.Models;
using BlazorDynamics.Core.Models;
using BlazorDynamics.UISchema.Contracts;
using BlazorDynamics.UISchema.Enums;
using BlazorDynamics.UISchema.Models;
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
                    HandleHorizontalLayout(result, item);
                    break;
                case LayoutType.VerticalLayout:
                    var verticalLayout = item as VerticalLayoutDescriptionItem;
                    HandleVerticalLayout(result, verticalLayout);
                    break;
                case LayoutType.Control:
                    HandleControl(result, item);
                    break;
                case LayoutType.Categorization:
                    HandleCategorization(result, item);
                    break;
                case LayoutType.Category:
                    HandleCategory(result, item);
                    break;
                case LayoutType.Group:
                    HandleGroup(result, item);
                    break;
                default:
                    return result;
            }
        }
        return result;
    }

    private void HandleGroup(List<DynamicFormModel> result, ILayoutDescriptionItem item)
    {
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
        HandleItemRules(group, groupDynamicFormModel);
        result.Add(groupDynamicFormModel);
    }

    private static void HandleItemRules(ILayoutDescriptionItem? layoutItem, DynamicFormModel groupDynamicFormModel)
    {
        if (layoutItem.Rule != null)
        {
            groupDynamicFormModel.Rules.Add(new DynamicFormModelRule(layoutItem.Rule.Effect,
            new DynamicFormModelRuleCondition(layoutItem.Rule.Condition.Scope, layoutItem.Rule.Condition.Schema)));
        }
    }

    private void HandleCategory(List<DynamicFormModel> result, ILayoutDescriptionItem item)
    {
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
        HandleItemRules(category, categoryDynamicFormModel);
        result.Add(categoryDynamicFormModel);
    }

    private void HandleCategorization(List<DynamicFormModel> result, ILayoutDescriptionItem item)
    {
        var categorization = item as CategorizationDescriptionItem;

        var categorizationDynamic = new DynamicFormModel
        {
            DynamicType = new ComponentSelectionKey(TypeName.Categorization),
            SubElements = GenerateModelsInternal(categorization.Elements, new List<DynamicFormModel>()),
            Rules = new List<DynamicFormModelRule> { new DynamicFormModelRule(categorization.Rule.Effect,
                        new DynamicFormModelRuleCondition(categorization.Rule.Condition.Scope, categorization.Rule.Condition.Schema)) }
        };

        HandleItemRules(categorization, categorizationDynamic);
        result.Add(categorizationDynamic);
    }

    private static void HandleControl(List<DynamicFormModel> result, ILayoutDescriptionItem item)
    {
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
        HandleItemRules(controlItem, dynamicFormModel);
        result.Add(dynamicFormModel);
    }

    private void HandleVerticalLayout(List<DynamicFormModel> result, VerticalLayoutDescriptionItem? verticalLayout)
    {
        if(verticalLayout == null) { return; }
        var dynamicFormModel = new DynamicFormModel
        {
            DynamicType = new ComponentSelectionKey(TypeName.VerticalLayout),
            SubElements = GenerateModelsInternal(verticalLayout.Elements, new List<DynamicFormModel>()),
            Rules = new List<DynamicFormModelRule> { new DynamicFormModelRule(verticalLayout.Rule?.Effect,
                        new DynamicFormModelRuleCondition(verticalLayout.Rule.Condition.Scope, verticalLayout.Rule.Condition.Schema)) }
        };
        HandleItemRules(verticalLayout,dynamicFormModel);
        result.Add(dynamicFormModel);
     
    }

    private void HandleHorizontalLayout(List<DynamicFormModel> result, ILayoutDescriptionItem item)
    {
        var currentItem = item as HorizontalLayoutDescriptionItem;

        var dynamicFormModel = new DynamicFormModel
        {
            DynamicType = new ComponentSelectionKey(TypeName.HorizontalLayout),
            SubElements = GenerateModelsInternal(currentItem.Elements, new List<DynamicFormModel>()),
        };

        HandleItemRules(item,dynamicFormModel);

        result.Add(dynamicFormModel);
    }
}