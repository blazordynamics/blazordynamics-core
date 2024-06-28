using BlazorDynamics.Forms.Components.Builders;
using BlazorDynamics.Common.Enums;
using Newtonsoft.Json.Linq;
using BlazorDynamics.Core.Models.ParameterModels;
using BlazorDynamics.Core.Models;

namespace BlazorDynamics.Forms.Components.Factories
{
    public static class FormFactory
    {
        // Vertical Layout Methods
        public static DynamicFormModel VerticalLayout()
        {
            return new DynamicFormModel(TypeName.VerticalLayout);
        }

        public static DynamicFormModel VerticalLayout(string variationName)
        {
            return new DynamicFormModel(TypeName.VerticalLayout, variationName);
        }

        public static DynamicFormModel VerticalLayout(params DynamicFormModel[] elements)
        {
            return new DynamicFormModel()
            {
                DynamicType = new ComponentSelectionKey(TypeName.VerticalLayout),
                SubElements = elements.ToList()
            };
        }

        public static VerticalLayoutBuilder VerticalLayoutBuilder()
        {
            return new VerticalLayoutBuilder();
        }

        public static DynamicFormModel VerticalLayout(Action<VerticalLayoutBuilder>? configure)
        {
            var builder = new VerticalLayoutBuilder();
            if (configure != null) { configure(builder); }
            return builder.Build();
        }

        // horizontallayout

        public static DynamicFormModel HorizontalLayout()
        {
            return new DynamicFormModel(TypeName.HorizontalLayout);
        }

        public static DynamicFormModel HorizontalLayout(string variationName)
        {
            return new DynamicFormModel(TypeName.HorizontalLayout, variationName);
        }

        public static DynamicFormModel HorizontalLayout(Action<HorizontalLayoutBuilder>? configure)
        {
            var builder = new HorizontalLayoutBuilder();
            if (configure != null) { configure(builder); }
            return builder.Build();
        }

        public static HorizontalLayoutBuilder HorizontalLayoutBuilder()
        {
            return new HorizontalLayoutBuilder();
        }

        public static DynamicFormModel HorizontalLayout(params DynamicFormModel[] elements)
        {
            return new DynamicFormModel()
            {
                DynamicType = new ComponentSelectionKey(TypeName.HorizontalLayout),
                SubElements = elements.ToList()
            };
        }


        // Group Layout Methods
        public static DynamicFormModel GroupLayout()
        {
            return new DynamicFormModel(TypeName.GroupLayout);
        }

        public static DynamicFormModel GroupLayout(string variationName)
        {
            return new DynamicFormModel(TypeName.GroupLayout, variationName);
        }

        public static DynamicFormModel GroupLayout(params DynamicFormModel[] elements)
        {
            return new DynamicFormModel()
            {
                DynamicType = new ComponentSelectionKey(TypeName.GroupLayout),
                SubElements = elements.ToList()
            };
        }

        public static DynamicFormModel GroupLayout(Action<GroupLayoutBuilder>? configure)
        {
            var builder = new GroupLayoutBuilder();
            if (configure != null) { configure(builder); }
            return builder.Build();
        }

        public static DynamicFormModel GroupLayout(Action<GroupLayoutBuilder>? configure, params DynamicFormModel[] elements)
        {
            var builder = new GroupLayoutBuilder();
            if (configure != null) { configure(builder); }
            var result = builder.Build();
            result.SubElements = elements.ToList();
            return result;
        }

        public static GroupLayoutBuilder GroupLayoutBuilder()
        {
            return new GroupLayoutBuilder();
        }


        // extention
        public static DynamicFormModel WithVariationName(this DynamicFormModel model, string variationName)
        {
            model.DynamicType = new ComponentSelectionKey(model.DynamicType.TypeName, variationName);
            return model;
        }
        public static DynamicFormModel AddSchemaRule(this DynamicFormModel model, RuleEffect ruleEffect, string scope, string jsonSchema)
        {
            return model.AddSchemaRule(ruleEffect, scope, JObject.Parse(jsonSchema));
        }

        public static DynamicFormModel AddSchemaRule(this DynamicFormModel model, RuleEffect ruleEffect, string scope, JToken jsonSchema)
        {
            if(model.Rules == null) { model.Rules = new List<DynamicFormModelRule>();  }
            model.Rules.Add(new DynamicFormModelRule(ruleEffect, new DynamicFormModelRuleCondition(scope, jsonSchema)));
            return model;
        }

        // Counter component

        public static DynamicFormModel NumberDisplay(string label, string path)
        {
            return new NumberDisplayBuilder()
                .WithLabel(label)
                .WithPath(path)
                .Build();
        }

        public static DynamicFormModel TemplateDisplay(string variationName, string path)
        {
            return new TemplateDisplayBuider()
                .WithTypeDefinition(variationName)
                .WithPath(path)
                .Build();
        }

        public static DynamicFormModel StringDisplay(string label, string path)
        {
            return new StringDisplayBuilder()
                .WithLabel(label)
                .WithPath(path)
                .Build();
        }

        public static DynamicFormModel StringDisplay(string path)
        {
            return new StringDisplayBuilder()
                .WithPath(path)
                .Build();
        }


        // String Component Methods
        public static DynamicFormModel StringComponent()
        {
            return new DynamicFormModel(TypeName.String);
        }

        public static DynamicFormModel StringComponent(string variationName)
        {
            return new DynamicFormModel(TypeName.String, variationName);
        }

        public static DynamicFormModel StringComponent(string label, string path, Action<StringComponentBuilder>? configure = null)
        {
            var builder = new StringComponentBuilder();
            if (configure != null) { configure(builder); }
            builder.WithPath(path);
            builder.WithLabel(label);
            return builder.Build();
        }

        public static StringComponentBuilder StringComponentBuilder(string path)
        {
            return new StringComponentBuilder().WithPath(path);
        }

        // Boolean Component Methods
        public static DynamicFormModel BooleanComponent()
        {
            return new DynamicFormModel(TypeName.Boolean);
        }

        public static DynamicFormModel BooleanComponent(string variationName)
        {
            return new DynamicFormModel(TypeName.Boolean, variationName);
        }

        public static DynamicFormModel BoolComponent(string label, string path)
        {
            return new DynamicFormModel()
            {
                DynamicType = new ComponentSelectionKey(TypeName.Boolean),
                Parameters = BooleanParameters.Set(label, path)
            };
        }

        public static DynamicFormModel BoolComponent(string variationName, string label, string path)
        {
            return new DynamicFormModel()
            {
                DynamicType = new ComponentSelectionKey(TypeName.Boolean, variationName),
                Parameters = BooleanParameters.Set(label, path)
            };
        }

        public static DynamicFormModel BooleanComponent(string label, string path, Action<BooleanComponentBuilder>? configure = null)
        {
            var builder = new BooleanComponentBuilder();
            if (configure != null) { configure(builder); }
            builder.WithPath(path);
            builder.WithLabel(label);
            return builder.Build();
        }


        public static BooleanComponentBuilder BooleanComponentBuilder()
        {
            return new BooleanComponentBuilder();
        }

        // DateTime Component Methods
        public static DynamicFormModel DateTimeComponent()
        {
            return new DynamicFormModel(TypeName.DateTime);
        }

        public static DynamicFormModel DateTimeComponent(string variationName)
        {
            return new DynamicFormModel(TypeName.DateTime, variationName);
        }
              
        public static DynamicFormModel DateTimeComponent(string label, string path, Action<DateTimeComponentBuilder>? configure = null)
        {
            var builder = new DateTimeComponentBuilder();
            if (configure != null) { configure(builder); }
            builder.WithPath(path);
            builder.WithLabel(label);
            return builder.Build();
        }

        public static DateTimeComponentBuilder DateTimeComponentBuilder()
        {
            return new DateTimeComponentBuilder();
        }

        // Number Component Methods
        public static DynamicFormModel NumberComponent()
        {
            return new DynamicFormModel(TypeName.Number);
        }

        public static DynamicFormModel NumberComponent(string variationName)
        {
            return new DynamicFormModel(TypeName.Number, variationName);
        }

        public static DynamicFormModel NumberComponent(string label, string path)
        {
            return new DynamicFormModel()
            {
                DynamicType = new ComponentSelectionKey(TypeName.Number),
                Parameters = BaseParameters.Set(label, path)
            };
        }

        public static DynamicFormModel NumberComponent(string label, string path, Action<NumberComponentBuilder>? configure)
        {
            var builder = new NumberComponentBuilder();
            if (configure != null) { configure(builder); }
            builder.WithPath(path);
            builder.WithLabel(label);
            return builder.Build();
        }

        public static NumberComponentBuilder NumberComponentBuilder()
        {
            return new NumberComponentBuilder();
        }

        // Int Component Methods
        public static DynamicFormModel IntComponent()
        {
            return new DynamicFormModel(TypeName.Int);
        }

        public static DynamicFormModel IntComponent(string variationName)
        {
            return new DynamicFormModel(TypeName.Int, variationName);
        }

        public static DynamicFormModel IntComponent(string variationName, string label, string path)
        {
            return new DynamicFormModel()
            {
                DynamicType = new ComponentSelectionKey(TypeName.Int, variationName),
                Parameters = BaseParameters.Set(label, path)
            };
        }

        public static DynamicFormModel IntComponent(string label, string path, Action<IntComponentBuilder>? configure = null)
        {
            var builder = new IntComponentBuilder();
            if (configure != null) { configure(builder); }
            builder.WithPath(path);
            builder.WithLabel(label);
            return builder.Build();
        }


        public static IntComponentBuilder IntComponentBuilder()
        {
            return new IntComponentBuilder();
        }

        // List Component Methods
        public static DynamicFormModel ListComponent()
        {
            return new DynamicFormModel(TypeName.List);
        }

        public static DynamicFormModel ListComponent(string variationName)
        {
            return new DynamicFormModel(TypeName.List, variationName);
        }

        public static DynamicFormModel ListComponent(string label, string path, params DynamicFormModel[] elements)
        {
            return new DynamicFormModel()
            {
                DynamicType = new ComponentSelectionKey(TypeName.List),
                Parameters = BaseParameters.Set(label, path),
                SubElements = elements.ToList()
            };
        }

        public static DynamicFormModel ListComponent(string label, string path, Action<ListComponentBuilder>? configure = null)
        {
            var builder = new ListComponentBuilder();
            if (configure != null) { configure(builder); }
            builder.WithPath(path);
            builder.WithLabel(label);
            return builder.Build();
        }

        public static ListComponentBuilder ListComponentBuilder()
        {
            return new ListComponentBuilder();
        }

        // Dropdown Component Methods
        public static DynamicFormModel DropDownComponent()
        {
            return new DynamicFormModel(TypeName.Dropdown);
        }

        public static DynamicFormModel DropDownComponent(string variationName)
        {
            return new DynamicFormModel(TypeName.Dropdown, variationName);
        }

        public static DynamicFormModel DropDownComponent(string label, string path, Dictionary<object, string> options)
        {
            return new DynamicFormModel()
            {
                DynamicType = new ComponentSelectionKey(TypeName.Dropdown),
                Parameters = DropdownParameters.Set(label, options, path)
            };
        }

        public static DynamicFormModel DropDownComponent(string label, Dictionary<object, string> options, string path, Action<DropDownComponentBuilder>? configure = null)
        {
            var builder = new DropDownComponentBuilder();
            if (configure != null) { configure(builder); }
            builder.WithPath(path);
            builder.WithLabel(label);
            builder.WithOptions(options);
            return builder.Build();
        }

        public static DropDownComponentBuilder DropDownComponentBuilder()
        {
            return new DropDownComponentBuilder();
        }

        // Action Component Methods
        public static DynamicFormModel DeleteAction(string label, string path)
        {
            return new DynamicFormModel()
            {
                DynamicType = new ComponentSelectionKey(TypeName.DeleteAction),
                Parameters = DeleteActionParameters.Set(label, path)
            };
        }

        public static DynamicFormModel AddAction(string label, string path, object defaultValue)
        {
            return new DynamicFormModel()
            {
                DynamicType = new ComponentSelectionKey(TypeName.AddAction),
                Parameters = AddActionParameters.Set(label, path, defaultValue)
            };
        }

        public static DynamicFormModel SubmitAction(string label)
        {
            return new DynamicFormModel()
            {
                DynamicType = new ComponentSelectionKey(TypeName.SubmitAction),
                Parameters = SubmitActionParameters.Set(label)
            };
        }
    }
}
