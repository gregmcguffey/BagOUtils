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
    public class ObjectGuardTests
    {
        [Test]
        public void GuardIsNotNull_WithNull_ThrowsException()
        {
            //-- any reference type will work.
            string param = null;
            var paramName = "param";
            var exceptionMessage = "You must provide a parameter.";

            var ex = Assert.Throws<ArgumentNullException>(() => param.GuardIsNotNull(paramName, exceptionMessage));

            StringAssert.Contains(exceptionMessage, ex.Message);
        }

        [Test]
        public void GuardIsNotNull_WithItem_ReturnsItem()
        {
            var param = "hello World";
            var paramName = "param";
            var exceptionMessage = "You must provide a parameter.";

            var returned = param.GuardIsNotNull(paramName, exceptionMessage);

            Assert.AreSame(param, returned);
        }

        [Test]
        public void GuardIsNotNull_WithValueType_ReturnsItem()
        {
            // Test if an item that can't be null, works.
            int param = 1;
            var paramName = "param";
            var exceptionMessage = "Should never see this if value can't be null.";

            var returned = param.GuardIsNotNull(paramName, exceptionMessage);

            Assert.AreEqual(param, returned);
        }

        [Test]
        public void GuardIsRequiredForOperation_WithNull_ThrowsException()
        {
            //-- any reference type will work.
            string param = null;
            var exceptionMessage = "The following operation can't be completed.";

            var ex = Assert.Throws<InvalidOperationException>(() => param.GuardIsRequiredForOperation(exceptionMessage));

            StringAssert.Contains(exceptionMessage, ex.Message);
        }

        [Test]
        public void GuardIsRequiredForOperation_WithItem_ReturnsItem()
        {
            var param = "hello World";
            var exceptionMessage = "The following operation can't be completed.";

            var returned = param.GuardIsRequiredForOperation(exceptionMessage);

            Assert.AreSame(param, returned);
        }

        [Test]
        public void GuardIsRequiredForOperation_WithValueType_ReturnsItem()
        {
            // Test if an item that can't be null, works.
            int param = 1;
            var exceptionMessage = "Should never see this if value can't be null.";

            var returned = param.GuardIsRequiredForOperation(exceptionMessage);

            Assert.AreEqual(param, returned);
        }

        [Test]
        public void GuardIsNotDefault_WithNullObject_ThrowsException()
        {
            // Arrange
            var nullMessage = "The item cannot be null.";
            string nullItem = null;
            var paramName = "param";

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => nullItem.GuardIsNotDefault<string>(paramName));

            // Assert
            StringAssert.Contains(expected: nullMessage, actual: ex.Message);
        }

        [Test]
        public void GuardIsNotDefault_WithDefaultInt_ThrowsException()
        {
            // Arrange
            var defaultMessage = "The item cannot be the default value for this type (Int32).";
            int defaultItem = default(int);
            var paramName = "param";

            // Act
            var ex = Assert.Throws<ArgumentException>(() => defaultItem.GuardIsNotDefault<int>(paramName));

            // Assert
            StringAssert.Contains(expected: defaultMessage, actual: ex.Message);
        }

        [Test]
        public void GuardIsNotDefault_WithNonNull_ReturnsItem()
        {
            // Arrange
            var nonNullItem = "a";
            var paramName = "param";

            // Act
            var returnedItem = nonNullItem.GuardIsNotDefault<string>(paramName);

            // Assert
            Assert.AreEqual(expected: nonNullItem, actual: returnedItem);
        }

        [Test]
        public void GuardIsNotDefault_WithNonDefault_ReturnsItem()
        {
            // Arrange
            var nonNullItem = 1;
            var paramName = "param";

            // Act
            var returnedItem = nonNullItem.GuardIsNotDefault<int>(paramName);

            // Assert
            Assert.AreEqual(expected: nonNullItem, actual: returnedItem);
        }
    }
}
