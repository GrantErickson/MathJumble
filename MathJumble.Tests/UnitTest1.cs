using MathJumble;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MathJumbleTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test2Nums()
        {
            var source = new CalculatedList<decimal> { 1, 2 };
            var result = MathJumble.Extensions.Permute(source);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void Test3Nums()
        {
            var source = new CalculatedList<decimal> { 1, 2, 3 };
            var result = MathJumble.Extensions.Permute(source);
            Assert.AreEqual(6, result.Count());
        }

        [Test]
        public void Test3NumsResults()
        {
            var source = new CalculatedList<decimal> { 1, 2, 3 };
            var result = MathJumble.Extensions.Permute(source).ToList();
            Assert.Contains(new List<decimal> { 1, 2, 3 }, result);
            Assert.Contains(new List<decimal> { 1, 3, 2 }, result);
            Assert.Contains(new List<decimal> { 2, 1, 3 }, result);
            Assert.Contains(new List<decimal> { 2, 3, 1 }, result);
            Assert.Contains(new List<decimal> { 3, 2, 1 }, result);
            Assert.Contains(new List<decimal> { 3, 1, 2 }, result);
        }


        [Test]
        public void Test4Nums()
        {
            var source = new CalculatedList<decimal> { 1, 2, 3, 4 };
            var result = MathJumble.Extensions.Permute(source);
            Assert.AreEqual(24, result.Count());
        }

        public void StackTest()
        {
            var source = new CalculatedList<decimal> { 1, 2, 3, 4 };
            var stack = new Stack<CalculatedList<decimal>>();
            stack.PermuteAdd(source);
            Assert.AreEqual(24, stack.Count());

        }
    }
}