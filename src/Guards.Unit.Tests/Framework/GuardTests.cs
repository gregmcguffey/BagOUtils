using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BagOUtils.Guards.Framework;

namespace BagOUtils.Guards.Unit.Tests.Framework
{
    [TestFixture]
    public class GuardTests
    {

        [Test]
        public void ValidateAndReturn_WithPassingTest_ReturnsValue()
        {
            // Arrange
            var value = 1;
            Func<bool> test = () => true;
            Func<Exception> exBuilder = () => new Exception();
            var guard = new Guard<int>(value);
            guard
                .ExceptionBuilderUsed(exBuilder)
                .Test(test);

            // Act
            var validatedValue = guard.ValidateAndReturn();

            // Assert
            Assert.AreEqual(value, validatedValue);
        }

        [Test]
        public void ValidateAndReturn_WithPassingTest_ReturnsObject()
        {
            // Arrange
            var item = new TestObject { Name = "bob" };
            Func<bool> test = () => true;
            Func<Exception> exBuilder = () => new Exception();
            var guard = new Guard<TestObject>(item);
            guard
                .ExceptionBuilderUsed(exBuilder)
                .Test(test);

            // Act
            var validatedItem = guard.ValidateAndReturn();

            // Assert
            Assert.AreSame(item, validatedItem);
        }


        [Test]
        public void ValidateAndReturn_WithFailingTest_ThrowsException()
        {
            // Arrange
            var value = 1;
            Func<bool> test = () => false;
            Func<Exception> exBuilder = () => new FormatException();
            var guard = new Guard<int>(value);
            guard
                .ExceptionBuilderUsed(exBuilder)
                .Test(test);

            // Act
            var actualEx = Assert.Throws<FormatException>(() => guard.ValidateAndReturn());

            // Assert
            Assert.IsInstanceOf<Exception>(actualEx);
        }


        [Test]
        public void ValidateAndReturn_WithNoExceptionBuilder_ThrowsException()
        {
            // Arrange
            var missingExceptionBuilderMessage = "Guard is not configured property. No exception builder has been defined.";
            var value = 1;
            Func<bool> test = () => false;
            Func<Exception> exBuilder = () => new Exception();
            var guard = new Guard<int>(value);
            guard
                .Test(test);

            // Act
            var actualEx = Assert.Throws<InvalidOperationException>(() => guard.ValidateAndReturn());

            // Assert
            StringAssert.Contains(missingExceptionBuilderMessage, actualEx.Message);
        }

        [Test]
        public void ValidateAndReturn_WithNoTest_ThrowsException()
        {
            // Arrange
            var missingTestMessage = "Guard is not configured property. No test has been defined.";
            var value = 1;
            Func<bool> test = () => false;
            Func<Exception> exBuilder = () => new Exception();
            var guard = new Guard<int>(value);
            guard
                .ExceptionBuilderUsed(exBuilder);

            // Act
            var actualEx = Assert.Throws<InvalidOperationException>(() => guard.ValidateAndReturn());

            // Assert
            StringAssert.Contains(missingTestMessage, actualEx.Message);
        }


        //-------------------------------------------------------------------------
        //
        // Test Objects
        //
        //-------------------------------------------------------------------------

        private class TestObject
        {
            public string Name { get; set; }
        }
    }
}
