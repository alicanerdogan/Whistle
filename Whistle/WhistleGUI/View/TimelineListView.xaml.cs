using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WhistleGUI.ViewModel;

namespace WhistleGUI.View
{
    /// <summary>
    /// Interaction logic for TimelineListView.xaml
    /// </summary>
    public partial class TimelineListView : UserControl, IViewFor<TimelineListViewModel>
    {
        public TimelineListView()
        {
            InitializeComponent();

            var scrollViewerInsideListbox = Helper.VisualTreeTraverser.GetScrollViewerInsideListBox(Tweets);
            var scrollToTheBottomEvents = Observable.FromEventPattern<ScrollChangedEventArgs>(scrollViewerInsideListbox, nameof(ScrollViewer.ScrollChanged)).Where(IsScrollBarAtTheBottom).Throttle(TimeSpan.FromSeconds(1));
            scrollToTheBottomEvents.ObserveOn(RxApp.MainThreadScheduler).Subscribe(_ =>
            {
                this.ViewModel.GetOlderTweets.Execute(null);
            });

            this.OneWayBind(ViewModel, vm => vm.Tweets, v => v.Tweets.ItemsSource);
        }

        private static bool IsScrollBarAtTheBottom(System.Reactive.EventPattern<ScrollChangedEventArgs> args)
        {
            var scrollViewer = args.EventArgs.OriginalSource as ScrollViewer;
            var scrollViewerBottomOffset = scrollViewer.ScrollableHeight;
            var currentScrollViewerOffset = args.EventArgs.VerticalOffset;
            return (scrollViewerBottomOffset > 0) && (scrollViewerBottomOffset == currentScrollViewerOffset);
        }

        #region IViewFor Extension
        public TimelineListViewModel ViewModel
        {
            get { return (TimelineListViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(TimelineListViewModel), typeof(TimelineListView), new PropertyMetadata(null));

        object IViewFor.ViewModel { get { return ViewModel; } set { ViewModel = value as TimelineListViewModel; } }
        #endregion
    }
}
