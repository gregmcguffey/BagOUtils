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
    public class IntegerGuardTests
    {
        [Test]
        public void GuardMinimum_BelowMinimum_ThrowsException()
        {
            int value = -1;
            string paramName = "param";
            int minValue = 0;
            var expectedMessage = CustomTemplate
                .BelowMinimum
                .UsingItem(paramName)
                .UsingValue(value)
                .ComparedTo(minValue)
                .Prepare();
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => value.GuardMinimum(paramName, minValue));

            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void GuardMinimum_ValidValue_ReturnsValue(int value)
        {
            int minValue = 0;
            string paramName = "param";

            var returned = value.GuardMinimum(paramName, minValue);

            Assert.AreEqual(value, returned);
        }

        [Test]
        public void GuardMaximum_AboveMaximum_ThrowsException()
        {
            int value = 1;
            string paramName = "param";
            int maxValue = 0;
            var expectedMessage = CustomTemplate
                .AboveMaximum
                .UsingItem(paramName)
                .UsingValue(value)
                .ComparedTo(maxValue)
                .Prepare();

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => value.GuardMaximum(paramName, maxValue));

            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [TestCase(0)]
        [TestCase(99)]
        [TestCase(100)]
        public void GuardMaximum_ValidValue_ReturnsValue(int value)
        {
            int maxValue = 100;
            string paramName = "param";

            var returned = value.GuardMaximum(paramName, maxValue);

            Assert.AreEqual(value, returned);
        }

        [TestCase(0)]
        [TestCase(4)]
        public void GuardInRange_IntValueOutOfRange_ThrowsException(int value)
        {
            var paramName = "param";
            int min = 1;
            int max = 3;
            var expectedMessage = CustomTemplate
                .OutOfRange
                .UsingItem(paramName)
                .UsingValue(value)
                .WithMinimum(min)
                .WithMaximum(max)
                .Prepare();

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => value.GuardInRange(paramName, min, max));

            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [TestCase(1)]
        [TestCase(3)]
        public void GuardInRange_IntValueInOfRange_ReturnsValue(int value)
        {
            var paramName = "param";
            int min = 1;
            int max = 3;

            var returned = value.GuardInRange(paramName, min, max);

            Assert.AreEqual(value, returned);
        }
    }
}
