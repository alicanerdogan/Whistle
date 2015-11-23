using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;

namespace WhistleGUI.ViewModel
{
    public class TweetViewModel : ReactiveObject
    {
        #region Properties
        public TwitterStatus Tweet { get; private set; }
        public string Content { get { return Tweet.Text; } }
        public string Username { get { return Tweet.User.Name; } }
        public string TimeTag { get { return Tweet.CreatedDate.ToString(); } }
        public bool IsLiked { get { return Tweet.IsFavorited; } }
        public bool IsRetweeted { get { return false; } }
        public int RetweetCount { get { return Tweet.RetweetCount; } }
        public int ReplyCount { get { return 0; } }
        public int LikeCount { get { return 0; } }
        #endregion

        public TweetViewModel(TwitterStatus status)
        {
            Tweet = status;
        }
    }
}
