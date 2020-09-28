namespace Philips.Chatbots.Desktop.Portal
{
    partial class NeuralActionsEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NeuralActionsEditor));
            this.btnApply = new System.Windows.Forms.Button();
            this.lblId = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblQuestionTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.tbId = new System.Windows.Forms.TextBox();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbQuestionTitle = new System.Windows.Forms.TextBox();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.lblActionType = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.cbActionType = new System.Windows.Forms.ComboBox();
            this.gbNeuralActionConfiguration = new System.Windows.Forms.GroupBox();
            this.lblResources = new System.Windows.Forms.Label();
            this.dataGridViewResources = new System.Windows.Forms.DataGridView();
            this.lnkLabels = new System.Windows.Forms.LinkLabel();
            this.treeViewNeuralActions = new System.Windows.Forms.TreeView();
            this.gbNeuralActionNodes = new System.Windows.Forms.GroupBox();
            this.gbNeuralActionConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResources)).BeginInit();
            this.gbNeuralActionNodes.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(415, 410);
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
            this.tbId.Location = new System.Drawing.Point(110, 29);
            this.tbId.Name = "tbId";
            this.tbId.ReadOnly = true;
            this.tbId.Size = new System.Drawing.Size(399, 27);
            this.tbId.TabIndex = 2;
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(110, 203);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(399, 27);
            this.tbDescription.TabIndex = 2;
            // 
            // tbQuestionTitle
            // 
            this.tbQuestionTitle.Location = new System.Drawing.Point(110, 158);
            this.tbQuestionTitle.Name = "tbQuestionTitle";
            this.tbQuestionTitle.Size = new System.Drawing.Size(399, 27);
            this.tbQuestionTitle.TabIndex = 2;
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(110, 115);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(399, 27);
            this.tbTitle.TabIndex = 2;
            // 
            // lblActionType
            // 
            this.lblActionType.AutoSize = true;
            this.lblActionType.Location = new System.Drawing.Point(8, 250);
            this.lblActionType.Name = "lblActionType";
            this.lblActionType.Size = new System.Drawing.Size(85, 20);
            this.lblActionType.TabIndex = 1;
            this.lblActionType.Text = "Action type";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(110, 73);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(399, 27);
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
            // cbActionType
            // 
            this.cbActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActionType.FormattingEnabled = true;
            this.cbActionType.Location = new System.Drawing.Point(110, 247);
            this.cbActionType.Name = "cbActionType";
            this.cbActionType.Size = new System.Drawing.Size(159, 28);
            this.cbActionType.TabIndex = 3;
            // 
            // gbNeuralActionConfiguration
            // 
            this.gbNeuralActionConfiguration.Controls.Add(this.lblResources);
            this.gbNeuralActionConfiguration.Controls.Add(this.dataGridViewResources);
            this.gbNeuralActionConfiguration.Controls.Add(this.lnkLabels);
            this.gbNeuralActionConfiguration.Controls.Add(this.btnApply);
            this.gbNeuralActionConfiguration.Controls.Add(this.lblId);
            this.gbNeuralActionConfiguration.Controls.Add(this.lblTitle);
            this.gbNeuralActionConfiguration.Controls.Add(this.lblQuestionTitle);
            this.gbNeuralActionConfiguration.Controls.Add(this.lblDescription);
            this.gbNeuralActionConfiguration.Controls.Add(this.tbId);
            this.gbNeuralActionConfiguration.Controls.Add(this.tbDescription);
            this.gbNeuralActionConfiguration.Controls.Add(this.tbQuestionTitle);
            this.gbNeuralActionConfiguration.Controls.Add(this.tbTitle);
            this.gbNeuralActionConfiguration.Controls.Add(this.lblActionType);
            this.gbNeuralActionConfiguration.Controls.Add(this.tbName);
            this.gbNeuralActionConfiguration.Controls.Add(this.lblName);
            this.gbNeuralActionConfiguration.Controls.Add(this.cbActionType);
            this.gbNeuralActionConfiguration.Location = new System.Drawing.Point(252, 12);
            this.gbNeuralActionConfiguration.Name = "gbNeuralActionConfiguration";
            this.gbNeuralActionConfiguration.Size = new System.Drawing.Size(526, 454);
            this.gbNeuralActionConfiguration.TabIndex = 2;
            this.gbNeuralActionConfiguration.TabStop = false;
            this.gbNeuralActionConfiguration.Text = "Neural action configuration";
            // 
            // lblResources
            // 
            this.lblResources.AutoSize = true;
            this.lblResources.Location = new System.Drawing.Point(8, 291);
            this.lblResources.Name = "lblResources";
            this.lblResources.Size = new System.Drawing.Size(75, 20);
            this.lblResources.TabIndex = 1;
            this.lblResources.Text = "Resources";
            // 
            // dataGridViewResources
            // 
            this.dataGridViewResources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResources.Location = new System.Drawing.Point(110, 291);
            this.dataGridViewResources.Name = "dataGridViewResources";
            this.dataGridViewResources.RowHeadersWidth = 51;
            this.dataGridViewResources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewResources.Size = new System.Drawing.Size(399, 113);
            this.dataGridViewResources.TabIndex = 3;
            this.dataGridViewResources.Text = "dataGridView1";
            this.dataGridViewResources.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewResources_RowsAdded);
            // 
            // lnkLabels
            // 
            this.lnkLabels.AutoSize = true;
            this.lnkLabels.Location = new System.Drawing.Point(314, 250);
            this.lnkLabels.Name = "lnkLabels";
            this.lnkLabels.Size = new System.Drawing.Size(51, 20);
            this.lnkLabels.TabIndex = 4;
            this.lnkLabels.TabStop = true;
            this.lnkLabels.Text = "Labels";
            this.lnkLabels.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLabels_LinkClicked);
            // 
            // treeViewNeuralActions
            // 
            this.treeViewNeuralActions.Location = new System.Drawing.Point(3, 23);
            this.treeViewNeuralActions.Name = "treeViewNeuralActions";
            this.treeViewNeuralActions.Size = new System.Drawing.Size(227, 425);
            this.treeViewNeuralActions.TabIndex = 0;
            this.treeViewNeuralActions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewNeuralActions_AfterSelect);
            // 
            // gbNeuralActionNodes
            // 
            this.gbNeuralActionNodes.Controls.Add(this.treeViewNeuralActions);
            this.gbNeuralActionNodes.Location = new System.Drawing.Point(12, 12);
            this.gbNeuralActionNodes.Name = "gbNeuralActionNodes";
            this.gbNeuralActionNodes.Size = new System.Drawing.Size(234, 454);
            this.gbNeuralActionNodes.TabIndex = 1;
            this.gbNeuralActionNodes.TabStop = false;
            this.gbNeuralActionNodes.Text = "Neural action nodes";
            // 
            // NeuralActionsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 477);
            this.Controls.Add(this.gbNeuralActionNodes);
            this.Controls.Add(this.gbNeuralActionConfiguration);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NeuralActionsEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Neural actions manager";
            this.Load += new System.EventHandler(this.NeuralActionsEditor_Load);
            this.gbNeuralActionConfiguration.ResumeLayout(false);
            this.gbNeuralActionConfiguration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResources)).EndInit();
            this.gbNeuralActionNodes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

       
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblQuestionTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.TextBox tbQuestionTitle;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label lblActionType;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ComboBox cbActionType;
        private System.Windows.Forms.GroupBox gbNeuralActionConfiguration;
        private System.Windows.Forms.TreeView treeViewNeuralActions;
        private System.Windows.Forms.GroupBox gbNeuralActionNodes;
        private System.Windows.Forms.LinkLabel lnkLabels;
        private System.Windows.Forms.DataGridView dataGridViewResources;
        private System.Windows.Forms.Label lblResources;
    }
}