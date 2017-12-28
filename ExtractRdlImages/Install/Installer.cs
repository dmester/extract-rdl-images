using ExtractRdlImages.Platform;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtractRdlImages.Install
{
    internal static class Installer
    {
        const string UninstallKey = "ExtractRdlImages";
        const string InstallDirectory = "Extract RDL Images";

        public static string GetInstallPath(InstallScope scope)
        {
            var fileName = Path.GetFileName(typeof(Program).Assembly.Location);
            return scope == InstallScope.LocalMachine ?
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), InstallDirectory, fileName) :
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), InstallDirectory, fileName);
        }

        public static bool IsInstalled(InstallScope scope)
        {
            using (var uninstallKey = OpenUninstall(scope, false))
            {
                using (var subKey = uninstallKey.OpenSubKey(UninstallKey))
                {
                    return subKey != null;
                }
            }
        }

        private static void Elevate(string commandLineArguments)
        {
            const int ERROR_CANCELLED = 1223;

            var process = new Process();
            process.StartInfo.FileName = Assembly.GetEntryAssembly().Location;
            process.StartInfo.Arguments = commandLineArguments;
            process.StartInfo.Verb = "runas";

            try
            {
                process.Start();
                process.WaitForExit();
            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode != ERROR_CANCELLED)
                {
                    throw;
                }
            }
        }
        
        private static RegistryKey OpenUninstall(InstallScope scope, bool writable)
        {
            var root = scope == InstallScope.LocalMachine ? Registry.LocalMachine : Registry.CurrentUser;
            return root.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall", writable);
        }

        public static void Install(InstallScope scope, bool autoElevate = true)
        {
            if (autoElevate && scope == InstallScope.LocalMachine)
            {
                Elevate("-install localmachine");
                return;
            }

            var target = GetInstallPath(scope);
            var targetDirectory = Path.GetDirectoryName(target);

            Directory.CreateDirectory(targetDirectory);
            File.Copy(typeof(Program).Assembly.Location, target, true);

            var assembly = typeof(Installer).Assembly;

            var assemblyTitle = GetAttribute<AssemblyTitleAttribute>(assembly);
            var assemblyCompany = GetAttribute<AssemblyCompanyAttribute>(assembly);

            using (var uninstallKey = OpenUninstall(scope, true))
            {
                using (var rdlUninstallKey = uninstallKey.OpenSubKey(UninstallKey, true, true))
                {
                    rdlUninstallKey.SetValue("DisplayName", assemblyTitle.Title, RegistryValueKind.String);
                    rdlUninstallKey.SetValue("Publisher", assemblyCompany.Company, RegistryValueKind.String);
                    rdlUninstallKey.SetValue("InstallDate", DateTime.Now.ToString("yyyyMMdd"), RegistryValueKind.String);
                    rdlUninstallKey.SetValue("InstallLocation", targetDirectory, RegistryValueKind.String);
                    rdlUninstallKey.SetValue("NoModify", 1, RegistryValueKind.DWord);
                    rdlUninstallKey.SetValue("NoRepair", 1, RegistryValueKind.DWord);
                    rdlUninstallKey.SetValue("UninstallString", string.Format("\"{0}\" -uninstall {1}",
                        target, scope == InstallScope.LocalMachine ? "localmachine" : "currentuser"), RegistryValueKind.ExpandString);
                }
            }

            ReportFileAssociation.Install(Convert(scope));
        }

        private static T GetAttribute<T>(Assembly assembly)
        {
            var attributes = assembly.GetCustomAttributes(typeof(T), false);
            if (attributes == null || attributes.Length == 0)
            {
                return default(T);
            }

            return (T)attributes[0];
        }

        public static void Uninstall(InstallScope scope, bool autoElevate = true)
        {
            if (autoElevate && scope == InstallScope.LocalMachine)
            {
                Elevate("-uninstall localmachine");
                return;
            }

            var target = GetInstallPath(scope);
            var targetDirectory = Path.GetDirectoryName(target);

            using (var uninstallKey = OpenUninstall(scope, true))
            {
                uninstallKey.DeleteSubKeyTree(UninstallKey, false);
            }

            ReportFileAssociation.Uninstall(Convert(scope));
            
            DeleteDirectoryDelayed(TimeSpan.FromSeconds(2), targetDirectory);
        }

        private static void DeleteDirectoryDelayed(TimeSpan delay, string directoryPath)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/C ping -n {(int)delay.TotalSeconds} 127.0.0.1 & rmdir /S /Q \"{directoryPath}\"",
                CreateNoWindow = true,
                UseShellExecute = false
            });
        }

        private static FileAssociationScope Convert(InstallScope scope)
        {
            return scope == InstallScope.LocalMachine ? FileAssociationScope.LocalMachine : FileAssociationScope.CurrentUser;
        }
    }
}
