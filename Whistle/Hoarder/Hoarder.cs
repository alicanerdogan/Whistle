using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hoarder
{
    public abstract class BaseHoarder<TKey, TItem>
    {
        protected IHoardContainer<TKey, TItem> Container { get; private set; }
        protected IHoardPolicy Policy { get; private set; }

        public BaseHoarder(IHoardContainer<TKey, TItem> hoardContainer, IHoardPolicy hoardPolicy)
        {
            Container = hoardContainer;
            Policy = hoardPolicy;
        }

        public BaseHoarder(IHoardContainer<TKey, TItem> hoardContainer)
        {
            Container = hoardContainer;
            Policy = new AlwaysValidPolicy();
        }

    }

    public abstract class Hoarder<TProviderKey, TKey, TItem> : BaseHoarder<TKey, TItem>
    {
        protected Func<TProviderKey, TItem> Provider { get; private set; }

        public Hoarder(IHoardContainer<TKey, TItem> hoardContainer, Func<TProviderKey, TItem> hoardProvider) : base(hoardContainer)
        {
            Provider = hoardProvider;
        }

        public Hoarder(IHoardContainer<TKey, TItem> hoardContainer, Func<TProviderKey, TItem> hoardProvider, IHoardPolicy hoardPolicy) : base(hoardContainer, hoardPolicy)
        {
            Provider = hoardProvider;
        }
    }

    public class ObjectHoarder<TKey, TItem> : Hoarder<TKey, TKey, TItem>, IObjectHoarder<TKey, TItem>
    {
        public ObjectHoarder(IHoardContainer<TKey, TItem> hoardContainer, Func<TKey, TItem> hoardProvider) : base(hoardContainer, hoardProvider)
        {
        }

        public ObjectHoarder(IHoardContainer<TKey, TItem> hoardContainer, Func<TKey, TItem> hoardProvider, IHoardPolicy hoardPolicy) : base(hoardContainer, hoardProvider, hoardPolicy)
        {
        }

        public TItem Get(TKey key)
        {
            IHoardItem<TItem> hoardedItem;
            if (Container.Contains(key))
            {
                hoardedItem = Container.Get(key);
                if (!Policy.IsValid(hoardedItem))
                {
                    hoardedItem = CreateHoardItem(key);
                }
            }
            else
            {
                hoardedItem = CreateHoardItem(key);
            }
            return hoardedItem.Item;
        }

        protected virtual IHoardItem<TItem> CreateHoardItem(TKey key)
        {
            var newItem = Provider(key);
            // TODO: Remove dependency for HoardObjectItem
            var hoardedItem = new HoardObjectItem<TItem>(newItem);
            Container.Set(key, hoardedItem);
            return hoardedItem;
        }
    }

    public abstract class HoarderAsync<TProviderKey, TKey, TItem> : BaseHoarder<TKey, TItem>
    {
        protected Func<TProviderKey, Task<TItem>> Provider { get; private set; }

        public HoarderAsync(IHoardContainer<TKey, TItem> hoardContainer, Func<TProviderKey, Task<TItem>> asyncHoardProvider) : base(hoardContainer)
        {
            Provider = asyncHoardProvider;
        }

        public HoarderAsync(IHoardContainer<TKey, TItem> hoardContainer, Func<TProviderKey, Task<TItem>> asyncHoardProvider, IHoardPolicy hoardPolicy) : base(hoardContainer, hoardPolicy)
        {
            Provider = asyncHoardProvider;
        }
    }

    public class ObjectHoarderAsync<TKey, TItem> : HoarderAsync<TKey, TKey, TItem>, IObjectHoarderAsync<TKey, TItem>
    {
        public ObjectHoarderAsync(IHoardContainer<TKey, TItem> hoardContainer, Func<TKey, Task<TItem>> asyncHoardProvider) : base(hoardContainer, asyncHoardProvider)
        {
        }

        public ObjectHoarderAsync(IHoardContainer<TKey, TItem> hoardContainer, Func<TKey, Task<TItem>> asyncHoardProvider, IHoardPolicy hoardPolicy) : base(hoardContainer, asyncHoardProvider, hoardPolicy)
        {
        }

        public TItem Get(TKey key)
        {
            IHoardItem<TItem> hoardedItem;
            if (Container.Contains(key))
            {
                hoardedItem = Container.Get(key);
                if (!Policy.IsValid(hoardedItem))
                {
                    hoardedItem = CreateHoardItemAsync(key).Result;
                }
            }
            else
            {
                hoardedItem = CreateHoardItemAsync(key).Result;
            }
            return hoardedItem.Item;
        }

        public async Task<TItem> GetAsync(TKey key)
        {
            IHoardItem<TItem> hoardedItem;
            if (Container.Contains(key))
            {
                hoardedItem = Container.Get(key);
                if (!Policy.IsValid(hoardedItem))
                {
                    hoardedItem = await CreateHoardItemAsync(key);
                }
            }
            else
            {
                hoardedItem = await CreateHoardItemAsync(key);
            }
            return hoardedItem.Item;
        }

        protected virtual async Task<IHoardItem<TItem>> CreateHoardItemAsync(TKey key)
        {
            var newItem = await Provider(key);
            // TODO: Remove dependency for HoardObjectItem
            var hoardedItem = new HoardObjectItem<TItem>(newItem);
            Container.Set(key, hoardedItem);
            return hoardedItem;
        }
    }

    public abstract class BaseObjectComplexHoarderAsync<TProviderKey, TKey, TItem> : HoarderAsync<TProviderKey, TKey, TItem>
    {
        protected Func<TProviderKey, TKey> KeyProvider { get; private set; }

        public BaseObjectComplexHoarderAsync(IHoardContainer<TKey, TItem> hoardContainer, Func<TProviderKey, Task<TItem>> asyncHoardProvider, Func<TProviderKey, TKey> keyProvider) : base(hoardContainer, asyncHoardProvider)
        {
            KeyProvider = keyProvider;
        }

        public BaseObjectComplexHoarderAsync(IHoardContainer<TKey, TItem> hoardContainer, Func<TProviderKey, Task<TItem>> asyncHoardProvider, Func<TProviderKey, TKey> keyProvider, IHoardPolicy hoardPolicy) : base(hoardContainer, asyncHoardProvider, hoardPolicy)
        {
            KeyProvider = keyProvider;
        }
    }

    public class ObjectComplexHoarderAsync<TProviderKey, TKey, TItem> : BaseObjectComplexHoarderAsync<TProviderKey, TKey, TItem>, IObjectComplexHoarderAsync<TProviderKey, TItem>
    {
        public ObjectComplexHoarderAsync(IHoardContainer<TKey, TItem> hoardContainer, Func<TProviderKey, Task<TItem>> asyncHoardProvider, Func<TProviderKey, TKey> keyProvider) : base(hoardContainer, asyncHoardProvider, keyProvider)
        {
        }

        public ObjectComplexHoarderAsync(IHoardContainer<TKey, TItem> hoardContainer, Func<TProviderKey, Task<TItem>> asyncHoardProvider, Func<TProviderKey, TKey> keyProvider, IHoardPolicy hoardPolicy) : base(hoardContainer, asyncHoardProvider, keyProvider, hoardPolicy)
        {
        }

        public TItem Get(TProviderKey keyProvider)
        {
            var key = KeyProvider(keyProvider);
            IHoardItem<TItem> hoardedItem;
            if (Container.Contains(key))
            {
                hoardedItem = GetExistingHoardItem(key);
                if (!Policy.IsValid(hoardedItem))
                {
                    //Semaphore locker;
                    //try
                    //{
                    //    locker = Semaphore.OpenExisting(key.GetHashCode().ToString());
                    //}
                    //catch (Exception)
                    //{
                    //    locker = new Semaphore(1, 1, key.GetHashCode().ToString());
                    //}
                    //locker.WaitOne();
                    hoardedItem = CreateHoardItem(keyProvider, key).Result;
                    //locker.Dispose();
                    //locker.Release();
                }
            }
            else
            {
                hoardedItem = CreateHoardItem(keyProvider, key).Result;
            }
            return hoardedItem.Item;
        }

        public async Task<TItem> GetAsync(TProviderKey keyProvider)
        {
            var key = KeyProvider(keyProvider);
            IHoardItem<TItem> hoardedItem;
            if (Container.Contains(key))
            {
                hoardedItem = GetExistingHoardItem(key);
                if (!Policy.IsValid(hoardedItem))
                {
                    hoardedItem = await CreateHoardItem(keyProvider, key);
                }
            }
            else
            {
                hoardedItem = await CreateHoardItem(keyProvider, key);
            }
            return hoardedItem.Item;
        }

        protected virtual IHoardItem<TItem> GetExistingHoardItem(TKey key)
        {
            return Container.Get(key);
        }

        protected virtual async Task<IHoardItem<TItem>> CreateHoardItem(TProviderKey keyProvider, TKey key)
        {
            var newItem = await Provider(keyProvider);
            // TODO: Remove dependency for HoardObjectItem
            var hoardedItem = new HoardObjectItem<TItem>(newItem);
            Container.Set(key, hoardedItem);
            return hoardedItem;
        }
    }
}
