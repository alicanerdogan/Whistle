using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterLibrary
{
    public abstract class APIOperation
    {
        private readonly TwitterService service_;
        protected TwitterService Service
        {
            get
            {
                return service_;
            }
        }

        public APIOperation(TwitterService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("Twitter Service cannot be null.");
            }
            service_ = service;
        }
    }
}
