using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TweetSharp;
using TwitterLibrary;

namespace WhistleGUI.ViewModel
{
    public class TimelineListViewModel : ReactiveObject, IRoutableViewModel
    {
        #region Commands
        public ReactiveCommand<IEnumerable<TwitterStatus>> Refresh { get; private set; }
        #endregion

        #region Properties
        private TweetViewModel _TweetViewModel;
        public TweetViewModel TweetViewModel
        {
            get { return _TweetViewModel; }
            set { this.RaiseAndSetIfChanged(ref _TweetViewModel, value); }
        }

        public ReactiveList<TwitterStatus> TimelineTweets { get; private set; }
        #endregion

        public TimelineListViewModel(IScreen screen)
        {
            HostScreen = screen;

            TimelineTweets = new ReactiveList<TwitterStatus>();
            Refresh = ReactiveCommand.CreateAsyncTask<IEnumerable<TwitterStatus>>(_ => Task.Run(() => APIManager.GetManager().GetTimelineTweets()));
            Refresh.Subscribe(tweets =>
            {
                TimelineTweets.AddRange(tweets);
                if (tweets.Count() > 0)
                {
                    TweetViewModel = new TweetViewModel(TimelineTweets.First());
                }
            });
            Refresh.ThrownExceptions.Subscribe(e => MessageBox.Show(e.Message));
        }

        #region IRoutableView Extension
        public string UrlPathSegment { get { return "Timeline List"; } }
        public IScreen HostScreen { get; protected set; }
        #endregion
    }
}
