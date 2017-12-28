using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExtractRdlImages.ReportingServices
{
    internal class ReportFile
    {
        XDocument doc;

        public ReportFile(string path)
        {
            doc = XDocument.Load(path);
        }
        
        public IEnumerable<EmbeddedImage> EmbeddedImages
        {
            get
            {
                var ns = doc.Root.Name.Namespace;

                var embeddedImages = doc.Root.Element(ns + "EmbeddedImages");
                if (embeddedImages != null)
                {
                    foreach (var embeddedImage in embeddedImages.Elements(ns + "EmbeddedImage"))
                    {
                        var nameAttribute = embeddedImage.Attribute("Name");
                        var mimeTypeElement = embeddedImage.Element(ns + "MIMEType");
                        var imageDataElement = embeddedImage.Element(ns + "ImageData");

                        if (nameAttribute != null && mimeTypeElement != null && imageDataElement != null)
                        {
                            yield return new EmbeddedImage(
                                nameAttribute.Value,
                                mimeTypeElement.Value,
                                Convert.FromBase64String(imageDataElement.Value));
                        }
                    }
                }
            }
        }
    }
}
