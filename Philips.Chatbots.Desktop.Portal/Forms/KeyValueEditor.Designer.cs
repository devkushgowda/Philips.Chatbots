namespace Philips.Chatbots.Desktop.Portal
{
    partial class KeyValueEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyValueEditor));
            this.btnSave = new System.Windows.Forms.Button();
            this.gbTitle = new System.Windows.Forms.GroupBox();
            this.dataGridViewKeyValues = new System.Windows.Forms.DataGridView();
            this.gbTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKeyValues)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(423, 409);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 29);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gbTitle
            // 
            this.gbTitle.Controls.Add(this.dataGridViewKeyValues);
            this.gbTitle.Location = new System.Drawing.Point(12, 12);
            this.gbTitle.Name = "gbTitle";
            this.gbTitle.Size = new System.Drawing.Size(508, 391);
            this.gbTitle.TabIndex = 2;
            this.gbTitle.TabStop = false;
            this.gbTitle.Text = "Key value editor";
            // 
            // dataGridViewKeyValues
            // 
            this.dataGridViewKeyValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewKeyValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewKeyValues.Location = new System.Drawing.Point(3, 23);
            this.dataGridViewKeyValues.Name = "dataGridViewKeyValues";
            this.dataGridViewKeyValues.RowHeadersWidth = 51;
            this.dataGridViewKeyValues.Size = new System.Drawing.Size(502, 365);
            this.dataGridViewKeyValues.TabIndex = 0;
            this.dataGridViewKeyValues.Text = "dataGridView1";
            this.dataGridViewKeyValues.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewKeyValues_RowsAdded);
            this.dataGridViewKeyValues.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewKeyValues_KeyDown);
            // 
            // KeyValueEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 450);
            this.Controls.Add(this.gbTitle);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KeyValueEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Key value editor";
            this.gbTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewKeyValues)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbTitle;
        private System.Windows.Forms.DataGridView dataGridViewKeyValues;
    }
}