namespace Philips.Chatbots.Desktop.Portal
{
    partial class BotConfigurationEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BotConfigurationEditor));
            this.tbBotId = new System.Windows.Forms.TextBox();
            this.lblBotId = new System.Windows.Forms.Label();
            this.lblEndPoint = new System.Windows.Forms.Label();
            this.tbEndpoint = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.gbConfiguration = new System.Windows.Forms.GroupBox();
            this.gbQuickLinks = new System.Windows.Forms.GroupBox();
            this.lnkResourceStrings = new System.Windows.Forms.LinkLabel();
            this.dataGridViewChatProfiles = new System.Windows.Forms.DataGridView();
            this.lblChatProfiles = new System.Windows.Forms.Label();
            this.cbxActiveProfile = new System.Windows.Forms.ComboBox();
            this.lblDataFolder = new System.Windows.Forms.Label();
            this.tbDataFolder = new System.Windows.Forms.TextBox();
            this.lblActiveProfile = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbConfiguration.SuspendLayout();
            this.gbQuickLinks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChatProfiles)).BeginInit();
            this.SuspendLayout();
            // 
            // tbBotId
            // 
            this.tbBotId.Location = new System.Drawing.Point(121, 21);
            this.tbBotId.Name = "tbBotId";
            this.tbBotId.ReadOnly = true;
            this.tbBotId.Size = new System.Drawing.Size(629, 27);
            this.tbBotId.TabIndex = 2;
            // 
            // lblBotId
            // 
            this.lblBotId.AutoSize = true;
            this.lblBotId.Location = new System.Drawing.Point(12, 24);
            this.lblBotId.Name = "lblBotId";
            this.lblBotId.Size = new System.Drawing.Size(49, 20);
            this.lblBotId.TabIndex = 1;
            this.lblBotId.Text = "Name";
            // 
            // lblEndPoint
            // 
            this.lblEndPoint.AutoSize = true;
            this.lblEndPoint.Location = new System.Drawing.Point(12, 116);
            this.lblEndPoint.Name = "lblEndPoint";
            this.lblEndPoint.Size = new System.Drawing.Size(69, 20);
            this.lblEndPoint.TabIndex = 1;
            this.lblEndPoint.Text = "Endpoint";
            // 
            // tbEndpoint
            // 
            this.tbEndpoint.Location = new System.Drawing.Point(121, 113);
            this.tbEndpoint.Name = "tbEndpoint";
            this.tbEndpoint.Size = new System.Drawing.Size(629, 27);
            this.tbEndpoint.TabIndex = 2;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 70);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(85, 20);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description";
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(121, 67);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(629, 27);
            this.tbDescription.TabIndex = 2;
            // 
            // gbConfiguration
            // 
            this.gbConfiguration.Controls.Add(this.gbQuickLinks);
            this.gbConfiguration.Controls.Add(this.dataGridViewChatProfiles);
            this.gbConfiguration.Controls.Add(this.lblChatProfiles);
            this.gbConfiguration.Controls.Add(this.cbxActiveProfile);
            this.gbConfiguration.Controls.Add(this.lblDataFolder);
            this.gbConfiguration.Controls.Add(this.tbDataFolder);
            this.gbConfiguration.Controls.Add(this.lblActiveProfile);
            this.gbConfiguration.Location = new System.Drawing.Point(121, 156);
            this.gbConfiguration.Name = "gbConfiguration";
            this.gbConfiguration.Size = new System.Drawing.Size(629, 324);
            this.gbConfiguration.TabIndex = 3;
            this.gbConfiguration.TabStop = false;
            this.gbConfiguration.Text = "Configuration";
            // 
            // gbQuickLinks
            // 
            this.gbQuickLinks.Controls.Add(this.lnkResourceStrings);
            this.gbQuickLinks.Location = new System.Drawing.Point(127, 119);
            this.gbQuickLinks.Name = "gbQuickLinks";
            this.gbQuickLinks.Size = new System.Drawing.Size(482, 58);
            this.gbQuickLinks.TabIndex = 6;
            this.gbQuickLinks.TabStop = false;
            this.gbQuickLinks.Text = "Quick links";
            // 
            // lnkResourceStrings
            // 
            this.lnkResourceStrings.AutoSize = true;
            this.lnkResourceStrings.Location = new System.Drawing.Point(6, 26);
            this.lnkResourceStrings.Name = "lnkResourceStrings";
            this.lnkResourceStrings.Size = new System.Drawing.Size(116, 20);
            this.lnkResourceStrings.TabIndex = 0;
            this.lnkResourceStrings.TabStop = true;
            this.lnkResourceStrings.Text = "Resource strings";
            this.lnkResourceStrings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkResourceStrings_LinkClicked);
            // 
            // dataGridViewChatProfiles
            // 
            this.dataGridViewChatProfiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewChatProfiles.Location = new System.Drawing.Point(127, 186);
            this.dataGridViewChatProfiles.Name = "dataGridViewChatProfiles";
            this.dataGridViewChatProfiles.RowHeadersWidth = 51;
            this.dataGridViewChatProfiles.Size = new System.Drawing.Size(482, 120);
            this.dataGridViewChatProfiles.TabIndex = 5;
            this.dataGridViewChatProfiles.Text = "dataGridView1";
            this.dataGridViewChatProfiles.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewChatProfiles_RowsAdded);
            // 
            // lblChatProfiles
            // 
            this.lblChatProfiles.AutoSize = true;
            this.lblChatProfiles.Location = new System.Drawing.Point(15, 177);
            this.lblChatProfiles.Name = "lblChatProfiles";
            this.lblChatProfiles.Size = new System.Drawing.Size(93, 20);
            this.lblChatProfiles.TabIndex = 4;
            this.lblChatProfiles.Text = "Chat profiles";
            // 
            // cbxActiveProfile
            // 
            this.cbxActiveProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxActiveProfile.FormattingEnabled = true;
            this.cbxActiveProfile.Location = new System.Drawing.Point(127, 76);
            this.cbxActiveProfile.Name = "cbxActiveProfile";
            this.cbxActiveProfile.Size = new System.Drawing.Size(482, 28);
            this.cbxActiveProfile.TabIndex = 3;
            // 
            // lblDataFolder
            // 
            this.lblDataFolder.AutoSize = true;
            this.lblDataFolder.Location = new System.Drawing.Point(15, 33);
            this.lblDataFolder.Name = "lblDataFolder";
            this.lblDataFolder.Size = new System.Drawing.Size(85, 20);
            this.lblDataFolder.TabIndex = 1;
            this.lblDataFolder.Text = "Data folder";
            // 
            // tbDataFolder
            // 
            this.tbDataFolder.Location = new System.Drawing.Point(127, 30);
            this.tbDataFolder.Name = "tbDataFolder";
            this.tbDataFolder.Size = new System.Drawing.Size(482, 27);
            this.tbDataFolder.TabIndex = 2;
            // 
            // lblActiveProfile
            // 
            this.lblActiveProfile.AutoSize = true;
            this.lblActiveProfile.Location = new System.Drawing.Point(15, 79);
            this.lblActiveProfile.Name = "lblActiveProfile";
            this.lblActiveProfile.Size = new System.Drawing.Size(98, 20);
            this.lblActiveProfile.TabIndex = 1;
            this.lblActiveProfile.Text = "Active profile";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(656, 493);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 29);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // BotConfigurationEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 534);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbConfiguration);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.tbEndpoint);
            this.Controls.Add(this.lblEndPoint);
            this.Controls.Add(this.lblBotId);
            this.Controls.Add(this.tbBotId);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BotConfigurationEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bot configuration";
            this.gbConfiguration.ResumeLayout(false);
            this.gbConfiguration.PerformLayout();
            this.gbQuickLinks.ResumeLayout(false);
            this.gbQuickLinks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChatProfiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbBotId;
        private System.Windows.Forms.Label lblBotId;
        private System.Windows.Forms.Label lblEndPoint;
        private System.Windows.Forms.TextBox tbEndpoint;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.GroupBox gbConfiguration;
        private System.Windows.Forms.Label lblDataFolder;
        private System.Windows.Forms.Label lblActiveProfile;
        private System.Windows.Forms.TextBox tbDataFolder;
        private System.Windows.Forms.ComboBox cbxActiveProfile;
        private System.Windows.Forms.GroupBox gbQuickLinks;
        private System.Windows.Forms.LinkLabel lnkResourceStrings;
        private System.Windows.Forms.DataGridView dataGridViewChatProfiles;
        private System.Windows.Forms.Label lblChatProfiles;
        private System.Windows.Forms.Button btnSave;
    }
}