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
    public class BooleanGuardTests
    {
        [Test]
        public void GuardIsTrue_WithFalseValue_ThrowException()
        {
            var flag = false;
            var item = "anyItem";
            var expectedMessage = ItemTemplate
                .NotTrue
                .UsingItem(item)
                .Prepare();

            var ex = Assert.Throws<InvalidOperationException>(() => flag.GuardIsTrue(item));

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
            var item = "anyItem";
            var expectedMessage = ItemTemplate
                .NotFalse
                .UsingItem(item)
                .Prepare();

            var ex = Assert.Throws<InvalidOperationException>(() => flag.GuardIsFalse(item));

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
