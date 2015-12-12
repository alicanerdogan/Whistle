using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterLibrary.Entity
{
    public class Mention : URL
    {
        public TwitterUser To { get; private set; }
        public string Name { get; private set; }

        public Mention(string name) : base("https://twitter.com/" + name, null, "@" + name)
        {
            Name = name;
        }
    }
}
