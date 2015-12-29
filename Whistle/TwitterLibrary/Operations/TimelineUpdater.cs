using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterLibrary.Operations
{
    public class TimelineUpdater : TweetGetter<ListTweetsOnHomeTimelineOptions>
    {
        public TimelineUpdater(TwitterService service) : base(service)
        {
        }

        protected override IEnumerable<TwitterStatus> GetLatestTemplate(ListTweetsOnHomeTimelineOptions options)
        {
            List<TwitterStatus> tweets = new List<TwitterStatus>();

            var currentTweets = Service.ListTweetsOnHomeTimeline(options);
            if (currentTweets == null)
            {
                throw new Exception("Connection Error!");
            }

            tweets.AddRange(currentTweets);


            while (tweets.Count < 30)
            {
                options.MaxId = tweets.Last().Id;
                var olderTweets = Service.ListTweetsOnHomeTimeline(options);
                if (olderTweets?.ToList().Count == 0)
                {
                    break;
                }
                tweets.Remove(tweets.Last());
                tweets.AddRange(olderTweets);
            }

            return tweets;
        }

        protected override IEnumerable<TwitterStatus> GetBeforeTemplate(ListTweetsOnHomeTimelineOptions options)
        {
            List<TwitterStatus> tweets = new List<TwitterStatus>();
            tweets.AddRange(Service.ListTweetsOnHomeTimeline(options));
            if (tweets.Count > 0)
            {
                tweets.RemoveAt(0);
            }
            return tweets;
        }

        protected override IEnumerable<TwitterStatus> GetAfterTemplate(ListTweetsOnHomeTimelineOptions options)
        {
            List<TwitterStatus> tweets = new List<TwitterStatus>();
            tweets.AddRange(Service.ListTweetsOnHomeTimeline(options));
            if (tweets.Count > 0)
            {
                tweets.Remove(tweets.Last());
            }
            return tweets;
        }

        protected override ListTweetsOnHomeTimelineOptions GetOptionsToGetLatest()
        {
            var options = new ListTweetsOnHomeTimelineOptions() { ExcludeReplies = false };
            options.MaxId = null;
            options.SinceId = null;
            options.Count = 30;
            return options;
        }

        protected override ListTweetsOnHomeTimelineOptions GetOptionsToGetBefore(long id)
        {
            var options = new ListTweetsOnHomeTimelineOptions() { ExcludeReplies = false };
            options.MaxId = id;
            options.SinceId = null;
            options.Count = 30;
            return options;
        }

        protected override ListTweetsOnHomeTimelineOptions GetOptionsToGetAfter(long id)
        {
            var options = new ListTweetsOnHomeTimelineOptions() { ExcludeReplies = false };
            options.MaxId = null;
            options.SinceId = id;
            options.Count = null;
            return options;
        }
    }
}
