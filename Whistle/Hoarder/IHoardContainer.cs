using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder
{
    public interface IHoardContainer<TKey, TItem>
    {
        IHoardItem<TItem> Get(TKey key);
        void Set(TKey key, IHoardItem<TItem> item);
        bool Contains(TKey key);
    }
}
