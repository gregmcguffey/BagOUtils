using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BagOUtils.Guards;
using BagOUtils.Guards.Messages;

namespace BagOUtils.Guards.Unit.Tests
{
    [TestFixture]
    public class FloatGuardTests
    {
        [TestCase(0.9F)]
        [TestCase(3.1F)]
        public void GuardInRange_FloatValueOutOfRange_ThrowsException(float value)
        {
            var paramName = "param";
            float min = 1;
            float max = 3;
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
        public void GuardInRange_FloatValueInOfRange_ReturnsValue(float value)
        {
            var paramName = "param";
            float min = 1;
            float max = 3;

            var returned = value.GuardInRange(paramName, min, max);

            Assert.AreEqual(value, returned);
        }
    }
}
