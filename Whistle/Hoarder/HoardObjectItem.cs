using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder
{
    public class HoardObjectItem<TItem> : IHoardItem<TItem>
    {
        public TItem Item { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public DateTime AccessedAt
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public HoardObjectItem(TItem item)
        {
            Item = item;
            CreatedAt = DateTime.Now;
        }
    }
}
