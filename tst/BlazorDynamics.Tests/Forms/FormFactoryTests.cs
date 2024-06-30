using BlazorDynamics.Common.Enums;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Core.Models.ParameterModels;
using BlazorDynamics.Forms.Components.Builders;
using BlazorDynamics.Forms.Components.Factories;
using Newtonsoft.Json.Linq;

namespace BlazorDynamics.Tests.Forms
{
    [TestFixture]
    public class FormFactoryTests
    {
        [Test]
        public void VerticalLayout_NoParams_ReturnsCorrectModel()
        {
            var model = FormFactory.VerticalLayout();

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.VerticalLayout));
        }

        [Test]
        public void BooleanComponent_NoParams_ReturnsCorrectModel()
        {
            // Act
            var model = FormFactory.BooleanComponent();

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Boolean));
        }

        [Test]
        public void StringComponentBuilder_WithPath_ReturnsBuilderWithCorrectPath()
        {
            // Arrange
            string path = "TestPath";

            // Act
            var builder = FormFactory.StringComponentBuilder(path);

            // Assert
            Assert.That(builder, Is.Not.Null);
            Assert.That(builder, Is.TypeOf<StringComponentBuilder>());
            var model = builder.Build();
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void StringComponent_WithLabelPathAndConfigure_ReturnsCorrectModel()
        {
            // Arrange
            string label = "TestLabel";
            string path = "TestPath";

            // Act
            var model = FormFactory.StringComponent(label, path, builder =>
            {
                builder.WithPath(label);
                builder.WithLabel(path);
            });

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.String));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void BooleanComponentBuilder_ReturnsNewInstance()
        {
            // Act
            var builder = FormFactory.BooleanComponentBuilder();

            // Assert
            Assert.That(builder, Is.Not.Null);
            Assert.That(builder, Is.TypeOf<BooleanComponentBuilder>());
        }

        [Test]
        public void DateTimeComponent_WithLabelPathAndConfigure_ReturnsCorrectModel()
        {
            // Arrange
            string label = "TestLabel";
            string path = "TestPath";

            // Act
            var model = FormFactory.DateTimeComponent(label, path, builder =>
            {
                builder.WithFormat("MM/dd/yyyy");
            });

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.DateTime));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void HorizontalLayoutBuilder_ReturnsNewInstance()
        {
            // Act
            var builder = FormFactory.HorizontalLayoutBuilder();

            // Assert
            Assert.That(builder, Is.Not.Null);
            Assert.That(builder, Is.TypeOf<HorizontalLayoutBuilder>());
        }

        [Test]
        public void NumberComponent_NoParams_ReturnsCorrectModel()
        {
            // Act
            var model = FormFactory.NumberComponent();

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Number));
        }

        [Test]
        public void NumberComponent_WithLabelPathAndConfigure_ReturnsCorrectModel()
        {
            // Arrange
            string label = "TestLabel";
            string path = "TestPath";

            // Act
            var model = FormFactory.NumberComponent(label, path, builder =>
            {
                builder.WithMinimum(1).WithMaximum(100);
            });

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Number));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
            Assert.That(model.Parameters[ParameterNames.Minimum], Is.EqualTo(1));
            Assert.That(model.Parameters[ParameterNames.Maximum], Is.EqualTo(100));
        }

        [Test]
        public void IntComponent_WithLabelPathAndConfigure_ReturnsCorrectModel()
        {
            // Arrange
            string label = "TestLabel";
            string path = "TestPath";

            // Act
            var model = FormFactory.IntComponent(label, path, builder =>
            {
                builder.WithMinimum(1).WithMaximum(100);
            });

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Int));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
            Assert.That(model.Parameters[ParameterNames.Minimum], Is.EqualTo(1));
            Assert.That(model.Parameters[ParameterNames.Maximum], Is.EqualTo(100));
        }

        [Test]
        public void ListComponent_WithLabelPathAndConfigure_ReturnsCorrectModel()
        {
            // Arrange
            string label = "TestLabel";
            string path = "TestPath";

            // Act
            var model = FormFactory.ListComponent(label, path, builder => { });

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.List));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void DropDownComponent_WithLabelPathAndConfigure_ReturnsCorrectModel()
        {
            // Arrange
            string label = "TestLabel";
            string path = "TestPath";
            var options = new Dictionary<object, string>
        {
            { 1, "Option 1" },
            { 2, "Option 2" }
        };

            // Act
            var model = FormFactory.DropDownComponent(label, options, path, builder => { });

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Dropdown));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
            Assert.That(model.Parameters[ParameterNames.Options], Is.EqualTo(options));
        }

        [Test]
        public void DropDownComponentBuilder_ReturnsNewInstance()
        {
            // Act
            var builder = FormFactory.DropDownComponentBuilder();

            // Assert
            Assert.That(builder, Is.Not.Null);
            Assert.That(builder, Is.TypeOf<DropDownComponentBuilder>());
        }

        [Test]
        public void DeleteAction_WithLabelAndPath_ReturnsCorrectModel()
        {
            // Arrange
            string label = "TestLabel";
            string path = "TestPath";

            // Act
            var model = FormFactory.DeleteAction(label, path);

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.DeleteAction));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void ListComponentBuilder_ReturnsNewInstance()
        {
            // Act
            var builder = FormFactory.ListComponentBuilder();

            // Assert
            Assert.That(builder, Is.Not.Null);
            Assert.That(builder, Is.TypeOf<ListComponentBuilder>());
        }

        [Test]
        public void DropDownComponent_NoParams_ReturnsCorrectModel()
        {
            // Act
            var model = FormFactory.DropDownComponent();

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Dropdown));
        }

        [Test]
        public void DropDownComponent_WithVariationName_ReturnsCorrectModel()
        {
            // Arrange
            string variationName = "TestVariation";

            // Act
            var model = FormFactory.DropDownComponent(variationName);

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Dropdown));
            Assert.That(model.DynamicType.VariationName, Is.EqualTo(variationName));
        }

        [Test]
        public void IntComponentBuilder_ReturnsNewInstance()
        {
            // Act
            var builder = FormFactory.IntComponentBuilder();

            // Assert
            Assert.That(builder, Is.Not.Null);
            Assert.That(builder, Is.TypeOf<IntComponentBuilder>());
        }

        [Test]
        public void ListComponent_NoParams_ReturnsCorrectModel()
        {
            // Act
            var model = FormFactory.ListComponent();

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.List));
        }

        [Test]
        public void ListComponent_WithVariationName_ReturnsCorrectModel()
        {
            // Arrange
            string variationName = "TestVariation";

            // Act
            var model = FormFactory.ListComponent(variationName);

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.List));
            Assert.That(model.DynamicType.VariationName, Is.EqualTo(variationName));
        }

        [Test]
        public void NumberComponentBuilder_ReturnsNewInstance()
        {
            // Act
            var builder = FormFactory.NumberComponentBuilder();

            // Assert
            Assert.That(builder, Is.Not.Null);
            Assert.That(builder, Is.TypeOf<NumberComponentBuilder>());
        }

        [Test]
        public void IntComponent_WithVariationName_ReturnsCorrectModel()
        {
            // Arrange
            string variationName = "TestVariation";

            // Act
            var model = FormFactory.IntComponent(variationName);

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Int));
            Assert.That(model.DynamicType.VariationName, Is.EqualTo(variationName));
        }

        [Test]
        public void IntComponent_NoParams_ReturnsCorrectModel()
        {
            // Act
            var model = FormFactory.IntComponent();

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Int));
        }

        [Test]
        public void NumberComponent_WithVariationName_ReturnsCorrectModel()
        {
            // Arrange
            string variationName = "TestVariation";

            // Act
            var model = FormFactory.NumberComponent(variationName);

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Number));
            Assert.That(model.DynamicType.VariationName, Is.EqualTo(variationName));
        }

        [Test]
        public void DateTimeComponentBuilder_ReturnsNewInstance()
        {
            // Act
            var builder = FormFactory.DateTimeComponentBuilder();

            // Assert
            Assert.That(builder, Is.Not.Null);
            Assert.That(builder, Is.TypeOf<DateTimeComponentBuilder>());
        }

        [Test]
        public void AddSchemaRule_WithStringJsonSchema_AddsRuleToModel()
        {
            // Arrange
            var model = new DynamicFormModel(TypeName.String);
            string scope = "TestScope";
            string jsonSchema = "{\"type\": \"string\"}";
            RuleEffect ruleEffect = RuleEffect.SHOW;

            // Act
            var result = model.AddSchemaRule(ruleEffect, scope, jsonSchema);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Rules.Count, Is.EqualTo(1));
            Assert.That(result.Rules.First().Effect, Is.EqualTo(ruleEffect));
            Assert.That(result.Rules.First().Condition.Scope, Is.EqualTo(scope));
            Assert.That(result.Rules.First().Condition.Schema.ToString(), Is.EqualTo(JObject.Parse(jsonSchema).ToString()));
        }

        [Test]
        public void GroupLayoutBuilder_ReturnsNewInstance()
        {
            // Act
            var builder = FormFactory.GroupLayoutBuilder();

            // Assert
            Assert.That(builder, Is.Not.Null);
            Assert.That(builder, Is.TypeOf<GroupLayoutBuilder>());
        }

        [Test]
        public void BoolComponent_WithVariationNameLabelAndPath_ReturnsCorrectModel()
        {
            // Arrange
            string variationName = "TestVariation";
            string label = "TestLabel";
            string path = "TestPath";

            // Act
            var model = FormFactory.BoolComponent(variationName, label, path);

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Boolean));
            Assert.That(model.DynamicType.VariationName, Is.EqualTo(variationName));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void BoolComponent_WithLabelAndPath_ReturnsCorrectModel()
        {
            // Arrange
            string label = "TestLabel";
            string path = "TestPath";

            // Act
            var model = FormFactory.BoolComponent(label, path);

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Boolean));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void StringComponent_WithVariationName_ReturnsCorrectModel()
        {
            // Arrange
            string variationName = "TestVariation";

            // Act
            var model = FormFactory.StringComponent(variationName);

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.String));
            Assert.That(model.DynamicType.VariationName, Is.EqualTo(variationName));
        }

        [Test]
        public void StringComponent_NoParams_ReturnsCorrectModel()
        {
            // Act
            var model = FormFactory.StringComponent();

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.String));
        }

        [Test]
        public void StringDisplay_WithPath_ReturnsCorrectModel()
        {
            // Arrange
            string path = "TestPath";

            // Act
            var model = FormFactory.StringDisplay(path);

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.StringDisplay));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void StringDisplay_WithLabelAndPath_ReturnsCorrectModel()
        {
            // Arrange
            string label = "TestLabel";
            string path = "TestPath";

            // Act
            var model = FormFactory.StringDisplay(label, path);

            // Assert
            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.StringDisplay));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void VerticalLayout_WithVariationName_ReturnsCorrectModel()
        {
            string variationName = "TestVariation";
            var model = FormFactory.VerticalLayout(variationName);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.VerticalLayout));
            Assert.That(model.DynamicType.VariationName, Is.EqualTo(variationName));
        }

        [Test]
        public void VerticalLayout_WithElements_ReturnsCorrectModel()
        {
            var elements = new DynamicFormModel[] {
            new DynamicFormModel(TypeName.String),
            new DynamicFormModel(TypeName.Boolean)
        };

            var model = FormFactory.VerticalLayout(elements);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.VerticalLayout));
            Assert.That(model.SubElements.Count, Is.EqualTo(elements.Length));
            Assert.That(model.SubElements, Contains.Item(elements[0]));
            Assert.That(model.SubElements, Contains.Item(elements[1]));
        }

        [Test]
        public void VerticalLayout_WithBuilder_ReturnsCorrectModel()
        {
            var model = FormFactory.VerticalLayout(builder =>
            {
                builder.WithSubElement(FormFactory.StringComponent("Label", "Path"));
            });

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.VerticalLayout));
            Assert.That(model.SubElements.Count, Is.EqualTo(1));
            Assert.That(model.SubElements.First().DynamicType.TypeName, Is.EqualTo(TypeName.String));
        }

        // New test for VerticalLayoutBuilder
        [Test]
        public void VerticalLayoutBuilder_ReturnsNewInstance()
        {
            var builder = FormFactory.VerticalLayoutBuilder();

            Assert.That(builder, Is.Not.Null);
            Assert.That(builder, Is.TypeOf<VerticalLayoutBuilder>());
        }

        // HorizontalLayout tests
        [Test]
        public void HorizontalLayout_NoParams_ReturnsCorrectModel()
        {
            var model = FormFactory.HorizontalLayout();

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.HorizontalLayout));
        }

        [Test]
        public void HorizontalLayout_WithVariationName_ReturnsCorrectModel()
        {
            string variationName = "TestVariation";
            var model = FormFactory.HorizontalLayout(variationName);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.HorizontalLayout));
            Assert.That(model.DynamicType.VariationName, Is.EqualTo(variationName));
        }

        [Test]
        public void HorizontalLayout_WithElements_ReturnsCorrectModel()
        {
            var elements = new DynamicFormModel[] {
            new DynamicFormModel(TypeName.String),
            new DynamicFormModel(TypeName.Boolean)
        };

            var model = FormFactory.HorizontalLayout(elements);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.HorizontalLayout));
            Assert.That(model.SubElements.Count, Is.EqualTo(elements.Length));
            Assert.That(model.SubElements, Contains.Item(elements[0]));
            Assert.That(model.SubElements, Contains.Item(elements[1]));
        }

        [Test]
        public void HorizontalLayout_WithBuilder_ReturnsCorrectModel()
        {
            var model = FormFactory.HorizontalLayout(builder =>
            {
                builder.WithSubElement(FormFactory.StringComponent("Label", "Path"));
            });

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.HorizontalLayout));
            Assert.That(model.SubElements.Count, Is.EqualTo(1));
            Assert.That(model.SubElements.First().DynamicType.TypeName, Is.EqualTo(TypeName.String));
        }

        // GroupLayout tests
        [Test]
        public void GroupLayout_NoParams_ReturnsCorrectModel()
        {
            var model = FormFactory.GroupLayout();

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.GroupLayout));
        }

        [Test]
        public void GroupLayout_WithVariationName_ReturnsCorrectModel()
        {
            string variationName = "TestVariation";
            var model = FormFactory.GroupLayout(variationName);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.GroupLayout));
            Assert.That(model.DynamicType.VariationName, Is.EqualTo(variationName));
        }

        [Test]
        public void GroupLayout_WithElements_ReturnsCorrectModel()
        {
            var elements = new DynamicFormModel[] {
            new DynamicFormModel(TypeName.String),
            new DynamicFormModel(TypeName.Boolean)
        };

            var model = FormFactory.GroupLayout(elements);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.GroupLayout));
            Assert.That(model.SubElements.Count, Is.EqualTo(elements.Length));
            Assert.That(model.SubElements, Contains.Item(elements[0]));
            Assert.That(model.SubElements, Contains.Item(elements[1]));
        }

        [Test]
        public void GroupLayout_WithBuilder_ReturnsCorrectModel()
        {
            var model = FormFactory.GroupLayout(builder =>
            {
                builder.WithSubElement(FormFactory.StringComponent("Label", "Path"));
            });

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.GroupLayout));
            Assert.That(model.SubElements.Count, Is.EqualTo(1));
            Assert.That(model.SubElements.First().DynamicType.TypeName, Is.EqualTo(TypeName.String));
        }

        // Component tests
        [Test]
        public void StringComponent_WithLabelAndPath_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var model = FormFactory.StringComponent(label, path);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.String));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void BooleanComponent_WithLabelAndPath_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var model = FormFactory.BooleanComponent(label, path);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Boolean));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void WithVariationName_AddsVariationNameToModel()
        {
            var model = new DynamicFormModel(TypeName.String);
            string variationName = "NewVariation";

            var result = model.WithVariationName(variationName);

            Assert.That(result.DynamicType.VariationName, Is.EqualTo(variationName));
        }

        [Test]
        public void AddSchemaRule_AddsRuleToModel()
        {
            var model = new DynamicFormModel(TypeName.String);
            string scope = "TestScope";
            var jsonSchema = JObject.Parse("{\"type\": \"string\"}");

            var result = model.AddSchemaRule(RuleEffect.SHOW, scope, jsonSchema);

            Assert.That(result.Rules.Count, Is.EqualTo(1));
            Assert.That(result.Rules.First().Effect, Is.EqualTo(RuleEffect.SHOW));
            Assert.That(result.Rules.First().Condition.Scope, Is.EqualTo(scope));
            Assert.That(result.Rules.First().Condition.Schema, Is.EqualTo(jsonSchema));
        }

        [Test]
        public void NumberDisplay_WithLabelAndPath_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var model = FormFactory.NumberDisplay(label, path);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.NumberDisplay));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void Number_WithLabelAndPath_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var model = FormFactory.NumberComponent(label, path);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Number));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void TemplateDisplay_WithVariationNameAndPath_ReturnsCorrectModel()
        {
            string variationName = "TemplateVariation";
            string path = "TestPath";
            var model = FormFactory.TemplateDisplay(variationName, path);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.TemplateContent));
            Assert.That(model.DynamicType.VariationName, Is.EqualTo(variationName));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        // Additional component tests
        [Test]
        public void DropDownComponent_WithLabelPathAndOptions_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var options = new Dictionary<object, string>
        {
            { 1, "Option 1" },
            { 2, "Option 2" }
        };

            var model = FormFactory.DropDownComponent(label, path, options);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Dropdown));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
            Assert.That(model.Parameters[ParameterNames.Options], Is.EqualTo(options));
        }

        [Test]
        public void ListComponent_WithLabelPathAndElements_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var elements = new DynamicFormModel[]
            {
            FormFactory.StringComponent("Label1", "Path1"),
            FormFactory.NumberComponent("Label2", "Path2")
            };

            var model = FormFactory.ListComponent(label, path, elements);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.List));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
            Assert.That(model.SubElements.Count, Is.EqualTo(elements.Length));
            Assert.That(model.SubElements, Contains.Item(elements[0]));
            Assert.That(model.SubElements, Contains.Item(elements[1]));
        }

        [Test]
        public void AddAction_WithLabelPathAndDefaultValue_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            object defaultValue = 123;

            var model = FormFactory.AddAction(label, path, defaultValue);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.AddAction));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
            Assert.That(model.Parameters["DefaultValue"], Is.EqualTo(defaultValue));
        }

        [Test]
        public void SubmitAction_WithLabel_ReturnsCorrectModel()
        {
            string label = "TestLabel";

            var model = FormFactory.SubmitAction(label);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.SubmitAction));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
        }

        // BooleanComponent tests with variations
        [Test]
        public void BooleanComponent_WithVariationName_ReturnsCorrectModel()
        {
            string variationName = "BooleanVariation";
            var model = FormFactory.BooleanComponent(variationName);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Boolean));
            Assert.That(model.DynamicType.VariationName, Is.EqualTo(variationName));
        }

        // DateTimeComponent tests
        [Test]
        public void DateTimeComponent_NoParams_ReturnsCorrectModel()
        {
            var model = FormFactory.DateTimeComponent();

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.DateTime));
        }

        [Test]
        public void DateTimeComponent_WithVariationName_ReturnsCorrectModel()
        {
            string variationName = "DateTimeVariation";
            var model = FormFactory.DateTimeComponent(variationName);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.DateTime));
            Assert.That(model.DynamicType.VariationName, Is.EqualTo(variationName));
        }

        [Test]
        public void DateTimeComponent_WithLabelAndPath_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var model = FormFactory.DateTimeComponent(label, path);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.DateTime));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void IntComponent_WithLabelAndPath_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var model = FormFactory.IntComponent(label, path);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Int));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }

        [Test]
        public void IntComponent_WithVariationNameLabelAndPath_ReturnsCorrectModel()
        {
            string variationName = "IntVariation";
            string label = "TestLabel";
            string path = "TestPath";
            var model = FormFactory.IntComponent(variationName, label, path);

            Assert.That(model, Is.Not.Null);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Int));
            Assert.That(model.DynamicType.VariationName, Is.EqualTo(variationName));
            Assert.That(model.Parameters[ParameterNames.Label], Is.EqualTo(label));
            Assert.That(model.Parameters[ParameterNames.Path], Is.EqualTo(path));
        }
    }

}
