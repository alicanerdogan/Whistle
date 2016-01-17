using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder
{
    public class FileComplexHoarderAsync<TProviderKey> : ObjectComplexHoarderAsync<TProviderKey, string, Stream>
    {
        private string FolderPath { get; set; }

        public FileComplexHoarderAsync(string folderPath, Func<TProviderKey, Task<Stream>> asyncHoardProvider, Func<TProviderKey, string> keyProvider) : base(new FileHoardContainer(folderPath), asyncHoardProvider, keyProvider)
        {
            FolderPath = folderPath;
        }

        public FileComplexHoarderAsync(string folderPath, Func<TProviderKey, Task<Stream>> asyncHoardProvider, Func<TProviderKey, string> keyProvider, IHoardPolicy hoardPolicy) : base(new FileHoardContainer(folderPath), asyncHoardProvider, keyProvider, hoardPolicy)
        {
        }

        protected override async Task<IHoardItem<Stream>> CreateHoardItem(TProviderKey keyProvider, string key)
        {
            var newItem = await Provider(keyProvider);
            var hoardedItem = new HoardFileItem(GetFilePath(key), newItem);
            return hoardedItem;
        }

        private string GetFilePath(string key)
        {
            return FolderPath + @"\" + key;
        }
    }
}
