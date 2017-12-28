using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractRdlImages.Platform
{
    internal static class RegistryKeyExtensions
    {
        public static RegistryKey OpenSubKey(this RegistryKey key, string name, bool writable, bool createIfNotExist)
        {
            var subKey = key.OpenSubKey(name, writable);
            if (subKey == null && createIfNotExist)
            {
                subKey = key.CreateSubKey(name);
            }
            return subKey;
        }
    }
}
