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
    public class DoubleGuardTests
    {
        [TestCase(0.9D)]
        [TestCase(3.1D)]
        public void GuardInRange_DoubleValueOutOfRange_ThrowsException(double value)
        {
            var paramName = "param";
            double min = 1D;
            double max = 3D;
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

        [TestCase(1D)]
        [TestCase(3D)]
        public void GuardInRange_DoubleValueInOfRange_ReturnsValue(double value)
        {
            var paramName = "param";
            double min = 1D;
            double max = 3D;

            var returned = value.GuardInRange(paramName, min, max);

            Assert.AreEqual(value, returned);
        }
    }
}
