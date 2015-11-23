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
using System.Windows.Shapes;
using WhistleGUI.ViewModel;
using System.Reactive.Linq;

namespace WhistleGUI.View
{
    /// <summary>
    /// Interaction logic for UserAuthorizationWindow.xaml
    /// </summary>
    public partial class UserAuthorizationWindow : Window, IViewFor<UserAuthorizationViewModel>
    {
        public UserAuthorizationWindow(Uri uri)
        {
            InitializeComponent();

            ViewModel = new UserAuthorizationViewModel(uri);
            this.Bind(ViewModel, vm => vm.BrowserViewModel, v => v.BrowserView.ViewModel);
            this.WhenAnyValue(vm => vm.ViewModel.IsTerminated)
                .Where(isTerminated => isTerminated)
                .Subscribe(_ => this.Close());
        }

        #region IViewFor Extension
        public UserAuthorizationViewModel ViewModel
        {
            get { return (UserAuthorizationViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(UserAuthorizationViewModel), typeof(UserAuthorizationWindow), new PropertyMetadata(null));

        object IViewFor.ViewModel { get { return ViewModel; } set { ViewModel = value as UserAuthorizationViewModel; } }
        #endregion
    }
}
