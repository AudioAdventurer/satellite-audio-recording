using System.IO;

namespace SAR.Libraries.Common.Helpers
{
    public static class DirectoryHelper
    {
        public static void EnsureDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
