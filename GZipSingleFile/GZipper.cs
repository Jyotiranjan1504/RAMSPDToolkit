using System.IO.Compression;

namespace GZipSingleFile
{
    internal class GZipper
    {
        public static void CompressFile(string source, string destination)
        {
            using FileStream sourceFile = new(source, FileMode.Open);
            using FileStream targetFile = new(destination, FileMode.OpenOrCreate);

            using GZipStream gzipStream = new(targetFile, CompressionMode.Compress);

            var buffer = new byte[1024];
            int read;

            while ((read = sourceFile.Read(buffer, 0, buffer.Length)) > 0)
            {
                gzipStream.Write(buffer, 0, read);
            }
        }
    }
}
