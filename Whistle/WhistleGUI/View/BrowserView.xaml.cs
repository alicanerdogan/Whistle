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
using mshtml;
using System.Reactive.Linq;

namespace WhistleGUI.View
{
    /// <summary>
    /// Interaction logic for BrowserView.xaml
    /// </summary>
    public partial class BrowserView : UserControl, IViewFor<BrowserViewModel>
    {
        public BrowserView()
        {
            InitializeComponent();
            this.WhenAnyObservable(x => x.ViewModel.Navigate).Subscribe(uri => Browser.Navigate(uri));
            this.Browser.Navigated += (sender, e) =>
            {
                this.ViewModel.IsLoading = true;
                this.ViewModel.CurrentURI = e.Uri;
            };
            this.Browser.LoadCompleted += (sender, e) =>
            {
                this.ViewModel.Document = (this.Browser.Document as IHTMLDocument2).body.innerHTML;
                this.ViewModel.IsLoading = false;
            };
        }

        #region IViewFor Extension
        public BrowserViewModel ViewModel
        {
            get { return (BrowserViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(BrowserViewModel), typeof(BrowserView), new PropertyMetadata(null));

        object IViewFor.ViewModel { get { return ViewModel; } set { ViewModel = value as BrowserViewModel; } }
        #endregion
    }
}
