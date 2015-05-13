using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTLibrary
{
    public static class Request
    {
        public static void Action()
        {
            var client = new RestClient("https://api.twitter.com");
            var request = new RestRequest("1.1/statuses/home_timeline.json", Method.GET);
            var queryResult = client.Execute(request).Content;
        }
    }
}
