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
    /// Interaction logic for TwitterUserView.xaml
    /// </summary>
    public partial class TwitterUserView : UserControl, IViewFor<TwitterUserViewModel>
    {
        public TwitterUserView()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, vm => vm.User.Username, v => v.Username.Text);
            this.OneWayBind(ViewModel, vm => vm.User.DisplayName, v => v.DisplayName.Text);
        }

        #region IViewFor Extension
        public TwitterUserViewModel ViewModel
        {
            get { return (TwitterUserViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(TwitterUserViewModel), typeof(TwitterUserView), new PropertyMetadata(null));

        object IViewFor.ViewModel { get { return ViewModel; } set { ViewModel = value as TwitterUserViewModel; } }
        #endregion
    }
}
