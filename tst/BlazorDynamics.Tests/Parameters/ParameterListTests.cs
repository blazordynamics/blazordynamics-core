using BlazorDynamics.Common.Models;

namespace BlazorDynamics.Tests.Parameters
{
    [TestFixture]
    public class ParameterListTests
    {
        [Test]
        public void CanReceiveParametersThroughConstructor()
        {
            // Arrange
            var parameters = new Dictionary<string, object>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 }
            };

            // Act 
            var sut = new ParameterList(parameters);

            // Assert
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Count(), Is.EqualTo(parameters.Count));
            Assert.That(sut.Contains(parameters.ToArray()[0]), Is.True);
            Assert.That(sut.Contains(parameters.ToArray()[1]), Is.True);
            Assert.That(sut.Contains(parameters.ToArray()[2]), Is.True);
        }

        [Test]
        public void CanReceiveParametersThroughConstructorAndAccessThemByEntries()
        {
            // Arrange
            var parameters = new Dictionary<string, object>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 }
            };

            // Act 
            var sut = new ParameterList(parameters);

            // Assert
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Entries.Count(), Is.EqualTo(parameters.Count));
            Assert.That(sut.Entries.Contains(parameters.ToArray()[0]), Is.True);
            Assert.That(sut.Entries.Contains(parameters.ToArray()[1]), Is.True);
            Assert.That(sut.Entries.Contains(parameters.ToArray()[2]), Is.True);
        }

        [Test]
        public void CanAddParameterAfterConstruction()
        {
            // Arrange
            var sut = new ParameterList();

            // Act 
            sut.Add("one", 1);

            // Assert
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Entries.Count(), Is.EqualTo(1));
            Assert.That(sut.Entries.First().Key, Is.EqualTo("one"));
            Assert.That(sut.Entries.First().Value, Is.EqualTo(1));
        }

        [Test]
        public void CanAddParametersAfterConstruction()
        {
            // Arrange
            var sut = new ParameterList();
            var parameters = new Dictionary<string, object>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 }
            };
            var baseParameterList = new ParameterList(parameters);

            // Act 
            sut.AddRange(baseParameterList);

            // Assert
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Entries.Count(), Is.EqualTo(parameters.Count));
            Assert.That(sut.Entries.Contains(parameters.ToArray()[0]), Is.True);
            Assert.That(sut.Entries.Contains(parameters.ToArray()[1]), Is.True);
            Assert.That(sut.Entries.Contains(parameters.ToArray()[2]), Is.True);
        }

        [Test]
        public void CanNotAddSameKeyParameterAfterConstruction()
        {
            // Arrange
            var sut = new ParameterList();

            // Act 
            sut.Add("one", 1);
            sut.Add("one", 11); // will override value

            // Assert
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Entries.Count(), Is.EqualTo(1));
            Assert.That(sut.Entries.First().Key, Is.EqualTo("one"));
            Assert.That(sut.Entries.First().Value, Is.EqualTo(11));
        }

        [Test]
        public void CanNotAddSameKeyParameterAfterConstructionUsingIndexer()
        {
            // Arrange
            var sut = new ParameterList();

            // Act 
            sut["one"] = 1;
            sut["one"] = 11; // will override value

            // Assert
            Assert.That(sut, Is.Not.Null);
            Assert.That(sut.Entries.Count(), Is.EqualTo(1));
            Assert.That(sut.Entries.First().Key, Is.EqualTo("one"));
            Assert.That(sut.Entries.First().Value, Is.EqualTo(11));
        }

        [Test]
        public void WillThrowErrorWhenKeyDoesntExists()
        {
            // Arrange
            var sut = new ParameterList();

            // Act + Assert
            Assert.That(() => { object test = sut["not present"]; }, Throws.TypeOf<KeyNotFoundException>());
        }

        [Test]
        public void WillReturnEnumerator()
        {
            // Arrange
            var sut = new ParameterList();

            // Assert
            Assert.That(sut.GetEnumerator(), Is.Not.Null);
        }
    }
}