using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterLibrary
{
    public class Tweet
    {
        public enum TweetType
        {
            NONE,
            REPLY,
            RETWEET
        }

        public TweetType Type { get; private set; }
        public long Id { get; private set; }
        public TwitterUser Owner { get; private set; }
        public TwitterUser Retweeter { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public string RelativeTime { get; private set; }
        public string RawContent { get; private set; }
        public string Content { get; private set; }
        public int ReplyCount { get; private set; }
        public int LikeCount { get; private set; }
        public int RetweetCount { get; private set; }

        public Tweet(TwitterStatus twitterStatus)
        {
            Id = twitterStatus.Id;
            RelativeTime = Helper.RelativeTime.Generate(twitterStatus.CreatedDate.ToLocalTime());
            if (twitterStatus.RetweetedStatus != null)
            {
                Type = TweetType.RETWEET;
                Owner = new TwitterUser(twitterStatus.RetweetedStatus.User);
                Retweeter = new TwitterUser(twitterStatus.User);
            }
            else
            {
                Type = TweetType.NONE;
                Owner = new TwitterUser(twitterStatus.User);
            }
            Content = twitterStatus.Text;
            RawContent = twitterStatus.TextAsHtml;
            RetweetCount = twitterStatus.RetweetCount;
            ReplyCount = 0;
        }

    }
}
