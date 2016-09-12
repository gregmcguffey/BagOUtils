using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Gpm.Validation;

namespace Gpm.Validation.Unit.Tests
{
    [TestFixture]
    public class GuardExtensionTests
    {
        [TestCase(1, 2, true)]
        [TestCase(1, 1, false)]
        [TestCase(1, 0, false)]
        public void LessThan_ComparedToLargerNumber_ReturnsCorrectResult(int value, int otherValue, bool expectedResult)
        {
            var result = value.LessThan(otherValue);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void LessThan_LongToInt_ReturnsCorrectResult()
        {
            long value = 1;
            int otherValue = 1;
            bool expectedResult = false;

            //-- This works with any combination of numbers
            //   as long as the otherValue can be implicitly
            //   cast to the value type. If not, it won't compile.
            var result = value.LessThan(otherValue);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(1, 2, true)]
        [TestCase(1, 1, true)]
        [TestCase(1, 0, false)]
        public void LessThanOrEqual_ComparedToLargerNumber_ReturnsCorrectResult(int value, int otherValue, bool expectedResult)
        {
            var result = value.LessThanOrEqual(otherValue);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void LessThanOrEqual_LongToInt_ReturnsCorrectResult()
        {
            long value = 1;
            int otherValue = 1;
            bool expectedResult = true;

            //-- This works with any combination of numbers
            //   as long as the otherValue can be implicitly
            //   cast to the value type. If not, it won't compile.
            var result = value.LessThanOrEqual(otherValue);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(1, 2, false)]
        [TestCase(1, 1, true)]
        [TestCase(1, 0, false)]
        public void EqualTo_ComparedToLargerNumber_ReturnsCorrectResult(int value, int otherValue, bool expectedResult)
        {
            var result = value.EqualTo(otherValue);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void EqualTo_LongToInt_ReturnsCorrectResult()
        {
            long value = 1;
            int otherValue = 1;
            bool expectedResult = true;

            //-- This works with any combination of numbers
            //   as long as the otherValue can be implicitly
            //   cast to the value type. If not, it won't compile.
            var result = value.EqualTo(otherValue);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(1, 2, false)]
        [TestCase(1, 1, true)]
        [TestCase(1, 0, true)]
        public void GreaterThanOrEqual_ComparedToLargerNumber_ReturnsCorrectResult(int value, int otherValue, bool expectedResult)
        {
            var result = value.GreaterThanOrEqual(otherValue);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GreaterThanOrEqual_LongToInt_ReturnsCorrectResult()
        {
            long value = 1;
            int otherValue = 1;
            bool expectedResult = true;

            //-- This works with any combination of numbers
            //   as long as the otherValue can be implicitly
            //   cast to the value type. If not, it won't compile.
            var result = value.GreaterThanOrEqual(otherValue);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(1, 2, false)]
        [TestCase(1, 1, false)]
        [TestCase(1, 0, true)]
        public void GreaterThan_ComparedToLargerNumber_ReturnsCorrectResult(int value, int otherValue, bool expectedResult)
        {
            var result = value.GreaterThan(otherValue);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GreaterThan_LongToInt_ReturnsCorrectResult()
        {
            long value = 1;
            int otherValue = 1;
            bool expectedResult = false;

            //-- This works with any combination of numbers
            //   as long as the otherValue can be implicitly
            //   cast to the value type. If not, it won't compile.
            var result = value.GreaterThan(otherValue);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
