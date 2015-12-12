using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterLibrary.Entity
{
    public class URL
    {
        public string LinkText { get; set; }
        public Uri ShortLink { get; private set; }
        public Uri Destination { get; private set; }

        public URL(string destination, string shortLink)
        {
            ShortLink = new Uri(shortLink);
            Destination = new Uri(destination);
            LinkText = ShortLink.ToString();
        }

        public URL(string destination, string shortLink, string linkText)
        {
            if (!String.IsNullOrEmpty(shortLink))
            {
                ShortLink = new Uri(shortLink);
            }
            Destination = new Uri(destination);
            LinkText = linkText;
        }
    }
}
