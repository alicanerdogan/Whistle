using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using TwitterLibrary.Operations;

namespace TwitterLibrary.Operations
{
    public class UserTweetsGetter : TweetGetter<ListTweetsOnUserTimelineOptions>
    {
        private long UserId { get; set; }

        public UserTweetsGetter(TwitterService service, long userId) : base(service)
        {
            UserId = userId;
        }

        protected override IEnumerable<TwitterStatus> GetAfterTemplate(ListTweetsOnUserTimelineOptions options)
        {
            var tweets = Service.ListTweetsOnUserTimeline(options).ToList();
            if (tweets.Count > 0)
            {
                tweets.Remove(tweets.Last());
            }
            return tweets;
        }

        protected override IEnumerable<TwitterStatus> GetBeforeTemplate(ListTweetsOnUserTimelineOptions options)
        {
            var tweets = Service.ListTweetsOnUserTimeline(options).ToList();
            if (tweets.Count > 0)
            {
                tweets.RemoveAt(0);
            }
            return tweets;
        }

        protected override IEnumerable<TwitterStatus> GetLatestTemplate(ListTweetsOnUserTimelineOptions options)
        {
            return Service.ListTweetsOnUserTimeline(options);
        }

        protected override ListTweetsOnUserTimelineOptions GetOptionsToGetAfter(long id)
        {
            var options = new ListTweetsOnUserTimelineOptions();
            options.UserId = UserId;
            options.MaxId = null;
            options.SinceId = id;
            options.Count = null;
            return options;
        }

        protected override ListTweetsOnUserTimelineOptions GetOptionsToGetBefore(long id)
        {
            var options = new ListTweetsOnUserTimelineOptions();
            options.UserId = UserId;
            options.MaxId = id;
            options.SinceId = null;
            options.Count = 30;
            return options;
        }

        protected override ListTweetsOnUserTimelineOptions GetOptionsToGetLatest()
        {
            var options = new ListTweetsOnUserTimelineOptions();
            options.UserId = UserId;
            options.MaxId = null;
            options.SinceId = null;
            options.Count = 30;
            return options;
        }
    }
}
