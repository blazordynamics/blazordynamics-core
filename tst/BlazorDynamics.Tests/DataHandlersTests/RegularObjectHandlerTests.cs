using BlazorDynamics.Forms.Commons.DataHandlers;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace BlazorDynamics.Tests.DataHandlersTests
{
    [TestFixture]
    public class RegularObjectHandlerTests
    {
        private MethodInfo _extractPropertyAndIndexMethod;
        private RegularObjectHandler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new RegularObjectHandler();
            _extractPropertyAndIndexMethod = typeof(RegularObjectHandler).GetMethod("ExtractPropertyAndIndex", BindingFlags.NonPublic | BindingFlags.Static);
        }

        [Test]
        public void GetValue_ShouldReturnCorrectValue_WhenPathIsValid()
        {
            var obj = new { Property = "Value", Nested = new { InnerProperty = "InnerValue" } };
            var result = _handler.GetValue("Nested.InnerProperty", obj);
            Assert.That("InnerValue", Is.EqualTo(result));
        }

        [Test]
        public void GetValue_ShouldReturnNull_WhenPathIsInvalid()
        {
            var obj = new { Property = "Value", Nested = new { InnerProperty = "InnerValue" } };
            var result = _handler.GetValue("Nested.NonExistentProperty", obj);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetValue_ShouldReturnCorrectIndexedValue_WhenPathContainsIndex()
        {
            var obj = new { List = new List<string> { "First", "Second" } };
            var result = _handler.GetValue("List[1]", obj);
            Assert.That(result, Is.EqualTo("Second"));
        }

        [Test]
        public void SetValue_ShouldSetPropertyValue_WhenPathIsValid()
        {
            var obj = new TestClass();
            _handler.SetValue("Property", obj, "NewValue");
            Assert.That(obj.Property, Is.EqualTo("NewValue"));
        }

        [Test]
        public void SetValue_ShouldSetIndexedPropertyValue_WhenPathContainsIndex()
        {
            var obj = new TestClass { List = new List<string> { "First", "Second" } };
            _handler.SetValue("List[1]", obj, "NewValue");
            Assert.That(obj.List[1], Is.EqualTo("NewValue"));
        }

        [Test]
        public void RemoveValue_ShouldRemoveIndexedValue_WhenPathContainsIndex()
        {
            var obj = new TestClass { List = new List<string> { "First", "Second" } };
            _handler.RemoveValue("List[0]", obj);
            Assert.That(obj.List.Count, Is.EqualTo(1));
            Assert.That(obj.List[0], Is.EqualTo("Second"));
        }

        [Test]
        public void AddValue_ShouldAddValueToList_WhenPathIsValid()
        {
            var obj = new TestClass { List = new List<string> { "First" } };
            _handler.AddValue("List", obj, "Second");
            Assert.That(obj.List.Count, Is.EqualTo(2));
            Assert.That(obj.List[1], Is.EqualTo("Second"));
        }

        [Test]
        public void GetCounterValue_ShouldReturnCorrectCount_WhenPathIsValid()
        {
            var obj = new { List = new List<string> { "First", "Second", "Third" } };
            var count = _handler.GetCounterValue("List", obj);
            Assert.That(count, Is.EqualTo(3));
        }

        [Test]
        public void GetCounterValue_ShouldReturnZero_WhenPathIsInvalid()
        {
            var obj = new { List = new List<string> { "First", "Second", "Third" } };
            var count = _handler.GetCounterValue("NonExistentList", obj);
            Assert.That(count, Is.EqualTo(0));
        }

        private class TestClass
        {
            public string Property { get; set; }
            public List<string> List { get; set; } = new List<string>();
        }

        [Test]
        public void ExtractPropertyAndIndex_ShouldThrowException_WhenIndexIsInvalid()
        {
            string segment = "Property[invalid]";

            var ex = Assert.Throws<TargetInvocationException>(() =>
                _extractPropertyAndIndexMethod.Invoke(null, new object[] { segment }));

            Assert.That(ex.InnerException, Is.InstanceOf<Exception>());
            Assert.That(ex.InnerException.Message, Is.EqualTo($"Invalid index format in segment {segment}"));
        }

        [Test]
        public void ExtractPropertyAndIndex_ShouldReturnCorrectTuple_WhenIndexIsValid()
        {
            string segment = "Property[2]";

            var result = (ValueTuple<string, int>)_extractPropertyAndIndexMethod.Invoke(null, new object[] { segment });

            Assert.That(result.Item1, Is.EqualTo("Property"));
            Assert.That(result.Item2, Is.EqualTo(2));
        }

        [Test]
        public void GetParameters_ShouldFilterAndReturnValidParameters()
        {
            // Arrange
            Type componentType = typeof(TestComponent);
            var allParams = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("ValidProp1", "Value1"),
                new KeyValuePair<string, object>("ValidProp2", "Value2"),
                new KeyValuePair<string, object>("InvalidProp", "Value3")
            };

            var expectedParameters = new Dictionary<string, object>
            {
                { "ValidProp1", "Value1" },
                { "ValidProp2", "Value2" }
            };

            // Act
            var result = RegularObjectHandler.GetParameters(componentType, new Dictionary<string, object>(), allParams);

            // Assert
            Assert.That(expectedParameters.Count, Is.EqualTo(result.Count));
            foreach (var key in expectedParameters.Keys)
            {
                Assert.That(result.ContainsKey(key), Is.True);
                Assert.That(expectedParameters[key], Is.EqualTo( result[key]));
            }
        }
        private class TestComponent
        {
            // Simulate properties that might have annotations determining they can accept parameters
            [Parameter]
            public string ValidProp1 { get; set; } = string.Empty;

            [Parameter]
            public string ValidProp2 { get; set; } = string.Empty;
        }

        [Test]
        public void RemoveValue_ShouldRemovePropertyValue_WhenPathIsValid()
        {
            // Arrange
            var obj = new TestObject
            {
                Property = "InitialValue",
                Nested = new NestedObject { InnerProperty = "InnerValue" }
            };

            string path = "Nested.InnerProperty";

            // Act
            _handler.RemoveValue(path, obj);

            // Assert
            Assert.That(obj.Nested.InnerProperty, Is.Null);
        }

        [Test]
        public void RemoveValue_ShouldThrowException_WhenIntermediatePropertyIsNull()
        {
            // Arrange
            var obj = new TestObject
            {
                Property = "InitialValue",
                Nested = null
            };

            string path = "Nested.InnerProperty";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _handler.RemoveValue(path, obj));
            Assert.That(ex.Message, Is.EqualTo("Property Nested was null and could not traverse to InnerProperty"));
        }

        [Test]
        public void AddValue_ShouldAddValueToList_WhenPathIsValidOnList()
        {
            // Arrange
            var obj = new TestClassArray { List = new List<string> { "First" }.ToArray() };

            // Act
            _handler.AddValue("List", obj, "Second");

            // Assert
            Assert.That(2,Is.EqualTo( obj.List.Length));
            Assert.That("Second", Is.EqualTo( obj.List[1]));
        }

        private class TestClassArray
        {
            public string Property { get; set; }
            public string[] List { get; set; } = new List<string>().ToArray();
        }


        private class TestObject
        {
            public string Property { get; set; }
            public NestedObject Nested { get; set; }
        }

        private class NestedObject
        {
            public string InnerProperty { get; set; }
        }
    }
}
