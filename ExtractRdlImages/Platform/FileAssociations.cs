using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractRdlImages.Platform
{
    public static class FileAssociations
    {
        public static void Refresh()
        {
            NativeMethods.SHChangeNotify(NativeMethods.SHCNE.SHCNE_ASSOCCHANGED, NativeMethods.SHCNF.SHCNF_FLUSH, IntPtr.Zero, IntPtr.Zero);
        }

        public static FileAssociation GetSystemAssociation(string extension, FileAssociationScope scope, bool writable = false)
        {
            if (extension == null) throw new ArgumentNullException("extension");
            if (extension.Length < 2 || extension[0] != '.') throw new ArgumentException("Invalid extension", "extension");
            
            using (var classes = (scope == FileAssociationScope.CurrentUser ? Registry.CurrentUser : Registry.LocalMachine).OpenSubKey("SOFTWARE\\Classes\\SystemFileAssociations", writable, writable))
            {
                var subKey = classes?.OpenSubKey(extension, writable, writable);
                return new FileAssociation(subKey, extension, writable);
            }
        }

        public static FileAssociation GetAssociation(string extension, FileAssociationScope scope, bool writable = false)
        {
            if (extension == null) throw new ArgumentNullException("extension");
            if (extension.Length < 2 || extension[0] != '.') throw new ArgumentException("Invalid extension", "extension");

            string type = null;

            using (var classes = (scope == FileAssociationScope.CurrentUser ? Registry.CurrentUser : Registry.LocalMachine).OpenSubKey("SOFTWARE\\Classes", writable))
            {
                var extensionKey = Registry.ClassesRoot.OpenSubKey(extension);
                if (extensionKey == null)
                {
                    if (writable)
                    {
                        // Create new extension
                        var sb = new StringBuilder(extension, 1, extension.Length - 1, extension.Length + 3);
                        sb[0] = char.ToUpper(sb[0]);
                        sb.Append("File");
                        type = sb.ToString();

                        using (var extensionKey2 = classes.CreateSubKey(extension))
                        {
                            extensionKey2.SetValue(null, type);
                        }
                    }
                }
                else
                {
                    try
                    {
                        type = extensionKey.GetValue(null) as string;
                    }
                    finally
                    {
                        extensionKey.Close();
                    }
                }

                if (type == null)
                {
                    return new FileAssociation(null, extension, false);
                }

                var subKey = classes.OpenSubKey(type, writable, writable);
                return new FileAssociation(subKey, extension, writable);
            }
        }
    }
}
