using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BagOUtils.Guards;

namespace BagOUtils.Guards.Unit.Tests
{
    [TestFixture]
    public class GuardExtensionTests
    {
        //-------------------------------------------------------------------------
        //
        // Comparison Methods
        //
        //-------------------------------------------------------------------------

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

        //-------------------------------------------------------------------------
        //
        // Null Handlers
        //
        //-------------------------------------------------------------------------


        [Test]
        public void NullToEmpty_WithNull_ReturnsEmpty()
        {
            // Arrange
            string nullString = null;

            // Act
            var actualString = nullString.NullToEmpty();

            // Assert
            Assert.IsEmpty(actualString);
        }

        [TestCase("")]
        [TestCase("    ")]
        [TestCase("any non empty string")]
        public void NullToEmpty_WithString_ReturnsString(string text)
        {
            // Act
            var actual = text.NullToEmpty();

            // Assert
            Assert.AreEqual(text, actual);
        }

        [Test]
        public void NullToEmpty_WithNullObject_ReturnsEmpty()
        {
            // Arrange
            StringBuilder nullBuilder = null;

            // Act
            var actual = nullBuilder.NullToEmpty();

            // Assert
            Assert.IsEmpty(actual);
        }
            
        [Test]
        public void NullToEmpty_WithObject_ReturnsCorrectString()
        {
            // Arrange
            var builder = new StringBuilder();
            var expected = builder.ToString();

            // Act
            var actual = builder.NullToEmpty();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
