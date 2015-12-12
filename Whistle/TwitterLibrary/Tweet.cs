using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using TwitterLibrary.Entity;

namespace TwitterLibrary
{
    public class Tweet : ITweet
    {
        public TweetType Type { get; set; }
        public long Id { get; set; }
        public TwitterUser Owner { get; set; }
        public TwitterUser Retweeter { get; set; }
        public DateTime TimeStamp { get; set; }
        public string RelativeTime { get; set; }
        public string RawContent { get; set; }
        public string Content { get; set; }
        public int ReplyCount { get; set; }
        public int LikeCount { get; set; }
        public int RetweetCount { get; set; }
        public IEnumerable<Media> Media { get; private set; }
        public IEnumerable<HashTag> HashTags { get; private set; }
        public IEnumerable<Mention> Mentions { get; private set; }
        public IEnumerable<URL> URLs { get; private set; }

        public Tweet()
        {
            Media = new List<Media>();
            HashTags = new List<HashTag>();
            Mentions = new List<Mention>();
            URLs = new List<URL>();
        }

        public void AddMedia(Media media)
        {
            ((List<Media>)Media).Add(media);
        }

        public void AddHashTag(HashTag hashtag)
        {
            ((List<HashTag>)HashTags).Add(hashtag);
        }

        public void AddMention(Mention mention)
        {
            ((List<Mention>)Mentions).Add(mention);
        }

        public void AddURL(URL url)
        {
            ((List<URL>)URLs).Add(url);
        }

    }
}
