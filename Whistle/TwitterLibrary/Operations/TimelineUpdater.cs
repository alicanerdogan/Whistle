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
            Options = new ListTweetsOnHomeTimelineOptions() { Count = 1, ExcludeReplies = false };
        }

        public IEnumerable<TwitterStatus> Update()
        {
            return Service.ListTweetsOnHomeTimeline(Options);
        }
    }
}
