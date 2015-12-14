using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using TwitterLibrary;
using WhistleGUI.Converter;

namespace WhistleGUI.ViewModel
{
    public class TweetViewModel : ReactiveObject, IRoutableViewModel
    {
        #region Properties
        public ITweet Tweet { get; private set; }
        public string Content { get { return Tweet.RawContent; } }
        public InlineCollection ProcessedContent { get { return MarkupTweetConverter.Convert(Tweet); } }
        public string Username { get { return Tweet.Owner.Username; } }
        public string DisplayName { get { return Tweet.Owner.DisplayName; } }
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

        public MultiMediaViewModel MultiMediaViewModel { get; private set; }

        public ReactiveCommand<TwitterUserViewModel> RouteToUser { get; private set; }
        #endregion

        public TweetViewModel(IScreen screen, ITweet tweet)
        {
            HostScreen = screen;
            Tweet = tweet;
            LoadAvatar(Tweet.Owner.AvatarURL);
            MultiMediaViewModel = new MultiMediaViewModel(Tweet.Media);

            RouteToUser = ReactiveCommand.CreateAsyncTask<TwitterUserViewModel>(_ => Task.Run(() => new TwitterUserViewModel(HostScreen, Tweet.Owner)));
            RouteToUser.SubscribeOn(RxApp.MainThreadScheduler).Subscribe(vm => HostScreen.Router.Navigate.Execute(vm));
        }

        private async void LoadAvatar(string url)
        {
            var bitmap = await Helper.BitmapDownloader.DownloadAsync(url);
            Avatar = bitmap;
        }

        #region IRoutableView Extension
        public string UrlPathSegment { get { return "Tweet"; } }
        public IScreen HostScreen { get; protected set; }
        #endregion
    }
}
