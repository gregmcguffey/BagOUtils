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
        private IStorage storage;
        private ICategoryRetriever<TCategory> retriever;
        private TCategory defaultCategory;

        public Cache()
        {
        }


        //-------------------------------------------------------------------------
        //
        // Cache Management
        //
        //-------------------------------------------------------------------------

        public void ResetCache()
        {
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
            this.retriever = retriever;
            return this;
        }

        public ICache<TCategory> SetStorage(IStorage storage)
        {
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
            return this.retriever.RetrieveValue<T>(this.defaultCategory, key);
        }

        public T RetrieveAndCache<T>(TCategory category, string key)
        {
            return this.retriever.RetrieveValue<T>(category, key);
        }

    }
}
