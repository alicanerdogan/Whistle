using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoarder
{
    public class HoardFileItem : IHoardItem<Stream>
    {
        public DateTime AccessedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Stream Item
        {
            get
            {
                var stream = new MemoryStream();
                using (var fileStream = new FileStream(Filepath, FileMode.Open))
                {
                    int data = 0;
                    while ((data = fileStream.ReadByte()) != -1)
                    {
                        stream.WriteByte((byte)data);
                    }
                }
                stream.Position = 0;
                return stream;
            }
        }
        public DateTime UpdatedAt { get; private set; }

        private string Filepath { get; set; }

        public HoardFileItem(string filePath)
        {
            Filepath = filePath;
            CreatedAt = File.GetCreationTime(Filepath);
            AccessedAt = File.GetLastAccessTime(Filepath);
            UpdatedAt = File.GetLastWriteTime(Filepath);
        }

        public HoardFileItem(string filePath, Stream stream)
        {
            Filepath = filePath;
            stream.Position = 0;
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                int data = 0;
                while ((data = stream.ReadByte()) != -1)
                {
                    fileStream.WriteByte((byte)data);
                }
            }
            Filepath = filePath;
            CreatedAt = File.GetCreationTime(Filepath);
            AccessedAt = File.GetLastAccessTime(Filepath);
            UpdatedAt = File.GetLastWriteTime(Filepath);
        }
    }
}
