using ExtractRdlImages.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractRdlImages.Install
{
    internal static class ReportFileAssociation
    {

        private static FileAssociationAction GetDefaultFileAssociation(FileAssociationScope scope)
        {
            return new FileAssociationAction
            {
                Name = "Extract",
                DisplayName = "E&xtract embedded images",
                Command = string.Format("\"{0}\" \"%1\"", Installer.GetInstallPath(Convert(scope)))
            };
        }

        private static InstallScope Convert(FileAssociationScope scope)
        {
            return scope == FileAssociationScope.LocalMachine ?
                InstallScope.LocalMachine : InstallScope.CurrentUser;
        }

        public static bool IsInstalled(FileAssociationScope scope)
        {
            using (var association = FileAssociations.GetAssociation(".rdl", scope))
            {
                return association.Actions.Contains(GetDefaultFileAssociation(scope));
            }
        }

        public static void Install(FileAssociationScope scope)
        {
            using (var association = FileAssociations.GetSystemAssociation(".rdl", scope, true))
            {
                association.Actions.Add(GetDefaultFileAssociation(scope));
            }

            FileAssociations.Refresh();
        }

        public static void Uninstall(FileAssociationScope scope)
        {
            using (var association = FileAssociations.GetSystemAssociation(".rdl", scope, true))
            {
                association.Actions.Remove(GetDefaultFileAssociation(scope));
            }

            FileAssociations.Refresh();
        }
    }
}
