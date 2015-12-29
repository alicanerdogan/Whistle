using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterLibrary.Operations
{
    public interface ITweetGetter
    {
        IEnumerable<TwitterStatus> GetLatest();
        IEnumerable<TwitterStatus> GetBefore(long id);
        IEnumerable<TwitterStatus> GetAfter(long id);
    }

    public abstract class TweetGetter<T> : APIOperation, ITweetGetter
    {
        public TweetGetter(TwitterService service) : base(service)
        {

        }

        public IEnumerable<TwitterStatus> GetLatest()
        {
            return GetLatestTemplate(GetOptionsToGetLatest());
        }
        public IEnumerable<TwitterStatus> GetBefore(long id)
        {
            return GetBeforeTemplate(GetOptionsToGetBefore(id));
        }
        public IEnumerable<TwitterStatus> GetAfter(long id)
        {
            return GetAfterTemplate(GetOptionsToGetBefore(id));
        }

        protected abstract IEnumerable<TwitterStatus> GetLatestTemplate(T options);
        protected abstract IEnumerable<TwitterStatus> GetBeforeTemplate(T options);
        protected abstract IEnumerable<TwitterStatus> GetAfterTemplate(T options);
        protected abstract T GetOptionsToGetLatest();
        protected abstract T GetOptionsToGetBefore(long id);
        protected abstract T GetOptionsToGetAfter(long id);
    }
}
