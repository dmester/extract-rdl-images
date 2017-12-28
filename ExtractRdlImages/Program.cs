using ExtractRdlImages.Forms;
using ExtractRdlImages.Install;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExtractRdlImages
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                var scope = 
                    args.Length > 1 && args[1] == "currentuser" ?
                    InstallScope.CurrentUser : InstallScope.LocalMachine;

                if (args[0] == "-install")
                {
                    Installer.Install(scope, false);
                    return;
                }
                if (args[0] == "-uninstall")
                {
                    Installer.Uninstall(scope, false);
                    return;
                }

                var reportPath = args[0];
                try
                {
                    Extractor.ExtractImages(reportPath);
                }
                catch (DirectoryNotFoundException)
                {
                    // File not found, do nothing
                }
                catch (FileNotFoundException)
                {
                    // File not found, do nothing
                }
            }
        }
    }
}
