using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BagOUtils.Guards.Messages;

namespace BagOUtils.Guards.Unit.Tests
{
    [TestFixture]
    public class StringGuardTests
    {
        [Test]
        public void GuardIsNotNull_WithString_ReturnsString()
        {
            // Arrange
            var originalString = "A valid, non-null, non-empty, not whitespace only string.";

            // Act
            var returnedString = originalString.GuardIsSet("any");

            // Assert
            Assert.AreEqual(expected: originalString, actual: returnedString);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void GuardIsSet_WithNotSetItem_ThrowsException(string param)
        {
            var paramName = "param";
            var expectedMessage = "Must be set to a non-null, non-empty, non-whitespace only string.";

            var ex = Assert.Throws<ArgumentException>(() => param.GuardIsSet(paramName));

            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void GuardIsSet_WithValue_ReturnsValue()
        {
            var param = "tested string";
            var paramName = "param";

            var returned = param.GuardIsSet(paramName);

            Assert.AreSame(param, returned);
        }

        [TestCase("a")]
        [TestCase("aaa")]
        public void GuardRequiredLength_WithWrongLength_ThrowsException(string param)
        {
            int requiredLength = 2;
            var paramName = "param";
            var actualLenth = param.Length;
            var pluralizer = actualLenth > 1 ? "s" : string.Empty;
            var exceptionMessage = CustomTemplate
                .NotRequiredSize
                .UsingItem(paramName)
                .UsingValue(param)
                .RequiringLength(requiredLength)
                .Prepare();

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => param.GuardRequiredLength(paramName, requiredLength));

            StringAssert.Contains(exceptionMessage, ex.Message);
        }

        [TestCase("a", 1)]
        [TestCase("aa", 2)]
        [TestCase("aaa", 3)]
        public void GuardRequiredLength_WithCorrectLength_ReturnsValue(string param, int requiredLength)
        {
            var paramName = "param";

            var returned = param.GuardRequiredLength(paramName, requiredLength);

            Assert.AreSame(param, returned);
        }

        [Test]
        public void GuardRequiredLength_WithRequiredZeroLength_ThrowsException()
        {
            var param = "anything";
            var paramName = "param";
            int requiredLength = 0;
            var expectedMessage = Message.BadGuardRequiredLength;

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => param.GuardRequiredLength(paramName, requiredLength));

            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void GuardSize_WithNotSetItem_ThrowsException(string param)
        {
            var paramName = "param";
            int min = 2;
            int max = 3;
            var expectedMessage = "Must be set to a non-null, non-empty, non-whitespace only string.";
            //var expectedMessage = string.Format( "The text must be between {0} and {1} characters long.", min, max );

            var ex = Assert.Throws<ArgumentException>(() => param.GuardSize(paramName, min, max));

            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [TestCase("a")]
        [TestCase("aaaa")]
        public void GuardSize_WithWrongSize_ThrowsException(string param)
        {
            var paramName = "param";
            int min = 2;
            int max = 3;
            var expectedMessage = string.Format("The text must be between {0} and {1} characters long.", min, max);

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => param.GuardSize(paramName, min, max));

            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [TestCase("aa")]
        [TestCase("aaa")]
        public void GuardSize_WithCorrectSize_ReturnsValue(string param)
        {
            var paramName = "param";
            int min = 2;
            int max = 3;

            var returned = param.GuardSize(paramName, min, max);

            Assert.AreSame(param, returned);
        }
    }
}
