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
        private const string CONSUMER_KEY = "Ocx6LOTBF1OBwkmFkor1zvHPT";
        private const string CONSUMER_SECRET = "afpnIjhNja8TtZmRwugX5oaClYD8Y8BDljzA0sPpyQwIGNNrAg";

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

        public IEnumerable<TwitterStatus> GetTimelineTweets()
        {
            return TimelineUpdater.Update();
        }
    }
}
