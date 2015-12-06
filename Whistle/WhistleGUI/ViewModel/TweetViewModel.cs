using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TwitterLibrary;

namespace WhistleGUI.ViewModel
{
    public class TweetViewModel : ReactiveObject
    {
        #region Properties
        public Tweet Tweet { get; private set; }
        public string Content { get { return Tweet.RawContent; } }
        public string Username { get { return Tweet.Owner.Name; } }
        public string TimeTag { get { return Tweet.RelativeTime; } }
        public bool IsLiked { get { return false; } }
        public bool IsRetweeted { get { return false; } }
        public int RetweetCount { get { return Tweet.RetweetCount; } }
        public int ReplyCount { get { return Tweet.ReplyCount; } }
        public int LikeCount { get { return Tweet.LikeCount; } }

        private BitmapImage _Avatar;
        public BitmapImage Avatar
        {
            get { return _Avatar; }
            set { this.RaiseAndSetIfChanged(ref _Avatar, value); }
        }
        #endregion

        public TweetViewModel(Tweet tweet)
        {
            Tweet = tweet;
            LoadAvatar(Tweet.Owner.AvatarURL);
        }

        private async void LoadAvatar(string url)
        {
            var bitmap = await Helper.BitmapDownloader.DownloadAsync(url);
            Avatar = bitmap;
        }
    }
}
