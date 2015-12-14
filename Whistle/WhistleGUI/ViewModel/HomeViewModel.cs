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
        public TimelineListViewModel TimelineListViewModel { get; private set; }

        public ReactiveCommand<bool> BackToPreviousView { get; private set; }

        public HomeViewModel()
        {
            Router = new RoutingState();

            LoginViewModel = new LoginViewModel(this);
            LoginViewModel.WhenAnyValue(vm => vm.IsLoggedIn).
                Where(isLoggedIn => isLoggedIn).
                Subscribe(_ =>
                {
                    TimelineListViewModel = new TimelineListViewModel(this);
                    Router.NavigateAndReset.Execute(TimelineListViewModel);
                    TimelineListViewModel.Refresh.Execute(null);
                });

            var canBackToPreviousView = Router.NavigationStack.WhenAny(stack => stack.Count, count => (count.Value > 1));
            BackToPreviousView = ReactiveCommand.CreateAsyncTask<bool>(canBackToPreviousView, _ => Task.Run(() => true));
            BackToPreviousView.SubscribeOn(RxApp.MainThreadScheduler).Subscribe(_ => Router.NavigateBack.Execute(null));

            Router.NavigateAndReset.Execute(LoginViewModel);

        }
    }
}
