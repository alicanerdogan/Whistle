using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder
{
    public interface IObjectHoarder<TKey, TItem>
    {
        TItem Get(TKey key);
    }

    public interface IObjectHoarderAsync<TKey, TItem> : IObjectHoarder<TKey, TItem>
    {
        Task<TItem> GetAsync(TKey key);
    }

    public interface IObjectComplexHoarder<TKeyProvider, TItem>
    {
        TItem Get(TKeyProvider keyProvider);
    }

    public interface IObjectComplexHoarderAsync<TKeyProvider, TItem> : IObjectComplexHoarder<TKeyProvider, TItem>
    {
        Task<TItem> GetAsync(TKeyProvider keyProvider);
    }
}
