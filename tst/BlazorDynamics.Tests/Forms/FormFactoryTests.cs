using NUnit.Framework;
using BlazorDynamics.Forms.Components.Factories;
using BlazorDynamics.Core.Models;
using BlazorDynamics.Common.Enums;
using System.Linq;
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
            Assert.AreEqual(TypeName.VerticalLayout, model.DynamicType.TypeName);
        }

        [Test]
        public void VerticalLayout_WithVariationName_ReturnsCorrectModel()
        {
            string variationName = "TestVariation";
            var model = FormFactory.VerticalLayout(variationName);

            Assert.NotNull(model);
            Assert.AreEqual(TypeName.VerticalLayout, model.DynamicType.TypeName);
            Assert.AreEqual(variationName, model.DynamicType.VariationName);
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
            Assert.AreEqual(TypeName.VerticalLayout, model.DynamicType.TypeName);
            Assert.AreEqual(elements.Length, model.SubElements.Count);
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
            Assert.AreEqual(TypeName.VerticalLayout, model.DynamicType.TypeName);
            Assert.AreEqual(1, model.SubElements.Count);
            Assert.AreEqual(TypeName.String, model.SubElements.First().DynamicType.TypeName);
        }

        // Repeat similar tests for HorizontalLayout, GroupLayout, and other component creation methods

        [Test]
        public void StringComponent_WithLabelAndPath_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var model = FormFactory.StringComponent(label, path);

            Assert.NotNull(model);
            Assert.AreEqual(TypeName.String, model.DynamicType.TypeName);
            Assert.AreEqual(label, model.Parameters["Label"]);
            Assert.AreEqual(path, model.Parameters["Path"]);
        }

        [Test]
        public void BooleanComponent_WithLabelAndPath_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var model = FormFactory.BooleanComponent(label, path);

            Assert.NotNull(model);
            Assert.AreEqual(TypeName.Boolean, model.DynamicType.TypeName);
            Assert.AreEqual(label, model.Parameters["Label"]);
            Assert.AreEqual(path, model.Parameters["Path"]);
        }

        // Add similar tests for each method in the FormFactory class

        [Test]
        public void WithVariationName_AddsVariationNameToModel()
        {
            var model = new DynamicFormModel(TypeName.String);
            string variationName = "NewVariation";

            var result = model.WithVariationName(variationName);

            Assert.AreEqual(variationName, result.DynamicType.VariationName);
        }

        [Test]
        public void AddSchemaRule_AddsRuleToModel()
        {
            var model = new DynamicFormModel(TypeName.String);
            string scope = "TestScope";
            var jsonSchema = JObject.Parse("{\"type\": \"string\"}");

            var result = model.AddSchemaRule(RuleEffect.SHOW, scope, jsonSchema);

            Assert.AreEqual(1, result.Rules.Count);
            Assert.AreEqual(RuleEffect.SHOW, result.Rules.First().Effect);
            Assert.AreEqual(scope, result.Rules.First().Condition.Scope);
            Assert.AreEqual(jsonSchema, result.Rules.First().Condition.Schema);
        }

        [Test]
        public void NumberDisplay_WithLabelAndPath_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var model = FormFactory.NumberDisplay(label, path);

            Assert.NotNull(model);
            Assert.AreEqual(TypeName.NumberDisplay, model.DynamicType.TypeName);
            Assert.AreEqual(label, model.Parameters["Label"]);
            Assert.AreEqual(path, model.Parameters["Path"]);
        }

        [Test]
        public void Number_WithLabelAndPath_ReturnsCorrectModel()
        {
            string label = "TestLabel";
            string path = "TestPath";
            var model = FormFactory.NumberComponent(label, path);

            Assert.NotNull(model);
            Assert.AreEqual(TypeName.Number, model.DynamicType.TypeName);
            Assert.AreEqual(label, model.Parameters["Label"]);
            Assert.AreEqual(path, model.Parameters["Path"]);
        }

        [Test]
        public void TemplateDisplay_WithVariationNameAndPath_ReturnsCorrectModel()
        {
            string variationName = "TemplateVariation";
            string path = "TestPath";
            var model = FormFactory.TemplateDisplay(variationName, path);

            Assert.NotNull(model);
            Assert.AreEqual(TypeName.TemplateContent, model.DynamicType.TypeName);
            Assert.AreEqual(variationName, model.DynamicType.VariationName);
            Assert.AreEqual(path, model.Parameters["Path"]);
        }
    }
}
