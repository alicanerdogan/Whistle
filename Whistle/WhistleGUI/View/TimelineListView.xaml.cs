using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

            this.Bind(ViewModel, vm => vm.TweetViewModel, v => v.Tweet.ViewModel);
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
