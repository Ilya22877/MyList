using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace MyList
{
    [TestFixture]
    public class MyListTests
    {
        private MyList<int> _sut;

        [SetUp]
        public void Init()
        {
            _sut = new MyList<int>();
        }

        [Test]
        public void ShouldReturnZEro_WhenListEmpty()
        {
            _sut.Count.Should().Be(0);
        }

        [Test]
        public void MustAddOneElement()
        {
            var element = 5;

            _sut.Add(element);

            _sut.Count.Should().Be(1);
            _sut.First().Should().Be(element);
        }

        [Test]
        public void ShouldIncreaseSize_WhenLotsElement()
        {
            var size = 1000;
            while (_sut.Count < size)
            {
                var value = _sut.Count;
                _sut.Add(value);
                _sut.Last().Should().Be(value);
            }

            _sut.Count.Should().Be(size);
        }

        [Test]
        public void ShouldClearList()
        {
            _sut.Add(1);
            _sut.Add(2);

            _sut.Clear();

            _sut.Count.Should().Be(0);
        }

        [Test]
        public void ShouldFindElement_WhenUseContains()
        {
            _sut.Add(1);
            _sut.Add(2);

            _sut.Contains(1).Should().Be(true);
        }

        [Test]
        public void ShouldNotFindDefaultElement_WhenUseContains()
        {
            _sut.Add(1);
            _sut.Add(2);

            _sut.Contains(0).Should().Be(false);
        }

        [Test]
        public void ShouldCopyToArray()
        {
            _sut.Add(1);
            _sut.Add(2);
            var array = new []{1,0,0,0};
            var expectedArray = new[] { 1, 1, 2, 0 };

            _sut.CopyTo(array, 1);

            array.Should().BeEquivalentTo(expectedArray);
        }

        [Test]
        public void ShouldInsertElement()
        {
            _sut.Add(1);
            _sut.Add(2);
            _sut.Add(3);
            var expectedResult = new[] { 1, 2, 4, 3 };

            _sut.Insert(2, 4);

            _sut.Count.Should().Be(4);
            _sut.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void ShouldRemoveElement()
        {
            _sut.Add(1);
            _sut.Add(2);
            _sut.Add(3);
            var expectedResult = new[] { 1, 3 };

            _sut.Remove( 2);

            _sut.Count.Should().Be(2);
            _sut.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public void ShouldRemoveAtElement()
        {
            _sut.Add(1);
            _sut.Add(2);
            _sut.Add(3);
            var expectedResult = new[] { 1, 3 };

            _sut.RemoveAt(1);

            _sut.Count.Should().Be(2);
            _sut.Should().BeEquivalentTo(expectedResult);
        }
    }
}