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
    public class LongGuardTests
    {
        [TestCase(0)]
        [TestCase(4)]
        public void GuardInRange_LongValueOutOfRange_ThrowsException(long value)
        {
            var paramName = "param";
            long min = 1;
            long max = 3;
            var expectedMessage = string.Format("The value, {0}, was not within the expected range of {1} to {2}.", value, min, max);

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
