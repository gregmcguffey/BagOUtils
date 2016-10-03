using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BagOUtils.ConfigurationCache
{
    /// <summary>
    /// Standard storage class used to cache values.
    /// </summary>
    public class Storage : IStorage
    {
        private readonly Hashtable storage;

        public Storage()
        {
            this.storage = new Hashtable();
        }

        /// <summary>
        /// Initialize a store for the indicated type.
        /// </summary>
        /// <typeparam name="T">Type of data store will store.</typeparam>
        public IStorage InitiaizeStore<T>()
        {
            var store = new Dictionary<string, T>();
            this.AddStore<T>(store);
            return this;
        }

        public int Count
        {
            get
            {
                return this.storage.Count;
            }
        }

        /// <summary>
        /// Add an existing store (a dictionary) of the indicated type.
        /// </summary>
        /// <typeparam name="T">Type of data in dictionary.</typeparam>
        /// <param name="store">Dictionary to add.</param>
        public IStorage AddStore<T>(Dictionary<string, T> store)
        {
            (store != null)
                .GuardIsTrue("The store (a dictionary with a string key), must be provided. It is null.");

            this.storage.Add(typeof(T), store);
            return this;
        }

        /// <summary>
        /// Clear all the stores.
        /// </summary>
        public void Clear()
        {
            this.storage.Clear();
        }

        /// <summary>
        /// Get the store for the indicated type.
        /// </summary>
        /// <typeparam name="T">Data type of store.</typeparam>
        public Dictionary<string, T> GetStore<T>()
        {
            (this.storage.ContainsKey(typeof(T)))
                .GuardIsTrue($"The type provided, '{typeof(T).ToString()}', is has not yet been initialized or added to this storage yet.");

            return (this.storage[typeof(T)]) as Dictionary<string, T>;
        }
    }
}
