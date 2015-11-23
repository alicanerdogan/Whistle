using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace WhistleGUI.ViewModel
{
    public class BrowserViewModel : ReactiveObject
    {
        private Uri _TargetUri;
        public Uri TargetUri
        {
            get { return _TargetUri; }
            set { this.RaiseAndSetIfChanged(ref _TargetUri, value); }
        }

        private string _Document;
        public string Document
        {
            get { return _Document; }
            set { this.RaiseAndSetIfChanged(ref _Document, value); }
        }

        private Uri _CurrentURI;
        public Uri CurrentURI
        {
            get { return _CurrentURI; }
            set { this.RaiseAndSetIfChanged(ref _CurrentURI, value); }
        }

        private bool _IsLoading;
        public bool IsLoading
        {
            get { return _IsLoading; }
            set { this.RaiseAndSetIfChanged(ref _IsLoading, value); }
        }

        public ReactiveCommand<Uri> Navigate { get; private set; }


        public BrowserViewModel()
        {
            IsLoading = false;
            Navigate = ReactiveCommand.CreateAsyncTask(uri => { return Task.Run(() => TargetUri as Uri); });
        }

        public BrowserViewModel(Uri uri)
        {
            TargetUri = uri;
        }
    }
}
