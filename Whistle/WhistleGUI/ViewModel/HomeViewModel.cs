using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhistleGUI.ViewModel
{
    public class HomeViewModel : ReactiveObject, IScreen
    {
        public RoutingState Router { get; private set; }
        public LoginViewModel LoginViewModel { get; private set; }

        public HomeViewModel()
        {
            Router = new RoutingState();

            LoginViewModel = new LoginViewModel(this);
            LoginViewModel.WhenAnyValue(vm => vm.IsLoggedIn).
                Where(isLoggedIn => isLoggedIn).
                Subscribe(_ =>
                {
                    var timelineListViewModel = new TimelineListViewModel(this);
                    Router.Navigate.Execute(timelineListViewModel);
                    timelineListViewModel.Refresh.Execute(null);
                });

            Router.Navigate.Execute(LoginViewModel);
        }
    }
}
