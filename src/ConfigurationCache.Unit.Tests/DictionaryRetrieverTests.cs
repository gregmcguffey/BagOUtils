using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BagOUtils.ConfigurationCache;

namespace ConfigurationCache.Unit.Tests
{
    [TestFixture]
    public class DictionaryRetrieverTests
    {
        //-------------------------------------------------------------------------
        //
        // ctor Tests
        //
        //-------------------------------------------------------------------------


        [Test]
        public void ctor_WithSource_ConstructsRetriever()
        {
            // Arrange
            var source = new Dictionary<string, string>();

            // Act & Assert
            Assert.DoesNotThrow(() => new DictionaryRetriever(source));
        }


        [Test]
        public void ctor_WithNoSource_ThrowsException()
        {
            // Arrange
            var missingSourceMessage = "No source dictionary was provided.";
            Dictionary<string, string> nullSource = null;

            // Act
            Assert.Throws<InvalidOperationException>(() => new DictionaryRetriever(nullSource));

            // Assert
        }

        //-------------------------------------------------------------------------
        //
        // RetrieveValue Tests
        //
        //-------------------------------------------------------------------------


        [Test]
        public void RetrieveValue_WithDefinedKey_ReturnsCorrectValue()
        {
            // Arrange
            var key = "3";
            var value = "Three";
            var source = new Dictionary<string, string>
            {
                ["1"] = "One",
                ["2"] = "Two",
                ["3"] = "Three",
            };
            var retriever = new DictionaryRetriever(source);

            // Act
            var actual = retriever.RetrieveValue<string>(TypeCode.String, key);

            // Assert
            Assert.AreEqual(value, actual);
        }


        [Test]
        public void RetrieveVAlue_WithUnsupportedTypeCode_ThrowsException()
        {
            // Arrange
            var unsupportedTypeCode = TypeCode.DBNull;
            var unsupportedTypeCodeMessage = $"The type code provided, '{unsupportedTypeCode.ToString()}', is not supported.";
            var key = "2";
            var source = new Dictionary<string, string>
            {
                ["1"] = "One",
                ["2"] = "Two",
                ["3"] = "Three",
            };
            var retriever = new DictionaryRetriever(source);

            // Act
            var ex = Assert.Throws<ArgumentException>(() => retriever.RetrieveValue<string>(unsupportedTypeCode, key));

            // Assert
            StringAssert.Contains(unsupportedTypeCodeMessage, ex.Message);
        }

        [Test]
        public void RetrieveValue_WithUndefinedKey_ThrowsException()
        {
            // Arrange
            var undefinedKeyMessage = "The key provided, '7', is not defined in the source dictionary.";
            var key = "7";
            var source = new Dictionary<string, string>
            {
                ["1"] = "One",
                ["2"] = "Two",
                ["3"] = "Three",
            };
            var retriever = new DictionaryRetriever(source);

            // Act
            var ex = Assert.Throws<ArgumentException>(() => retriever.RetrieveValue<string>(TypeCode.String, key));

            // Assert
            StringAssert.Contains(undefinedKeyMessage, ex.Message);
        }


        [Test]
        public void RetrieveValue_WithMismatchedTypes_ThrowsException()
        {
            var supportedType = TypeCode.String;
            var mismatchedTypesMessage = $"The type code, '{supportedType.ToString()}', and the generic type, 'System.Int32', do not match.";
            var key = "2";
            var source = new Dictionary<string, string>
            {
                ["1"] = "One",
                ["2"] = "Two",
                ["3"] = "Three",
            };
            var retriever = new DictionaryRetriever(source);

            // Act
            var ex = Assert.Throws<ArgumentException>(() => retriever.RetrieveValue<int>(supportedType, key));

            // Assert
            StringAssert.Contains(mismatchedTypesMessage, ex.Message);
        }

    }
}
