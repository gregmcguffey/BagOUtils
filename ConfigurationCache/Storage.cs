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

        public IStorage AddStore<T>(Dictionary<string, T> store)
        {
            this.storage.Add(typeof(T), store);
            return this;
        }

        public void Clear()
        {
            this.storage.Clear();
        }

        public Dictionary<string, T> GetStore<T>()
        {
            return (this.storage[typeof(T)]) as Dictionary<string, T>;
        }

        public IStorage InitiaizeStore<T>()
        {
            var store = new Dictionary<string, T>();
            this.AddStore<T>(store);
            return this;
        }
    }
}
