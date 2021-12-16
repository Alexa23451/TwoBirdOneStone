using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.GZip;

namespace ServerConnect
{
    internal class Zipper
    {
        public static string CompressString(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            using (var compressedStream = new MemoryStream())
            {
                GZip.Compress(new MemoryStream(buffer), compressedStream, false);
                byte[] compressedData = compressedStream.ToArray();
                return Convert.ToBase64String(compressedData);
            }
        }


        public static string DecompressString(string compressedText)
        {
            if (string.IsNullOrEmpty(compressedText))
                return null;
            byte[] gZipBuffer = Convert.FromBase64String(compressedText);
            using (var memoryStream = new MemoryStream())
            {
                using (var compressedStream = new MemoryStream(gZipBuffer))
                {
                    var decompressedStream = new MemoryStream();
                    GZip.Decompress(compressedStream, decompressedStream, false);

                    return Encoding.UTF8.GetString(decompressedStream.ToArray()).Trim();
                }
            }
        }
    }
}