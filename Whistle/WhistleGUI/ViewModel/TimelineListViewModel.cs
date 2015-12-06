using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TwitterLibrary;

namespace WhistleGUI.ViewModel
{
    public class TimelineListViewModel : ReactiveObject, IRoutableViewModel
    {
        #region Commands
        public ReactiveCommand<IEnumerable<Tweet>> Refresh { get; private set; }
        public ReactiveCommand<IEnumerable<Tweet>> GetOlderTweets { get; private set; }
        #endregion

        #region Properties
        public ReactiveList<TweetViewModel> Tweets { get; private set; }
        #endregion

        public TimelineListViewModel(IScreen screen)
        {
            HostScreen = screen;

            Tweets = new ReactiveList<TweetViewModel>();
            Refresh = ReactiveCommand.CreateAsyncTask<IEnumerable<Tweet>>(_ => Task.Run(() => APIManager.GetManager().GetTimelineTweets()));
            Refresh.Subscribe(tweets =>
            {
                foreach (var tweet in tweets)
                {
                    Tweets.Add(new TweetViewModel(tweet));
                }
            });
            Refresh.ThrownExceptions.Subscribe(e => MessageBox.Show(e.Message));

            GetOlderTweets = ReactiveCommand.CreateAsyncTask<IEnumerable<Tweet>>(_ => Task.Run(() => APIManager.GetManager().GetTimelineTweetsBefore(Tweets.Last().Tweet.Id)));
            GetOlderTweets.Subscribe(tweets =>
            {
                foreach (var tweet in tweets)
                {
                    Tweets.Add(new TweetViewModel(tweet));
                }
            });
            GetOlderTweets.ThrownExceptions.Subscribe(e => MessageBox.Show(e.Message));
        }

        #region IRoutableView Extension
        public string UrlPathSegment { get { return "Timeline List"; } }
        public IScreen HostScreen { get; protected set; }
        #endregion
    }
}
