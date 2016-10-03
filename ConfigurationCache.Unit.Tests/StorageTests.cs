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
    public class StorageTests
    {

        [Test]
        public void InitializeStore_WhenCalled_AddsCorrectStore()
        {
            // Arrange
            var storage = new Storage();

            // Act
            storage.InitiaizeStore<string>();

            // Assert
            Assert.AreEqual(1, storage.Count);
            var actual = storage.GetStore<string>();
            Assert.IsInstanceOf<Dictionary<string, string>>(actual);
        }

        [Test]
        public void AddStore_WithStore_AddsStore()
        {
            // Arrange
            var store = new Dictionary<string, string>();
            var storage = new Storage();

            // Act
            storage.AddStore(store);

            // Assert
            Assert.AreEqual(1, storage.Count);
        }


        [Test]
        public void AddStore_WithNullStore_ThrowsException()
        {
            // Arrange
            var missingStoreMessage = "The store (a dictionary with a string key), must be provided. It is null.";
            Dictionary<string, string> nullStore = null;
            var storage = new Storage();

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => storage.AddStore(nullStore));

            // Assert
            StringAssert.Contains(missingStoreMessage, ex.Message);
        }


        [Test]
        public void Clear_WhenCalled_ClearsAllStores()
        {
            var stringStore = new Dictionary<string, string>();
            var intStore = new Dictionary<string, int>();
            var storage = new Storage();

            storage
                .AddStore(stringStore)
                .AddStore(intStore);

            // Act
            storage.Clear();

            // Assert
            Assert.AreEqual(0, storage.Count);
        }


        [Test]
        public void GetStore_WithValidType_ReturnsCorrectStore()
        {
            // Arrange
            var stringStore = new Dictionary<string, string>();
            var intStore = new Dictionary<string, int>();
            var storage = new Storage();

            storage
                .AddStore(stringStore)
                .AddStore(intStore);

            // Act
            var retrievedStore = storage.GetStore<int>();

            // Assert
            Assert.AreSame(intStore, retrievedStore);
        }


        [Test]
        public void GetStore_WithInvalidType_ThrowsException()
        {
            // Arrange
            var typeNotFoundMessage = "The type provided, 'System.Int32', is has not yet been initialized or added to this storage yet.";
            var stringStore = new Dictionary<string, string>();
            var intStore = new Dictionary<string, int>();
            var storage = new Storage();

            storage
                .AddStore(stringStore);

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => storage.GetStore<int>());

            // Assert
            StringAssert.Contains(typeNotFoundMessage, ex.Message);
        }
    }
}
