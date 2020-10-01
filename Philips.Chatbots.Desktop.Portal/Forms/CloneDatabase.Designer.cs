namespace Philips.Chatbots.Desktop.Portal
{
    partial class CloneDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloneDatabase));
            this.lblSource = new System.Windows.Forms.Label();
            this.cbxFromDb = new System.Windows.Forms.ComboBox();
            this.lblDestination = new System.Windows.Forms.Label();
            this.cbxToDb = new System.Windows.Forms.ComboBox();
            this.btnClone = new System.Windows.Forms.Button();
            this.chkbDrop = new System.Windows.Forms.CheckBox();
            this.cbxChatProfile = new System.Windows.Forms.ComboBox();
            this.chkbSelectProfile = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(48, 25);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(119, 20);
            this.lblSource.TabIndex = 0;
            this.lblSource.Text = "Source database";
            // 
            // cbxFromDb
            // 
            this.cbxFromDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFromDb.FormattingEnabled = true;
            this.cbxFromDb.Location = new System.Drawing.Point(48, 60);
            this.cbxFromDb.Name = "cbxFromDb";
            this.cbxFromDb.Size = new System.Drawing.Size(364, 28);
            this.cbxFromDb.TabIndex = 1;
            this.cbxFromDb.SelectedIndexChanged += new System.EventHandler(this.cbxFromDb_SelectedIndexChanged);
            // 
            // lblDestination
            // 
            this.lblDestination.AutoSize = true;
            this.lblDestination.Location = new System.Drawing.Point(48, 102);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(150, 20);
            this.lblDestination.TabIndex = 2;
            this.lblDestination.Text = "Destination database";
            // 
            // cbxToDb
            // 
            this.cbxToDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxToDb.FormattingEnabled = true;
            this.cbxToDb.Location = new System.Drawing.Point(48, 134);
            this.cbxToDb.Name = "cbxToDb";
            this.cbxToDb.Size = new System.Drawing.Size(364, 28);
            this.cbxToDb.TabIndex = 3;
            // 
            // btnClone
            // 
            this.btnClone.Location = new System.Drawing.Point(320, 279);
            this.btnClone.Name = "btnClone";
            this.btnClone.Size = new System.Drawing.Size(92, 29);
            this.btnClone.TabIndex = 4;
            this.btnClone.Text = "Clone";
            this.btnClone.UseVisualStyleBackColor = true;
            this.btnClone.Click += new System.EventHandler(this.btnClone_Click);
            // 
            // chkbDrop
            // 
            this.chkbDrop.AutoSize = true;
            this.chkbDrop.Checked = true;
            this.chkbDrop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbDrop.Location = new System.Drawing.Point(48, 238);
            this.chkbDrop.Name = "chkbDrop";
            this.chkbDrop.Size = new System.Drawing.Size(208, 24);
            this.chkbDrop.TabIndex = 5;
            this.chkbDrop.Text = "Drop destination database";
            this.chkbDrop.UseVisualStyleBackColor = true;
            // 
            // cbxChatProfile
            // 
            this.cbxChatProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxChatProfile.Enabled = false;
            this.cbxChatProfile.FormattingEnabled = true;
            this.cbxChatProfile.Location = new System.Drawing.Point(230, 186);
            this.cbxChatProfile.Name = "cbxChatProfile";
            this.cbxChatProfile.Size = new System.Drawing.Size(182, 28);
            this.cbxChatProfile.TabIndex = 3;
            // 
            // chkbSelectProfile
            // 
            this.chkbSelectProfile.AutoSize = true;
            this.chkbSelectProfile.Location = new System.Drawing.Point(48, 188);
            this.chkbSelectProfile.Name = "chkbSelectProfile";
            this.chkbSelectProfile.Size = new System.Drawing.Size(176, 24);
            this.chkbSelectProfile.TabIndex = 5;
            this.chkbSelectProfile.Text = "Clone selected profile";
            this.chkbSelectProfile.UseVisualStyleBackColor = true;
            this.chkbSelectProfile.CheckedChanged += new System.EventHandler(this.chkbSelectProfile_CheckedChanged);
            // 
            // CloneDatabase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 333);
            this.Controls.Add(this.chkbSelectProfile);
            this.Controls.Add(this.cbxChatProfile);
            this.Controls.Add(this.chkbDrop);
            this.Controls.Add(this.btnClone);
            this.Controls.Add(this.cbxToDb);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.cbxFromDb);
            this.Controls.Add(this.lblSource);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CloneDatabase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clone database";
            this.Load += new System.EventHandler(this.ImportDatabase_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.ComboBox cbxFromDb;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.ComboBox cbxToDb;
        private System.Windows.Forms.Button btnClone;
        private System.Windows.Forms.CheckBox chkbDrop;
        private System.Windows.Forms.ComboBox cbxChatProfile;
        private System.Windows.Forms.CheckBox chkbSelectProfile;
    }
}