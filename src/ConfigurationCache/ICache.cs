using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.ConfigurationCache
{

    /// <summary>
    /// Defines interface for a cache. The cache uses a retriever to
    /// get the values from some source, then caches those values
    /// into the a storage. The storages are all dictionaries with
    /// a string key and a value of any type.
    /// </summary>
    public interface ICache<TCategory>
    {
        ICache<TCategory> SetStorage(IStorage storage);
        ICache<TCategory> SetRetreiver(ICategoryRetriever<TCategory> retriever);
        ICache<TCategory> SetDefaultCategory(TCategory defaultCategory);

        TCategory DefaultCategory { get; }
        bool IsConfigured { get; }
        void ResetCache();

        T RetrieveAndCache<T>(string key);
        T RetrieveAndCache<T>(TCategory category, string key);
    }
}
