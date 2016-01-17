using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder
{
    public class TimeExpirationPolicy : IHoardPolicy
    {
        public TimeSpan ShelfLife { get; private set; }

        public TimeExpirationPolicy(TimeSpan shelfLife)
        {
            ShelfLife = shelfLife;
        }

        public bool IsValid(IHoardItem item)
        {
            return (DateTime.Now - item.CreatedAt < ShelfLife);
        }
    }
}
