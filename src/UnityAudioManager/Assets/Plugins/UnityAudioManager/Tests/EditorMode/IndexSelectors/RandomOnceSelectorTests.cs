using NUnit.Framework;
using System.Collections.Generic;

namespace long18.AudioManager.Tests.IndexSelectors
{
    [TestFixture, Category("Unit Tests")]
    public class RandomOnceSelectorTests
    {
        private IIndexSelector _selector = new RandomOnceSelector();
        private List<int> _emptyList = new();
        private List<int> _oneElementList = new() { 0 };
        private List<int> _multipleElementList = new() { 0, 1, 2 };

        [Test]
        public void GetNextIndex_ListIsEmpty_ReturnsMinusOne()
        {
            var result = _selector.GetNextIndex(_emptyList, 0);
            Assert.AreEqual(-1, result);
        }

        [Test]
        public void GetNextIndex_ListHasOneElement_ReturnsZero()
        {
            var result = _selector.GetNextIndex(_oneElementList, -1);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetNextIndex_ListHasMultipleElements_FirstRandom_ReturnsRandomIndex()
        {
            var result = _selector.GetNextIndex(_multipleElementList, -1);
            Assert.IsTrue(result >= 0 && result < _multipleElementList.Count);
        }

        [Test]
        public void GetNextIndex_CurrentIndexIsNotMinusOne_SecondRandom_ReturnsMinusOne()
        {
            var result = _selector.GetNextIndex(_multipleElementList, 0);
            Assert.AreEqual(-1, result);
        }
    }
}