using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterLibrary;
using WhistleGUI.View;

namespace WhistleGUI.ViewModel
{
    public class LoginViewModel : ReactiveObject, IRoutableViewModel
    {
        #region PROPERTIES
        string _Username;
        public string Username
        {
            get { return _Username; }
            set { this.RaiseAndSetIfChanged(ref _Username, value); }
        }

        string _Password;
        public string Password
        {
            get { return _Password; }
            set { this.RaiseAndSetIfChanged(ref _Password, value); }
        }

        public string PIN { get; set; }

        private bool _IsLoggedIn;
        public bool IsLoggedIn
        {
            get { return _IsLoggedIn; }
            set { this.RaiseAndSetIfChanged(ref _IsLoggedIn, value); }
        }
        public ReactiveCommand<Uri> GetAuthorizationUri { get; private set; }
        public ReactiveCommand<bool> Authenticate { get; private set; }

        public string UrlPathSegment { get { return "Login"; } }
        public IScreen HostScreen { get; protected set; }
        #endregion

        public LoginViewModel(IScreen screen)
        {
            HostScreen = screen;
            //TODO: Fetch previous used username
            Username = "";
            Password = "";
            IsLoggedIn = false;
            var canLogin = this.WhenAny(x => x.Username, x => !string.IsNullOrEmpty(x.Value));
            GetAuthorizationUri = ReactiveCommand.CreateAsyncTask<Uri>(canLogin, _ =>
            {
                return Task.Run(() => APIManager.GetManager().GetAuthorizationUri());
            });
            GetAuthorizationUri.Subscribe(uri =>
            {
                var window = new UserAuthorizationWindow(uri);
                window.ShowDialog();
                PIN = window.ViewModel.PIN;
                if (PIN != null)
                {
                    Authenticate.Execute(null);
                }
            });
            Authenticate = ReactiveCommand.CreateAsyncTask<bool>(canLogin, _ =>
            {
                return Task.Run(() => APIManager.GetManager().Authenticate(PIN));
            });
            Authenticate.Subscribe(authenticated =>
            {
                if (authenticated)
                {
                    IsLoggedIn = true;
                }
            });
        }
    }
}
