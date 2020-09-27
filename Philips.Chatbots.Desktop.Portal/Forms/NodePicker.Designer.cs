namespace Philips.Chatbots.Desktop.Portal
{
    partial class NodePicker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodePicker));
            this.cbxNodes = new System.Windows.Forms.ComboBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.lblNodeId = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbxNodes
            // 
            this.cbxNodes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxNodes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxNodes.FormattingEnabled = true;
            this.cbxNodes.Location = new System.Drawing.Point(51, 51);
            this.cbxNodes.Name = "cbxNodes";
            this.cbxNodes.Size = new System.Drawing.Size(337, 28);
            this.cbxNodes.TabIndex = 1;
            this.cbxNodes.SelectedValueChanged += new System.EventHandler(this.cbxNodes_SelectedValueChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(51, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(87, 20);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Select node";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(294, 129);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(94, 29);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Pick";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lblNodeId
            // 
            this.lblNodeId.AutoSize = true;
            this.lblNodeId.Location = new System.Drawing.Point(54, 84);
            this.lblNodeId.Name = "lblNodeId";
            this.lblNodeId.Size = new System.Drawing.Size(73, 20);
            this.lblNodeId.TabIndex = 4;
            this.lblNodeId.Text = "[Node id]";
            // 
            // NodePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 178);
            this.Controls.Add(this.lblNodeId);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cbxNodes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NodePicker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Node picker";
            this.Load += new System.EventHandler(this.NodePicker_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbxNodes;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblNodeId;
    }
}