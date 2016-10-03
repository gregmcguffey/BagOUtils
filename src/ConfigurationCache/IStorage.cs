using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.ConfigurationCache
{
    /// <summary>
    /// Defines the interface to the storage used by a cache.
    /// This manages the cached values.
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Initialize a blank store for the indicated type.
        /// </summary>
        /// <typeparam name="T">Type of store desired.</typeparam>
        IStorage InitiaizeStore<T>();

        /// <summary>
        /// Add an existing store of the indicated type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="store"></param>
        IStorage AddStore<T>(Dictionary<string, T> store);

        /// <summary>
        /// Returns count of stores currently defined.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Get the store of the indicated type.
        /// </summary>
        /// <typeparam name="T">Type of store to retrieve.</typeparam>
        Dictionary<string, T> GetStore<T>();

        /// <summary>
        /// Clear all the stored values.
        /// </summary>
        void Clear();
    }
}
