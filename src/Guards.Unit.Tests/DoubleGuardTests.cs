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
    public class DoubleGuardTests
    {
        [TestCase(0.9D)]
        [TestCase(3.1D)]
        public void GuardInRange_DoubleValueOutOfRange_ThrowsException(double value)
        {
            var paramName = "param";
            double min = 1D;
            double max = 3D;
            var expectedMessage = string.Format("The value, {0}, was not within the expected range of {1} to {2}.", value, min, max);

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
