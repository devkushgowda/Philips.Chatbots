namespace Philips.Chatbots.Desktop.Portal
{
    partial class NeuralResourcesEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NeuralResourcesEditor));
            this.gbTitle = new System.Windows.Forms.GroupBox();
            this.treeViewNeuralresources = new System.Windows.Forms.TreeView();
            this.gbNeuralResourceConfiguration = new System.Windows.Forms.GroupBox();
            this.chkBxIsLocal = new System.Windows.Forms.CheckBox();
            this.tbLocation = new System.Windows.Forms.TextBox();
            this.lblLocation = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lnkLabels = new System.Windows.Forms.LinkLabel();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblId = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblQuestionTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.tbId = new System.Windows.Forms.TextBox();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbQuestionTitle = new System.Windows.Forms.TextBox();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.lblResourceType = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.cbResourceType = new System.Windows.Forms.ComboBox();
            this.gbTitle.SuspendLayout();
            this.gbNeuralResourceConfiguration.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTitle
            // 
            this.gbTitle.Controls.Add(this.treeViewNeuralresources);
            this.gbTitle.Location = new System.Drawing.Point(12, 12);
            this.gbTitle.Name = "gbTitle";
            this.gbTitle.Size = new System.Drawing.Size(253, 454);
            this.gbTitle.TabIndex = 1;
            this.gbTitle.TabStop = false;
            this.gbTitle.Text = "Neural resource nodes";
            // 
            // treeViewNeuralresources
            // 
            this.treeViewNeuralresources.Location = new System.Drawing.Point(3, 23);
            this.treeViewNeuralresources.Name = "treeViewNeuralresources";
            this.treeViewNeuralresources.Size = new System.Drawing.Size(250, 425);
            this.treeViewNeuralresources.TabIndex = 0;
            this.treeViewNeuralresources.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewNeuralresources_AfterSelect);
            // 
            // gbNeuralResourceConfiguration
            // 
            this.gbNeuralResourceConfiguration.Controls.Add(this.chkBxIsLocal);
            this.gbNeuralResourceConfiguration.Controls.Add(this.tbLocation);
            this.gbNeuralResourceConfiguration.Controls.Add(this.lblLocation);
            this.gbNeuralResourceConfiguration.Controls.Add(this.groupBox1);
            this.gbNeuralResourceConfiguration.Controls.Add(this.btnApply);
            this.gbNeuralResourceConfiguration.Controls.Add(this.lblId);
            this.gbNeuralResourceConfiguration.Controls.Add(this.lblTitle);
            this.gbNeuralResourceConfiguration.Controls.Add(this.lblQuestionTitle);
            this.gbNeuralResourceConfiguration.Controls.Add(this.lblDescription);
            this.gbNeuralResourceConfiguration.Controls.Add(this.tbId);
            this.gbNeuralResourceConfiguration.Controls.Add(this.tbDescription);
            this.gbNeuralResourceConfiguration.Controls.Add(this.tbQuestionTitle);
            this.gbNeuralResourceConfiguration.Controls.Add(this.tbTitle);
            this.gbNeuralResourceConfiguration.Controls.Add(this.lblResourceType);
            this.gbNeuralResourceConfiguration.Controls.Add(this.tbName);
            this.gbNeuralResourceConfiguration.Controls.Add(this.lblName);
            this.gbNeuralResourceConfiguration.Controls.Add(this.cbResourceType);
            this.gbNeuralResourceConfiguration.Location = new System.Drawing.Point(271, 12);
            this.gbNeuralResourceConfiguration.Name = "gbNeuralResourceConfiguration";
            this.gbNeuralResourceConfiguration.Size = new System.Drawing.Size(495, 454);
            this.gbNeuralResourceConfiguration.TabIndex = 2;
            this.gbNeuralResourceConfiguration.TabStop = false;
            this.gbNeuralResourceConfiguration.Text = "Neural resource configuration";
            // 
            // chkBxIsLocal
            // 
            this.chkBxIsLocal.AutoSize = true;
            this.chkBxIsLocal.Location = new System.Drawing.Point(310, 249);
            this.chkBxIsLocal.Name = "chkBxIsLocal";
            this.chkBxIsLocal.Size = new System.Drawing.Size(137, 24);
            this.chkBxIsLocal.TabIndex = 7;
            this.chkBxIsLocal.Text = "Is local resource";
            this.chkBxIsLocal.UseVisualStyleBackColor = true;
            // 
            // tbLocation
            // 
            this.tbLocation.Location = new System.Drawing.Point(139, 290);
            this.tbLocation.Name = "tbLocation";
            this.tbLocation.Size = new System.Drawing.Size(350, 27);
            this.tbLocation.TabIndex = 2;
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(8, 293);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(66, 20);
            this.lblLocation.TabIndex = 1;
            this.lblLocation.Text = "Location";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lnkLabels);
            this.groupBox1.Location = new System.Drawing.Point(139, 335);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 62);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Quick links";
            // 
            // lnkLabels
            // 
            this.lnkLabels.AutoSize = true;
            this.lnkLabels.Location = new System.Drawing.Point(6, 31);
            this.lnkLabels.Name = "lnkLabels";
            this.lnkLabels.Size = new System.Drawing.Size(51, 20);
            this.lnkLabels.TabIndex = 5;
            this.lnkLabels.TabStop = true;
            this.lnkLabels.Text = "Labels";
            this.lnkLabels.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLabels_LinkClicked);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(395, 410);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(94, 29);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "Update";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(8, 32);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(22, 20);
            this.lblId.TabIndex = 1;
            this.lblId.Text = "Id";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(8, 118);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(38, 20);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Title";
            // 
            // lblQuestionTitle
            // 
            this.lblQuestionTitle.AutoSize = true;
            this.lblQuestionTitle.Location = new System.Drawing.Point(8, 161);
            this.lblQuestionTitle.Name = "lblQuestionTitle";
            this.lblQuestionTitle.Size = new System.Drawing.Size(98, 20);
            this.lblQuestionTitle.TabIndex = 1;
            this.lblQuestionTitle.Text = "Question title";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(8, 206);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(85, 20);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description";
            // 
            // tbId
            // 
            this.tbId.Location = new System.Drawing.Point(139, 29);
            this.tbId.Name = "tbId";
            this.tbId.ReadOnly = true;
            this.tbId.Size = new System.Drawing.Size(350, 27);
            this.tbId.TabIndex = 2;
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(139, 203);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(350, 27);
            this.tbDescription.TabIndex = 2;
            // 
            // tbQuestionTitle
            // 
            this.tbQuestionTitle.Location = new System.Drawing.Point(139, 158);
            this.tbQuestionTitle.Name = "tbQuestionTitle";
            this.tbQuestionTitle.Size = new System.Drawing.Size(350, 27);
            this.tbQuestionTitle.TabIndex = 2;
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(139, 115);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(350, 27);
            this.tbTitle.TabIndex = 2;
            // 
            // lblResourceType
            // 
            this.lblResourceType.AutoSize = true;
            this.lblResourceType.Location = new System.Drawing.Point(8, 250);
            this.lblResourceType.Name = "lblResourceType";
            this.lblResourceType.Size = new System.Drawing.Size(102, 20);
            this.lblResourceType.TabIndex = 1;
            this.lblResourceType.Text = "Resource type";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(139, 73);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(350, 27);
            this.tbName.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(8, 76);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(49, 20);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // cbResourceType
            // 
            this.cbResourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResourceType.FormattingEnabled = true;
            this.cbResourceType.Location = new System.Drawing.Point(139, 247);
            this.cbResourceType.Name = "cbResourceType";
            this.cbResourceType.Size = new System.Drawing.Size(147, 28);
            this.cbResourceType.TabIndex = 3;
            // 
            // NeuralResourcesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 480);
            this.Controls.Add(this.gbNeuralResourceConfiguration);
            this.Controls.Add(this.gbTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NeuralResourcesEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Neural resource manager";
            this.Load += new System.EventHandler(this.NeuralResourcesEditor_Load);
            this.gbTitle.ResumeLayout(false);
            this.gbNeuralResourceConfiguration.ResumeLayout(false);
            this.gbNeuralResourceConfiguration.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbTitle;
        private System.Windows.Forms.GroupBox gbNeuralResourceConfiguration;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel lnkLabels;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblQuestionTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.TextBox tbQuestionTitle;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label lblResourceType;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ComboBox cbResourceType;
        private System.Windows.Forms.CheckBox chkBxIsLocal;
        private System.Windows.Forms.TextBox tbLocation;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TreeView treeViewNeuralresources;
    }
}