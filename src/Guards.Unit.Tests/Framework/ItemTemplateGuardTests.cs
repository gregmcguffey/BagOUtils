using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BagOUtils.Guards.Framework;

namespace BagOUtils.Guards.Unit.Tests.Framework
{
    [TestFixture]
    public class ItemTemplateGuardTests
    {


        [Test]
        public void Guard_WithValidValue_ReturnsValue()
        {
            // Arrange
            var value = "hello";

            // Act
            var checkedValue = value
                .PrepareItemTemplateGuard<string, ArgumentException>()
                .TestToExecute(() => true)
                .ExceptionBuilder((item, message) => new ArgumentException(message, item))
                .ForItem(value)
                .Guard();

            // Assert
            Assert.AreEqual(value, checkedValue);
        }


        [Test]
        public void Guard_WithBadValue_ThrowsCorrectException()
        {
            // Arrange

            // Act

            // Assert
        }

    }
}
