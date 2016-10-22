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
    public class ItemTemplateComposerTests
    {


        [Test]
        public void Guard_WithValidValue_ReturnsValue()
        {
            // Arrange
            var value = "world";
            var isValid = true;

            // Act
            var checkedValue = value
                .ComposeItemTemplateGuard<string>()
                .TestToExecute(() => isValid)
                .ExceptionBuilder(CommonException.Argument)
                .TemplateUsed("hello {0}")
                .ForItem(value)
                .Guard();

            // Assert
            Assert.AreEqual(value, checkedValue);
        }


        [Test]
        public void Guard_WithBadValue_ThrowsCorrectException()
        {
            // Arrange
            var expectedMessage = "hello world";
            var value = "world";
            var isValid = false;

            Action guardAction = () =>
            {
                value
                    .ComposeItemTemplateGuard<string>()
                    .TestToExecute(() => isValid)
                    .ExceptionBuilder(CommonException.Argument)
                    .TemplateUsed("hello {0}")
                    .ForItem(value)
                    .Guard();
            };

            // Act
            var ex = Assert.Throws<ArgumentException>(() => guardAction());

            // Assert
            StringAssert.Contains(expectedMessage, ex.Message);
        }

        [Test]
        public void Guard_WhenFailsWithNameTemplate_ThrowsCorrectException()
        {
            // Arrange
            var expectedMessage = "hello world";
            var value = "world";
            var isValid = false;

            Action guardAction = () =>
            {
                value
                    .ComposeItemTemplateGuard<string>()
                    .TestToExecute(() => isValid)
                    .ExceptionBuilder(CommonException.Argument)
                    .NameTemplateUsed("hello {item}")
                    .ForItem(value)
                    .Guard();
            };

            // Act
            var ex = Assert.Throws<ArgumentException>(() => guardAction());

            // Assert
            StringAssert.Contains(expectedMessage, ex.Message);
        }
    }
}
