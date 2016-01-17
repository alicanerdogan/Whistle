using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder
{
    public class FileHoardContainer : IHoardContainer<string, Stream>
    {
        private string FolderPath { get; set; }

        public FileHoardContainer(string folderPath)
        {
            FolderPath = folderPath;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        public bool Contains(string key)
        {
            var fileNames = Directory.GetFiles(FolderPath);
            return fileNames.Contains(CreateFilePath(key));
        }

        public IHoardItem<Stream> Get(string key)
        {
            return new HoardFileItem(CreateFilePath(key));
        }

        public void Set(string key, IHoardItem<Stream> item)
        {
            using (var stream = item.Item)
            {
                new HoardFileItem(key, stream);
            }
        }

        private string CreateFilePath(string filename)
        {
            return FolderPath + @"\" + filename;
        }
    }
}
