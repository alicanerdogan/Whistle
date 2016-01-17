using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder
{
    public class AlwaysValidPolicy : IHoardPolicy
    {
        public bool IsValid(IHoardItem item)
        {
            return true;
        }
    }
}
