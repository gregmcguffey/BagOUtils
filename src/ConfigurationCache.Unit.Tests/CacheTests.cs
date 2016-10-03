using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FakeItEasy;
using BagOUtils.ConfigurationCache;

namespace ConfigurationCache.Unit.Tests
{
    [TestFixture]
    public class CacheTests
    {
        //-------------------------------------------------------------------------
        //
        // Fluent Creation Tests
        //
        //-------------------------------------------------------------------------

        [Test]
        public void FluentSetup_WhenNoExplicitDefault_UsesDefaultOfType()
        {
            // Arrange
            var storage = A.Fake<IStorage>();
            var retriever = A.Fake<ICategoryRetriever<string>>();

            var expectedDefault = default(string);

            // Act
            var cache = Cache<string>
                .Create()
                .SetStorage(storage)
                .SetRetreiver(retriever);

            // Assert
            Assert.IsTrue(cache.IsConfigured);
            Assert.AreEqual(expectedDefault, cache.DefaultCategory);
        }


        [Test]
        public void FluentSetup_WithExplicitDefalut_SetsTheDefault()
        {
            // Arrange
            var storage = A.Fake<IStorage>();
            var retriever = A.Fake<ICategoryRetriever<string>>();

            var expectedDefault = "any";

            // Act
            var cache = Cache<string>
                .Create()
                .SetStorage(storage)
                .SetRetreiver(retriever)
                .SetDefaultCategory("any");

            // Assert
            Assert.IsTrue(cache.IsConfigured);
            Assert.AreEqual(expectedDefault, cache.DefaultCategory);
        }


        //-------------------------------------------------------------------------
        //
        // Cache Management Tests
        //
        //-------------------------------------------------------------------------

        [Test]
        public void IsConfigured_WhenConfigured_ReturnsTrue()
        {
            // Arrange
            var storage = A.Fake<IStorage>();
            var retriever = A.Fake<ICategoryRetriever<string>>();

            var cache = Cache<string>
                .Create()
                .SetStorage(storage)
                .SetRetreiver(retriever);

            // Act
            var actual = cache.IsConfigured;

            // Assert
            Assert.IsTrue(actual);
        }


        [Test]
        public void IsConfigured_WhenOnlyStorageConfigured_ReturnsFalse()
        {
            // Arrange
            var storage = A.Fake<IStorage>();
            var retriever = A.Fake<ICategoryRetriever<string>>();

            var cache = Cache<string>
                .Create()
                .SetStorage(storage);

            // Act
            var actual = cache.IsConfigured;

            // Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void IsConfigured_WhenOnlyRetrieverConfigured_ReturnsFalse()
        {
            // Arrange
            var storage = A.Fake<IStorage>();
            var retriever = A.Fake<ICategoryRetriever<string>>();

            var cache = Cache<string>
                .Create()
                .SetRetreiver(retriever);

            // Act
            var actual = cache.IsConfigured;

            // Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void ResetStorage_WhenCalled_CallsStorageClear()
        {
            // Arrange
            var storage = A.Fake<IStorage>();
            var retriever = A.Fake<ICategoryRetriever<string>>();

            var cache = Cache<string>
                .Create()
                .SetStorage(storage)
                .SetRetreiver(retriever);

            // Act
            cache.ResetCache();

            // Assert
            A.CallTo(() => storage.Clear())
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void ResetStorage_WithNotStorageSet_ThrowsException()
        {
            // Arrange
            var noStorageMessage = "Storage class is not set yet.";
            var storage = A.Fake<IStorage>();
            var retriever = A.Fake<ICategoryRetriever<string>>();

            var cache = Cache<string>
                .Create()
                .SetRetreiver(retriever);

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => cache.ResetCache());

            // Assert
            StringAssert.Contains(noStorageMessage, ex.Message);
        }


        //-------------------------------------------------------------------------
        //
        // Retrieve and Cache Values Tests
        //
        //-------------------------------------------------------------------------


        [Test]
        public void RetrieveAndCache_WithKey_CallsRetrieverCorrectly()
        {
            // Arrange
            var storage = A.Fake<IStorage>();
            var retriever = A.Fake<ICategoryRetriever<string>>();

            var categoryDefalut = "anyCategory";
            var key = "anyKey";

            var cache = Cache<string>
                .Create()
                .SetStorage(storage)
                .SetRetreiver(retriever)
                .SetDefaultCategory(categoryDefalut);

            // Act
            cache.RetrieveAndCache<string>(key);

            // Assert
            A.CallTo(() => retriever.RetrieveValue<string>(categoryDefalut, key))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void RetrieveAndCache_WithCategoryAndKey_CallsRetrieverCorrectly()
        {
            // Arrange
            var storage = A.Fake<IStorage>();
            var retriever = A.Fake<ICategoryRetriever<string>>();

            var category = "anyCategory";
            string key = "anyKey";

            var cache = Cache<string>
                .Create()
                .SetStorage(storage)
                .SetRetreiver(retriever);

            // Act
            cache.RetrieveAndCache<string>(category, key);

            // Assert
            A.CallTo(() => retriever.RetrieveValue<string>(category, key))
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void RetrieveAndCache_WithEmptyKey_ThrowsException(string key)
        {
            // Arrange
            var missingKeyMessage = "A key for the cached item must be provided.";
            var storage = A.Fake<IStorage>();
            var retriever = A.Fake<ICategoryRetriever<string>>();

            var category = "anyCategory";

            var cache = Cache<string>
                .Create()
                .SetStorage(storage)
                .SetRetreiver(retriever);

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => cache.RetrieveAndCache<string>(category, key));

            // Assert
            StringAssert.Contains(missingKeyMessage, ex.Message);
        }

        [Test]
        public void RetrieveAndCache_WhenNotConfigured_ThrowsException()
        {
            // Arrange
            var notConfiguredMessage = "The cache is not configured with both storage and retriever.";
            var storage = A.Fake<IStorage>();
            var retriever = A.Fake<ICategoryRetriever<string>>();

            var category = "anyCategory";
            string key = "anyKey";

            var cache = Cache<string>
                .Create()
                .SetRetreiver(retriever);

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => cache.RetrieveAndCache<string>(category, key));

            // Assert
            StringAssert.Contains(notConfiguredMessage, ex.Message);
        }

    }
}
