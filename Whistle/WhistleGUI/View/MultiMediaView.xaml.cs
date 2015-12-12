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
    /// Interaction logic for MultiMediaView.xaml
    /// </summary>
    public partial class MultiMediaView : UserControl, IViewFor<MultiMediaViewModel>
    {
        public MultiMediaView()
        {
            InitializeComponent();

            this.OneWayBind(ViewModel, vm => vm.Images, v => v.Media.ItemsSource);
        }

        #region IViewFor Extension
        public MultiMediaViewModel ViewModel
        {
            get { return (MultiMediaViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(MultiMediaViewModel), typeof(MultiMediaView), new PropertyMetadata(null));

        object IViewFor.ViewModel { get { return ViewModel; } set { ViewModel = value as MultiMediaViewModel; } }
        #endregion
    }
}
