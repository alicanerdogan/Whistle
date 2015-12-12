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
    /// Interaction logic for TweetView.xaml
    /// </summary>
    public partial class TweetView : UserControl, IViewFor<TweetViewModel>
    {
        public TweetView()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, vm => vm.ProcessedContent, v => v.TweetContent.RichText);
            this.Bind(ViewModel, vm => vm.ScreenName, v => v.ScreenName.Text);
            this.Bind(ViewModel, vm => vm.Username, v => v.Username.Text);
            this.Bind(ViewModel, vm => vm.TimeTag, v => v.TimeTag.Text);
            this.Bind(ViewModel, vm => vm.ReplyCount, v => v.ReplyCount.Text);
            this.Bind(ViewModel, vm => vm.LikeCount, v => v.LikeCount.Text);
            this.Bind(ViewModel, vm => vm.RetweetCount, v => v.RetweetCount.Text);
            this.OneWayBind(ViewModel, vm => vm.Avatar, v => v.Avatar.Source);
            this.Bind(ViewModel, vm => vm.MultiMediaViewModel, v => v.MultiMedia.ViewModel);
        }


        #region IViewFor Extension
        public TweetViewModel ViewModel
        {
            get { return (TweetViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(TweetViewModel), typeof(TweetView), new PropertyMetadata(null));

        object IViewFor.ViewModel { get { return ViewModel; } set { ViewModel = value as TweetViewModel; } }
        #endregion
    }
}
