using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterLibrary.Entity
{
    public class HashTag : URL
    {
        public HashTag(string text) : base("https://twitter.com/hashtag/" + text, null, "#" + text)
        {

        }
    }
}
