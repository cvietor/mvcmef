using System;
using System.IO;
using System.IO.Packaging;
using System.Linq;

namespace Utils
{
    public class ZipUtil
    {
        private const long BUFFER_SIZE = 4096;

        public static void AddFileToZip(string zipFilename, string fileNameToAdd, Stream fileToAdd)
        {
            using (Package zip = Package.Open(zipFilename, FileMode.OpenOrCreate))
            {
                Uri uri = PackUriHelper.CreatePartUri(new Uri(fileNameToAdd, UriKind.Relative));
                if (zip.PartExists(uri))
                {
                    zip.DeletePart(uri);
                }
                PackagePart part = zip.CreatePart(uri, "", CompressionOption.Normal);

                using (Stream dest = part.GetStream())
                {
                    CopyStream(fileToAdd, dest);
                }
            }
        }

        private static void CopyStream(Stream inputStream, Stream outputStream)
        {
            long bufferSize = inputStream.Length < BUFFER_SIZE ? inputStream.Length : BUFFER_SIZE;
            var buffer = new byte[bufferSize];
            int bytesRead = 0;
            long bytesWritten = 0;
            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
                bytesWritten += bufferSize;
            }
        }

        public static void DecompressFile(string zipFilename, string outPath)
        {
            using (Package zip = System.IO.Packaging.Package.Open(zipFilename, FileMode.Open))
            {
                var binPart = zip.GetParts().Where(p => p.Uri.ToString().Contains("/bin/" + Path.GetFileNameWithoutExtension(zipFilename))).FirstOrDefault().Uri;
                var viewPart = zip.GetParts().Where(p => p.Uri.ToString().Contains("/Views/"));

                var assemblyPart = zip.GetPart(binPart);
                Extract(assemblyPart, outPath);

                foreach (var part in viewPart)
                {
                    Extract(part, outPath);
                }
            }
        }

        private static void Extract(PackagePart part, string outPath)
        {
            string outFileName = Path.Combine(outPath, part.Uri.OriginalString.Substring(1));

            if (!Directory.Exists(Path.GetDirectoryName(outFileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outFileName));
            }

            using (System.IO.FileStream outFileStream = new System.IO.FileStream(outFileName, FileMode.Create))
            {
                using (Stream inFileStream = part.GetStream())
                {
                    CopyStream(inFileStream, outFileStream);
                }
            }
        }
    }
}