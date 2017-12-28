using ExtractRdlImages.Install;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtractRdlImages.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            UpdateState();
        }

        private InstallScope CurrentScope
        {
            get { return allUsersCheckBox.Checked ? InstallScope.LocalMachine : InstallScope.CurrentUser; }
        }

        private bool DropHighlighted
        {
            get { return lbExtract.BackColor != gbExtract.BackColor; }
            set
            {
                if (value)
                {
                    lbExtract.BackColor = SystemColors.Highlight;
                    lbExtract.ForeColor = SystemColors.HighlightText;
                }
                else
                {
                    lbExtract.BackColor = gbExtract.BackColor;
                    lbExtract.ForeColor = gbExtract.ForeColor;
                }
            }
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            Installer.Install(CurrentScope);
            UpdateState();
        }

        private void uninstallButton_Click(object sender, EventArgs e)
        {
            Installer.Uninstall(CurrentScope);
            UpdateState();
        }

        private void allUsersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            var installed = false;
            var installedCurrentUser = Installer.IsInstalled(InstallScope.CurrentUser);
            var installedLocalMachine = Installer.IsInstalled(InstallScope.LocalMachine);

            if (installedLocalMachine)
            {
                installed = true;
                allUsersCheckBox.Checked = true;
            }

            if (installedCurrentUser)
            {
                installed = true;
                allUsersCheckBox.Checked = false;
            }

            installButton.SetShield(allUsersCheckBox.Checked);
            uninstallButton.SetShield(allUsersCheckBox.Checked);
            
            installButton.Enabled = !installed;
            uninstallButton.Enabled = installed;

            allUsersCheckBox.Visible = !installed;
        }
        
        private void lbExtract_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileDrop"))
            {
                e.Effect = DragDropEffects.Copy;
                DropHighlighted = true;
            }
        }

        private void lbExtract_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileDrop"))
            {
                var filePaths = e.Data.GetData("FileDrop") as string[];
                if (filePaths != null)
                {
                    var reports = filePaths
                        .Where(x => x.EndsWith(".rdl", StringComparison.OrdinalIgnoreCase));

                    foreach (var report in reports)
                    {
                        Extractor.ExtractImages(report);
                    }
                }
            }

            DropHighlighted = false;
        }

        private void lbExtract_DragLeave(object sender, EventArgs e)
        {
            DropHighlighted = false;
        }
    }
}
