using ReactiveUI;
using System;
using System.Reactive.Linq;

namespace WhistleGUI.ViewModel
{
    public class UserAuthorizationViewModel : ReactiveObject
    {
        public BrowserViewModel BrowserViewModel { get; private set; }

        private bool _IsTerminated;
        public bool IsTerminated
        {
            get { return _IsTerminated; }
            set { this.RaiseAndSetIfChanged(ref _IsTerminated, value); }
        }

        private string _PIN;
        public string PIN
        {
            get { return _PIN; }
            set { this.RaiseAndSetIfChanged(ref _PIN, value); }
        }

        public UserAuthorizationViewModel(Uri uri)
        {

            BrowserViewModel = new BrowserViewModel() { TargetUri = uri };

            BrowserViewModel.WhenAnyValue(vm => vm.IsLoading).Subscribe(isLoading =>
                {
                    if (isLoading)
                    {
                        //TODO: Block Browser View and Show Spinner
                    }
                    else
                    {
                        if (BrowserViewModel.CurrentURI?.OriginalString == "https://api.twitter.com/oauth/authorize")
                        {
                            //TODO: Parse PIN in Document and Redirect Timeline
                            PIN = DOMSelector.Selector.GetSelection(BrowserViewModel.Document, "code");
                            IsTerminated = true;
                        }
                    }
                });

            BrowserViewModel.Navigate.Execute(null);
        }

        protected UserAuthorizationViewModel()
        {
        }
    }
}
