using BlazorDynamics.Forms.Components.Factories;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Common.Enums;
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

            Assert.NotNull(model);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.VerticalLayout));
        }

        [Test]
        public void VerticalLayout_WithVariationName_ReturnsCorrectModel()
        {
            string variationName = "TestVariation";
            var model = FormFactory.VerticalLayout(variationName);

            Assert.NotNull(model);
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

            Assert.NotNull(model);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.VerticalLayout));
            Assert.That(model.SubElements.Count, Is.EqualTo(elements.Length));
            Assert.Contains(elements[0], model.SubElements);
            Assert.Contains(elements[1], model.SubElements);
        }

        [Test]
        public void VerticalLayout_WithBuilder_ReturnsCorrectModel()
        {
            var model = FormFactory.VerticalLayout(builder =>
            {
                builder.WithSubElement(FormFactory.StringComponent("Label", "Path"));
            });

            Assert.NotNull(model);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.VerticalLayout));
            Assert.That(model.SubElements.Count, Is.EqualTo(1));
            Assert.That(model.SubElements.First().DynamicType.TypeName, Is.EqualTo(TypeName.String));
        }

        // Repeat similar tests for HorizontalLayout, GroupLayout, and other component creation methods

        [Test]
        public void StringComponent_WithLabelAndPath_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var model = FormFactory.StringComponent(label, path);

            Assert.NotNull(model);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.String));
            Assert.That(model.Parameters["Label"], Is.EqualTo(label));
            Assert.That(model.Parameters["Path"], Is.EqualTo(path));
        }

        [Test]
        public void BooleanComponent_WithLabelAndPath_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var model = FormFactory.BooleanComponent(label, path);

            Assert.NotNull(model);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Boolean));
            Assert.That(model.Parameters["Label"], Is.EqualTo(label));
            Assert.That(model.Parameters["Path"], Is.EqualTo(path));
        }

        // Add similar tests for each method in the FormFactory class

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

            Assert.NotNull(model);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.NumberDisplay));
            Assert.That(model.Parameters["Label"], Is.EqualTo(label));
            Assert.That(model.Parameters["Path"], Is.EqualTo(path));
        }

        [Test]
        public void Number_WithLabelAndPath_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var model = FormFactory.NumberComponent(label, path);

            Assert.NotNull(model);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.Number));
            Assert.That(model.Parameters["Label"], Is.EqualTo(label));
            Assert.That(model.Parameters["Path"], Is.EqualTo(path));
        }

        [Test]
        public void TemplateDisplay_WithVariationNameAndPath_ReturnsCorrectModel()
        {
            string variationName = "TemplateVariation";
            string path = "TestPath";
            var model = FormFactory.TemplateDisplay(variationName, path);

            Assert.NotNull(model);
            Assert.That(model.DynamicType.TypeName, Is.EqualTo(TypeName.TemplateContent));
            Assert.That(model.DynamicType.VariationName, Is.EqualTo(variationName));
            Assert.That(model.Parameters["Path"], Is.EqualTo(path));
        }
    }
}
