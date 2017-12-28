using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractRdlImages.Platform
{
    public class FileAssociation : IDisposable
    {
        RegistryKey key;
        
        internal FileAssociation(RegistryKey key, string extension, bool writable)
        {
            this.Extension = extension;
            this.key = key;
            
            if (key == null)
            {
                this.Actions = new FileAssociationAction[0];
            }
            else
            {
                this.Actions = new FileAssociationActionCollection(new Lazy<RegistryKey>(() => key.OpenSubKey("shell", writable, writable)));
            }
        }

        public string Extension { get; private set; }

        public string DisplayName
        {
            get { return key == null ? null : key.GetValue(null) as string; }
            set { key.SetValue(null, value, RegistryValueKind.String); }
        }

        public ICollection<FileAssociationAction> Actions { get; private set; }

        public void Dispose()
        {
            key.Dispose();

            var disposableActions = Actions as IDisposable;
            if (disposableActions != null)
            {
                disposableActions.Dispose();
            }
        }
    }
}
