using System.Collections.Generic;
using System.IO;

namespace SAR.Libraries.Common.Storage
{
    public class LocalStorage : IFileStorage
    {
        private readonly string _basePath;

        public LocalStorage(
            string basePath)
        {
            _basePath = basePath;

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
        }

        public Stream GetFile(string path)
        {
            var temp = Path.Combine(_basePath, path);

            if (File.Exists(temp))
            {
                return File.OpenRead(temp);
            }

            return null;
        }

        public void SaveFile(string path, Stream data)
        {
            var temp = Path.Combine(_basePath, path);

            var folder = Path.GetDirectoryName(temp);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            using (var fs = File.Create(temp))
            {
                data.Seek(0, SeekOrigin.Begin);
                data.CopyTo(fs);
                fs.Close();
            }
        }

        public IEnumerable<string> ListFiles(string path)
        {
            var temp = Path.Combine(_basePath, path);
            return Directory.EnumerateFiles(temp);
        }

        public string ToPath(params string[] parts)
        {
            return Path.Combine(parts);
        }
    }
}
