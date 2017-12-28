namespace ExtractRdlImages.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.installButton = new System.Windows.Forms.Button();
            this.allUsersCheckBox = new System.Windows.Forms.CheckBox();
            this.uninstallButton = new System.Windows.Forms.Button();
            this.gbExtract = new System.Windows.Forms.GroupBox();
            this.lbExtract = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbExtract.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // installButton
            // 
            this.installButton.BackColor = System.Drawing.SystemColors.Control;
            this.installButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.installButton.Location = new System.Drawing.Point(49, 97);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(138, 32);
            this.installButton.TabIndex = 0;
            this.installButton.Text = "&Install";
            this.installButton.UseVisualStyleBackColor = true;
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // allUsersCheckBox
            // 
            this.allUsersCheckBox.AutoSize = true;
            this.allUsersCheckBox.Location = new System.Drawing.Point(131, 138);
            this.allUsersCheckBox.Name = "allUsersCheckBox";
            this.allUsersCheckBox.Size = new System.Drawing.Size(114, 19);
            this.allUsersCheckBox.TabIndex = 1;
            this.allUsersCheckBox.Text = "Do it for &all users";
            this.allUsersCheckBox.UseVisualStyleBackColor = true;
            this.allUsersCheckBox.CheckedChanged += new System.EventHandler(this.allUsersCheckBox_CheckedChanged);
            // 
            // uninstallButton
            // 
            this.uninstallButton.BackColor = System.Drawing.SystemColors.Control;
            this.uninstallButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.uninstallButton.Location = new System.Drawing.Point(193, 97);
            this.uninstallButton.Name = "uninstallButton";
            this.uninstallButton.Size = new System.Drawing.Size(138, 32);
            this.uninstallButton.TabIndex = 2;
            this.uninstallButton.Text = "&Uninstall";
            this.uninstallButton.UseVisualStyleBackColor = true;
            this.uninstallButton.Click += new System.EventHandler(this.uninstallButton_Click);
            // 
            // gbExtract
            // 
            this.gbExtract.Controls.Add(this.lbExtract);
            this.gbExtract.Location = new System.Drawing.Point(12, 12);
            this.gbExtract.Name = "gbExtract";
            this.gbExtract.Size = new System.Drawing.Size(371, 138);
            this.gbExtract.TabIndex = 3;
            this.gbExtract.TabStop = false;
            this.gbExtract.Text = "Extract images";
            // 
            // lbExtract
            // 
            this.lbExtract.AllowDrop = true;
            this.lbExtract.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbExtract.Location = new System.Drawing.Point(3, 19);
            this.lbExtract.Name = "lbExtract";
            this.lbExtract.Size = new System.Drawing.Size(365, 116);
            this.lbExtract.TabIndex = 0;
            this.lbExtract.Text = "Drop report files here to extract embedded images.";
            this.lbExtract.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbExtract.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbExtract_DragDrop);
            this.lbExtract.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbExtract_DragEnter);
            this.lbExtract.DragLeave += new System.EventHandler(this.lbExtract_DragLeave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.allUsersCheckBox);
            this.groupBox2.Controls.Add(this.installButton);
            this.groupBox2.Controls.Add(this.uninstallButton);
            this.groupBox2.Location = new System.Drawing.Point(13, 157);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(370, 170);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Context menu";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(344, 66);
            this.label2.TabIndex = 3;
            this.label2.Text = "By installing \"Extract RDL Images\" a new option will appear on the Windows Explor" +
    "er context menu to extract images from RDL files.";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 340);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gbExtract);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Extract RDL Images";
            this.gbExtract.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.CheckBox allUsersCheckBox;
        private System.Windows.Forms.Button uninstallButton;
        private System.Windows.Forms.GroupBox gbExtract;
        private System.Windows.Forms.Label lbExtract;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
    }
}

