using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BagOUtils.Guards.Unit.Tests
{
    [TestFixture]
    public class LimitMessageBuilderTests
    {

        [Test]
        public void MessageForValue_WhenOnlyMin_ReturnsCorrectMessage()
        {
            // Arrange
            var expectedMessage = "The value, 1, is below the minimum limit of 10.";
            var minLimit = 10;
            var value = 1;
            var builder = value.BuildLimitMessage();

            // Act
            var actualMessage = builder
                .WithMin(minLimit)
                .BuildMessage();

            // Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void MessageForValue_WhenOnlyMax_ReturnsCorrectMessage()
        {
            // Arrange
            var expectedMessage = "The value, 20, is above the maximum limit of 10.";
            var maxLimit = 10;
            var value = 20;
            var builder = value.BuildLimitMessage();

            // Act
            var actualMessage = builder
                .WithMax(maxLimit)
                .BuildMessage();

            // Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestCase(5,10,100)]
        [TestCase(9,10,100)]
        [TestCase(101,10,100)]
        [TestCase(105,10,100)]
        public void MessageForValue_WhenBelowRange_ReturnsCorrectMessage(int value, int minLimit, int maxLimit)
        {
            // Arrange
            var expectedMessage = $"The value, {value}, was not within the expected range of {minLimit} to {maxLimit}.";
            var builder = value.BuildLimitMessage();

            // Act
            var actualMessage = builder
                .WithMin(minLimit)
                .WithMax(maxLimit)
                .BuildMessage();

            // Assert
            Assert.AreEqual(expectedMessage, actualMessage);
        }

    }
}
