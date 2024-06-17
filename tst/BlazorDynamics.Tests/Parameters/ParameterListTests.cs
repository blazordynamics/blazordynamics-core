using BlazorDynamics.Core.Models.ParameterModels;

namespace BlazorDynamics.Tests.Parameters
{
    [TestFixture]
    public class ParameterListTests
    {

       
        [Test]
        public void CanReceiveParametersThroughConstructor()
        {
            //Arrange
            var parameters = new Dictionary<string, object>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 }
            };

            // act 
            var sut = new ParameterList(parameters);
            Assert.NotNull(sut);
            Assert.That(sut.Count(), Is.EqualTo(parameters.Count));
            Assert.That(sut.Contains(parameters.ToArray()[0]));
            Assert.That(sut.Contains(parameters.ToArray()[1]));
            Assert.That(sut.Contains(parameters.ToArray()[2]));
        }

        [Test]
        public void CanReceiveParametersThroughConstructorAndAccessThemByEntries()
        {
            //Arrange
            var parameters = new Dictionary<string, object>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 }
            };

            // act 
            var sut = new ParameterList(parameters);
            Assert.NotNull(sut);
            Assert.That(sut.Entries.Count(), Is.EqualTo(parameters.Count));
            Assert.That(sut.Entries.Contains(parameters.ToArray()[0]));
            Assert.That(sut.Entries.Contains(parameters.ToArray()[1]));
            Assert.That(sut.Entries.Contains(parameters.ToArray()[2]));
        }

        [Test]
        public void CanAddParameterAfterConstruction()
        {
            //Arrange
            var sut = new ParameterList();

            // act 
            sut.Add("one", 1);

            Assert.NotNull(sut);
            Assert.That(sut.Entries.Count(), Is.EqualTo(1));
            Assert.That(sut.Entries.First().Key, Is.EqualTo("one"));
            Assert.That(sut.Entries.First().Value, Is.EqualTo(1));
        }

        [Test]
        public void CanAddParametersAfterConstruction()
        {
            //Arrange
            var sut = new ParameterList();
            var parameters = new Dictionary<string, object>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 }
            };
            var baseParameterList = new ParameterList(parameters);

            // act 
            sut.AddRange(baseParameterList);

            Assert.NotNull(sut);
            Assert.That(sut.Entries.Count(), Is.EqualTo(parameters.Count));
            Assert.That(sut.Entries.Contains(parameters.ToArray()[0]));
            Assert.That(sut.Entries.Contains(parameters.ToArray()[1]));
            Assert.That(sut.Entries.Contains(parameters.ToArray()[2]));
        }

        [Test]
        public void CanNotAddSameKeyParameterAfterConstruction()
        {
            //Arrange
            var sut = new ParameterList();

            // act 
            sut.Add("one", 1);
            sut.Add("one", 11);// will override value

            Assert.NotNull(sut);
            Assert.That(sut.Entries.Count(), Is.EqualTo(1));
            Assert.That(sut.Entries.First().Key, Is.EqualTo("one"));
            Assert.That(sut.Entries.First().Value, Is.EqualTo(11));
        }

        [Test]
        public void CanNotAddSameKeyParameterAfterConstructionUsingIndexer()
        {
            //Arrange
            var sut = new ParameterList();

            // act 
            sut["one"] = 1;
            sut["one"] =  11;// will override value

            Assert.NotNull(sut);
            Assert.That(sut.Entries.Count(), Is.EqualTo(1));
            Assert.That(sut.Entries.First().Key, Is.EqualTo("one"));
            Assert.That(sut.Entries.First().Value, Is.EqualTo(11));
        }

        [Test]
        public void WillThrowErrorWhenKeyDoesntExists()
        {
            //Arrange
            var sut = new ParameterList();

            // act +  assert
            Assert.Throws<KeyNotFoundException>(() => { object test = sut["not present"]; });
        }

        [Test]
        public void WillReturnEnumerator()
        {
            //Arrange

            var sut = new ParameterList();

            Assert.IsNotNull(sut.GetEnumerator());
        }
    }
}
