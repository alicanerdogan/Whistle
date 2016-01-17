using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder
{
    public class ObjectHoardContainer<TKey, TItem> : IHoardContainer<TKey, TItem>
    {
        private Dictionary<TKey, IHoardItem<TItem>> ObjectDictionary { get; set; }

        public ObjectHoardContainer()
        {
            ObjectDictionary = new Dictionary<TKey, IHoardItem<TItem>>();
        }

        public bool Contains(TKey key)
        {
            return ObjectDictionary.ContainsKey(key);
        }

        public IHoardItem<TItem> Get(TKey key)
        {
            return ObjectDictionary[key];
        }

        public void Set(TKey key, IHoardItem<TItem> item)
        {
            ObjectDictionary[key] = item;
        }
    }
}
