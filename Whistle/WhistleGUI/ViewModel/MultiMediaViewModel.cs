using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TwitterLibrary;
using TwitterLibrary.Entity;

namespace WhistleGUI.ViewModel
{
    public class MultiMediaViewModel : ReactiveObject
    {
        public ReactiveList<BitmapImage> Images { get; private set; }

        public MultiMediaViewModel(IEnumerable<Media> media)
        {
            Images = new ReactiveList<BitmapImage>();
            media.ToList().ForEach(m => LoadImageAsync(m));
        }

        private async void LoadImageAsync(Media media)
        {
            var bitmap = await Helper.BitmapDownloader.DownloadAsync(media.Destination.ToString());
            Images.Add(bitmap);
        }
    }
}
