using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;
using System.Windows.Media.Imaging;
using System.Net.Cache;
using System.IO;

namespace WhistleGUI.Helper
{
    public static class BitmapDownloader
    {
        public static async Task<BitmapImage> DownloadAsync(string url)
        {
            var bitmap = new BitmapImage();

            using (var client = new WebClient())
            {
                var data = await client.DownloadDataTaskAsync(url);
                MemoryStream imageStream = new MemoryStream(data);

                bitmap.BeginInit();
                bitmap.StreamSource = imageStream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
            }

            return bitmap;
        }
    }
}
