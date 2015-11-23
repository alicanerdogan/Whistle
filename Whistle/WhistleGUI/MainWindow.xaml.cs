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
using WhistleGUI.View;
using WhistleGUI.ViewModel;

namespace WhistleGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<HomeViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            Splat.Locator.CurrentMutable.Register(() => new LoginView(), typeof(IViewFor<LoginViewModel>));
            Splat.Locator.CurrentMutable.Register(() => new TimelineListView(), typeof(IViewFor<TimelineListViewModel>));

            ViewModel = new HomeViewModel();
            this.Bind(ViewModel, vm => vm.Router, v => v.ContentView.Router);
        }

        private void CloseApp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void OnAppButtons(object sender, MouseEventArgs e)
        {
            var visualElement = (Panel)sender;
            visualElement.Background = Brushes.DarkGray;
        }

        private void OffAppButtons(object sender, MouseEventArgs e)
        {
            var visualElement = (Panel)sender;
            visualElement.Background = Brushes.Transparent;
        }

        #region IViewFor Extension
        public HomeViewModel ViewModel
        {
            get { return (HomeViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(HomeViewModel), typeof(MainWindow), new PropertyMetadata(null));

        object IViewFor.ViewModel { get { return ViewModel; } set { ViewModel = value as HomeViewModel; } }
        #endregion
    }
}
