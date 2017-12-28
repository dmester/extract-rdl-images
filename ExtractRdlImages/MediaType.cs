using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtractRdlImages
{
    internal class MediaType
    {
        private MediaType() { }

        /// <summary>
        /// Gets the name of this media type.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the default file name extension (including leading period) generally used for this media type.
        /// </summary>
        public string Extension { get; private set; }

        /// <summary>
        /// Gets a <see cref="MediaType"/> by its name.
        /// </summary>
        /// <param name="name">Name of the media type.</param>
        public static MediaType ByName(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException($"{name} cannot be an empty string.", nameof(name));

            string extension;

            switch (name)
            {
                case "image/bmp": extension = ".bmp"; break;
                case "image/gif": extension = ".gif"; break;
                case "image/png": extension = ".png"; break;
                case "image/x-png": extension = ".png"; break;
                case "image/jpeg": extension = ".jpg"; break;
                default: throw new ArgumentException($"Unknown media type '{name}'.", nameof(name));
            }

            return new MediaType
            {
                Name = name,
                Extension = extension
            };
        }
    }
}
