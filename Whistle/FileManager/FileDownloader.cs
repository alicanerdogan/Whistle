using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public static class FileDownloader
    {
        public static void Download(string url, string file)
        {
            using (var client = new WebClient())
            {
                var data = client.DownloadFile(url);
                MemoryStream imageStream = new MemoryStream(data);

                bitmap.BeginInit();
                bitmap.StreamSource = imageStream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
            }
        }
    }
}
