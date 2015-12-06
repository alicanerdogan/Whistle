using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterLibrary.Operations
{
    public class TimelineUpdater : APIOperation
    {
        private ListTweetsOnHomeTimelineOptions Options { get; set; }

        public TimelineUpdater(TwitterService service) : base(service)
        {
            Options = new ListTweetsOnHomeTimelineOptions() { ExcludeReplies = false };
        }

        public IEnumerable<TwitterStatus> Update()
        {
            List<TwitterStatus> tweets = new List<TwitterStatus>();
            Options.MaxId = null;
            Options.SinceId = null;
            Options.Count = 30;

            tweets.AddRange(Service.ListTweetsOnHomeTimeline(Options));

            while (tweets.Count < 30)
            {
                Options.MaxId = tweets.Last().Id;
                var olderTweets = Service.ListTweetsOnHomeTimeline(Options);
                tweets.Remove(tweets.Last());
                tweets.AddRange(olderTweets);
            }

            return tweets;
        }

        public IEnumerable<TwitterStatus> UpdateBefore(long id)
        {
            Options.MaxId = id;
            Options.SinceId = null;
            Options.Count = 30;

            List<TwitterStatus> tweets = new List<TwitterStatus>();
            tweets.AddRange(Service.ListTweetsOnHomeTimeline(Options));
            if (tweets.Count > 0)
            {
                tweets.RemoveAt(0);
            }
            return tweets;
        }

        public IEnumerable<TwitterStatus> UpdateAfter(long id)
        {
            Options.MaxId = null;
            Options.SinceId = id;
            Options.Count = null;

            List<TwitterStatus> tweets = new List<TwitterStatus>();
            tweets.AddRange(Service.ListTweetsOnHomeTimeline(Options));
            if (tweets.Count > 0)
            {
                tweets.Remove(tweets.Last());
            }
            return tweets;
        }
    }
}
