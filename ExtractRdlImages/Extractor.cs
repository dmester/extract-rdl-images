using ExtractRdlImages.ReportingServices;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExtractRdlImages
{
    internal static class Extractor
    {
        public static void ExtractImages(string reportPath)
        {
            var report = new ReportFile(reportPath);

            var reportFolder = Path.GetDirectoryName(reportPath);
            var extractFolder = Path.Combine(reportFolder, Path.GetFileNameWithoutExtension(reportPath));
            var temporaryExtractFolder = CreateTemporaryDirectory(
                Path.Combine(reportFolder, $"{Path.GetFileName(reportPath)} Extracted Images"));

            // Extract images to a temporary directory and then use SHFileOperation exposed
            // via FileSystem to allow the user choose an action if the extracted files
            // already exist.
            try
            {
                ExtractImages(temporaryExtractFolder, report.EmbeddedImages);
                Directory.CreateDirectory(extractFolder);
                FileSystem.CopyDirectory(temporaryExtractFolder, extractFolder, 
                    UIOption.AllDialogs, UICancelOption.DoNothing);
            }
            finally
            {
                Directory.Delete(temporaryExtractFolder, true);
            }
        }

        private static string CreateTemporaryDirectory(string templateName)
        {
            for (var i = 1; i < 20; i++)
            {
                var suggestedPath = i == 1 ? templateName : $"{templateName} ({i})";

                if (!Directory.Exists(suggestedPath))
                {
                    Directory.CreateDirectory(suggestedPath);
                    return suggestedPath;
                }
            }

            throw new IOException("Failed to create a temporary directory.");
        }

        private static void ExtractImages(string destinationDirectory, IEnumerable<EmbeddedImage> images)
        {
            foreach (var image in images)
            {
                using (var stream = File.OpenWrite(Path.Combine(destinationDirectory, GetFileName(image))))
                {
                    image.ContentStream.CopyTo(stream);
                }
            }
        }
        
        private static string GetFileName(EmbeddedImage image)
        {
            var name = new StringBuilder(image.Name);
            var mediaType = MediaType.ByName(image.MimeType);
            var invalidChars = Path.GetInvalidFileNameChars();

            for (var i = name.Length - 1; i >= 0; i--)
            {
                if (invalidChars.Contains(name[i]))
                {
                    name.Remove(i, 1);
                }
            }

            name.Append(mediaType.Extension);
            return name.ToString();
        }
    }
}
