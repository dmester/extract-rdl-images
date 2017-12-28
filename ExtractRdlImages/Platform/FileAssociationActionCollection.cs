using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractRdlImages.Platform
{
    internal class FileAssociationActionCollection : ICollection<FileAssociationAction>, IDisposable
    {
        Lazy<RegistryKey> shellKey;

        internal FileAssociationActionCollection(Lazy<RegistryKey> shellKey)
        {
            this.shellKey = shellKey;
        }

        public void Add(FileAssociationAction item)
        {
            using (var actionKey = shellKey.Value.OpenSubKey(item.Name, true, true))
            {
                actionKey.SetValue(null, item.DisplayName);

                using (var commandKey = actionKey.OpenSubKey("command", true, true))
                {
                    commandKey.SetValue(null, item.Command);
                }
            }
        }

        public void Clear()
        {
            foreach (var name in shellKey.Value.GetSubKeyNames())
            {
                shellKey.Value.DeleteSubKeyTree(name);
            }
        }

        public bool Contains(FileAssociationAction item)
        {
            return shellKey.Value.GetSubKeyNames().Contains(item.Name);
        }

        public void CopyTo(FileAssociationAction[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { return shellKey.Value.GetSubKeyNames().Length; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(FileAssociationAction item)
        {
            using (var key = shellKey.Value.OpenSubKey(item.Name))
            {
                if (key == null)
                {
                    return false;
                }
            }

            shellKey.Value.DeleteSubKeyTree(item.Name);
            return true;
        }

        public IEnumerator<FileAssociationAction> GetEnumerator()
        {
            foreach (var name in shellKey.Value.GetSubKeyNames())
            {
                using (var subKey = shellKey.Value.OpenSubKey(name))
                {
                    using (var commandSubKey = subKey.OpenSubKey("command"))
                    {
                        yield return new FileAssociationAction
                        {
                            Name = name,
                            DisplayName = subKey.GetValue(null) as string,
                            Command = commandSubKey.GetValue(null) as string
                        };
                    }
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Dispose()
        {
            if (shellKey.IsValueCreated)
            {
                shellKey.Value.Close();
            }
        }
    }
}
