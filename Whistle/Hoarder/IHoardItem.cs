using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder
{
    public interface IHoardItem
    {
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
        DateTime AccessedAt { get; }
    }

    public interface IHoardItem<TItem> : IHoardItem
    {
        TItem Item { get; }
    }
}
