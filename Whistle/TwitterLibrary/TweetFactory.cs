using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using TwitterLibrary.Entity;

namespace TwitterLibrary
{
    public static class TweetFactory
    {
        private static ITweet CreateTweet(TwitterStatus twitterStatus)
        {
            var tweet = new Tweet();

            tweet.Id = twitterStatus.Id;
            tweet.RelativeTime = Helper.RelativeTime.Generate(twitterStatus.CreatedDate.ToLocalTime());
            if (twitterStatus.RetweetedStatus != null)
            {
                tweet.Type = TweetType.RETWEET;
                tweet.Retweeter = new TwitterUser(twitterStatus.User);
                twitterStatus = twitterStatus.RetweetedStatus;
            }
            else
            {
                tweet.Type = TweetType.NONE;
            }
            tweet.Owner = new TwitterUser(twitterStatus.User);
            tweet.Content = twitterStatus.Text;
            tweet.RawContent = twitterStatus.TextAsHtml;
            tweet.RetweetCount = twitterStatus.RetweetCount;
            tweet.ReplyCount = 0;

            twitterStatus.Entities.Media.ToList().ForEach(m => tweet.AddMedia(new Media(m)));
            twitterStatus.Entities.HashTags.ToList().ForEach(h => tweet.AddHashTag(new HashTag(h.Text)));
            twitterStatus.Entities.Mentions.ToList().ForEach(m => tweet.AddMention(new Mention(m.ScreenName)));
            twitterStatus.Entities.Urls.ToList().ForEach(u => tweet.AddURL(new URL(u.ExpandedValue, u.Value)));

            return tweet;
        }

        public static IEnumerable<ITweet> CreateTweets(IEnumerable<TwitterStatus> twitterStatuses)
        {
            var tweets = new List<ITweet>();
            foreach (var twitterStatus in twitterStatuses)
            {
                if (twitterStatus.User != null)
                {
                    tweets.Add(CreateTweet(twitterStatus));
                }
            }
            return tweets;
        }
    }
}
