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
    public class LongGuardTests
    {
        [TestCase(0)]
        [TestCase(4)]
        public void GuardInRange_LongValueOutOfRange_ThrowsException(long value)
        {
            var paramName = "param";
            long min = 1;
            long max = 3;
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
        public void GuardInRange_LongValueInOfRange_ReturnsValue(long value)
        {
            var paramName = "param";
            long min = 1;
            long max = 3;

            var returned = value.GuardInRange(paramName, min, max);

            Assert.AreEqual(value, returned);
        }
    }
}
