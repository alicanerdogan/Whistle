using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterLibrary.Operations
{
    public class Authorizer : APIOperation
    {
        private OAuthRequestToken RequestToken { get; set; }

        public Authorizer(TwitterService service) : base(service)
        {
            RequestToken = Service.GetRequestToken();
        }

        public Uri GetAuthorizationUri()
        {
            return Service.GetAuthorizationUri(RequestToken);
        }

        public OAuthAccessToken GetAccessToken(string verifier)
        {
            return Service.GetAccessToken(RequestToken, verifier);
        }
    }
}
