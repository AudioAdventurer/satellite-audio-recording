using System.Collections.Generic;
using System.IO;

namespace SAR.Libraries.Common.Storage
{
    public interface IFileStorage
    {
        Stream GetFile(string path);

        void SaveFile(string path, Stream data);

        IEnumerable<string> ListFiles(string path);

        string ToPath(params string[] parts);
    }
}
