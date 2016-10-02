using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.ConfigurationCache
{
    /// <summary>
    /// Manages a cache of configuraiton values. The manager uses 
    /// a retriever to retrieve values from some persistent source
    /// and stores the values in a set of dictionaries that allow 
    /// strong typing of the values.
    /// </summary>
    public class Cache<TCategory> : ICache<TCategory>
    {
        public static ICache<TCategory> Create()
        {
            var cache = new Cache<TCategory>();
            return cache;
        }

        private IStorage storage;
        private ICategoryRetriever<TCategory> retriever;
        private TCategory defaultCategory;

        private Cache()
        {
        }


        //-------------------------------------------------------------------------
        //
        // Cache Management
        //
        //-------------------------------------------------------------------------

        public TCategory DefaultCategory
        {
            get
            {
                return this.defaultCategory;
            }
        }

        /// <summary>
        /// Return if the cache is configured with both storage and retriever.
        /// </summary>
        public bool IsConfigured
        {
            get
            {
                var isStorageSet = this.storage != null;
                var isRetrieverSet = this.retriever != null;
                return isStorageSet && isRetrieverSet;
            }
        }

        public void ResetCache()
        {
            (this.storage != null)
                .GuardIsTrue("Storage class is not set yet.");

            this.storage.Clear();
        }


        //-------------------------------------------------------------------------
        //
        // Fluent Setup
        //
        //-------------------------------------------------------------------------

        public ICache<TCategory> SetDefaultCategory(TCategory defaultCategory)
        {
            this.defaultCategory = defaultCategory;
            return this;
        }

        public ICache<TCategory> SetRetreiver(ICategoryRetriever<TCategory> retriever)
        {
            (retriever != null)
                .GuardIsTrue("A retriever class (ICategoryRetriever<>) is required.");

            this.retriever = retriever;
            return this;
        }

        public ICache<TCategory> SetStorage(IStorage storage)
        {
            (storage != null)
                .GuardIsTrue("A storage class (IStorage) is required.");

            this.storage = storage;
            return this;
        }


        //-------------------------------------------------------------------------
        //
        // Retrieve and Cache Values
        //
        //-------------------------------------------------------------------------

        public T RetrieveAndCache<T>(string key)
        {
            return this.RetrieveAndCache<T>(this.defaultCategory, key);
        }

        public T RetrieveAndCache<T>(TCategory category, string key)
        {
            string.IsNullOrWhiteSpace(key)
                .GuardIsFalse("A key for the cached item must be provided.");
            this.IsConfigured
                .GuardIsTrue("The cache is not configured with both storage and retriever.");

            return this.retriever.RetrieveValue<T>(category, key);
        }

    }
}
