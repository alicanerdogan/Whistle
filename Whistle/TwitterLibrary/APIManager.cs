using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using TwitterLibrary.Operations;

namespace TwitterLibrary
{
    public class APIManager
    {
        private const string CONSUMER_KEY = "xl16P09KavpHIBq5iZ51r1FYE";
        private const string CONSUMER_SECRET = "JlFXhvZruwd4mzY8BSai8QGx773YwAZ3BGDFx9m5F3rwihgE0n";

        private static APIManager Instance { get; set; }
        public static APIManager GetManager()
        {
            if (Instance == null)
            {
                Instance = new APIManager();
            }
            return Instance;
        }

        private TwitterService Service { get; set; }
        private Authorizer Authorizer { get; set; }
        private Authenticator Authenticator { get; set; }
        private TimelineUpdater TimelineUpdater { get; set; }

        private APIManager()
        {
            Service = new TwitterService(CONSUMER_KEY, CONSUMER_SECRET);
            Authorizer = new Authorizer(Service);
            Authenticator = new Authenticator(Service);
            TimelineUpdater = new TimelineUpdater(Service);
        }

        public Uri GetAuthorizationUri()
        {
            return Authorizer.GetAuthorizationUri();
        }

        public bool Authenticate(string verifier)
        {
            Authenticator.Authenticate(Authorizer.GetAccessToken(verifier));
            return true;
        }

        public IEnumerable<ITweet> GetTimelineTweets()
        {
            return TweetFactory.CreateTweets(TimelineUpdater.GetLatest());
        }

        public IEnumerable<ITweet> GetTimelineTweetsBefore(long id)
        {
            return TweetFactory.CreateTweets(TimelineUpdater.GetBefore(id));
        }

        public IEnumerable<ITweet> GetTimelineTweetsAfter(long id)
        {
            return TweetFactory.CreateTweets(TimelineUpdater.GetAfter(id));
        }

        public IEnumerable<ITweet> GetUserTweets(long userId)
        {
            ITweetGetter tweetGetter = new UserTweetsGetter(Service, userId);
            return TweetFactory.CreateTweets(tweetGetter.GetLatest());
        }
    }
}
