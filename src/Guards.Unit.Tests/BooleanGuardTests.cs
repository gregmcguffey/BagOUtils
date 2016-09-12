using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BagOUtils.Guards.Unit.Tests
{
    [TestFixture]
    public class BooleanGuardTests
    {
        [Test]
        public void GuardIsTrue_WithFalseValue_ThrowException()
        {
            var flag = false;
            var expectedMessage = "Why being false is bad...";

            var ex = Assert.Throws<InvalidOperationException>(() => flag.GuardIsTrue(expectedMessage));

            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void GuardIsTrue_WithTrueValue_DoesNotThrowException()
        {
            var flag = true;
            var errorMessage = "Why being false is bad...";

            Assert.DoesNotThrow(() => flag.GuardIsTrue(errorMessage));
        }

        [Test]
        public void GuardIsFalse_WithTrueValue_ThrowException()
        {
            var flag = true;
            var expectedMessage = "Why true is bad...";

            var ex = Assert.Throws<InvalidOperationException>(() => flag.GuardIsFalse(expectedMessage));

            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void GuardIsFalse_WithFalseValue_DoesNotThrowException()
        {
            var flag = false;
            var stateDescription = "Why try is bad...";

            Assert.DoesNotThrow(() => flag.GuardIsFalse(stateDescription));
        }
    }
}
