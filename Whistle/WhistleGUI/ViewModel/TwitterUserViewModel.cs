using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterLibrary;

namespace WhistleGUI.ViewModel
{
    public class TwitterUserViewModel : ReactiveObject, IRoutableViewModel
    {
        public TwitterUser User { get; private set; }

        public TwitterUserViewModel(IScreen screen, TwitterUser twitterUser)
        {
            HostScreen = screen;
            User = twitterUser;
        }

        #region IRoutableView Extension
        public string UrlPathSegment { get { return "User"; } }
        public IScreen HostScreen { get; protected set; }
        #endregion
    }
}
