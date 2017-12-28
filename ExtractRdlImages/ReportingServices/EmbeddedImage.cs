using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractRdlImages.ReportingServices
{
    public class EmbeddedImage
    {
        public EmbeddedImage(string name, string mimeType, byte[] content)
        {
            this.Name = name;
            this.MimeType = mimeType;
            this.ContentStream = new MemoryStream(content, false);
        }

        public string Name { get; private set; }

        public string MimeType { get; private set; }

        public Stream ContentStream { get; private set; }
    }
}
