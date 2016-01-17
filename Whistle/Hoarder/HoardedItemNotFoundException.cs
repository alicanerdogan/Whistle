using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder
{
    public class HoardedItemNotFoundException<TKey> : Exception
    {
        public TKey Key { get; private set; }
        public override string Message
        {
            get
            {
                return "HoardedItem searched by key value '" + Key.ToString() + "' not found.";
            }
        }

        public HoardedItemNotFoundException(TKey key)
        {
            Key = key;
        }
    }
}
