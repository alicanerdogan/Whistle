using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterLibrary.Operations
{
    public class Authenticator : APIOperation
    {
        public Authenticator(TwitterService service) : base(service)
        {
        }

        public void Authenticate(OAuthAccessToken token)
        {
            Service.AuthenticateWith(token.Token, token.TokenSecret);
        }


        public void Authenticate()
        {
            var token = Service.GetAccessToken(Service.GetRequestToken());
            Service.AuthenticateWith(token.Token, token.TokenSecret);
        }
    }
}
