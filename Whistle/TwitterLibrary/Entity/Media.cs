using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterLibrary.Entity
{
    public class Media : URL
    {
        public enum TwitterMediaType
        {
            Photo = 0
        }

        public TwitterMediaType MediaType { get; set; }

        public Media(TwitterMedia media) : base(media.MediaUrl, "http://" + media.DisplayUrl, media.Url)
        {
            MediaType = TwitterMediaType.Photo;
        }
    }
}
