namespace Philips.Chatbots.Desktop.Portal
{
    partial class LinkExpressionEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkExpressionEditor));
            this.btnSave = new System.Windows.Forms.Button();
            this.lblQuestionTitle = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.tbHint = new System.Windows.Forms.TextBox();
            this.tbQuestionTitle = new System.Windows.Forms.TextBox();
            this.chkBxSkipEval = new System.Windows.Forms.CheckBox();
            this.dataGridViewOptions = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOptions)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(566, 344);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 29);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblQuestionTitle
            // 
            this.lblQuestionTitle.AutoSize = true;
            this.lblQuestionTitle.Location = new System.Drawing.Point(33, 29);
            this.lblQuestionTitle.Name = "lblQuestionTitle";
            this.lblQuestionTitle.Size = new System.Drawing.Size(98, 20);
            this.lblQuestionTitle.TabIndex = 1;
            this.lblQuestionTitle.Text = "Question title";
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Location = new System.Drawing.Point(33, 65);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(37, 20);
            this.lblHint.TabIndex = 2;
            this.lblHint.Text = "Hint";
            // 
            // tbHint
            // 
            this.tbHint.Location = new System.Drawing.Point(166, 62);
            this.tbHint.Name = "tbHint";
            this.tbHint.Size = new System.Drawing.Size(494, 27);
            this.tbHint.TabIndex = 4;
            // 
            // tbQuestionTitle
            // 
            this.tbQuestionTitle.Location = new System.Drawing.Point(166, 26);
            this.tbQuestionTitle.Name = "tbQuestionTitle";
            this.tbQuestionTitle.Size = new System.Drawing.Size(494, 27);
            this.tbQuestionTitle.TabIndex = 5;
            // 
            // chkBxSkipEval
            // 
            this.chkBxSkipEval.AutoSize = true;
            this.chkBxSkipEval.Location = new System.Drawing.Point(166, 99);
            this.chkBxSkipEval.Name = "chkBxSkipEval";
            this.chkBxSkipEval.Size = new System.Drawing.Size(132, 24);
            this.chkBxSkipEval.TabIndex = 6;
            this.chkBxSkipEval.Text = "Skip evaluation";
            this.chkBxSkipEval.UseVisualStyleBackColor = true;
            // 
            // dataGridViewOptions
            // 
            this.dataGridViewOptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOptions.Location = new System.Drawing.Point(166, 130);
            this.dataGridViewOptions.Name = "dataGridViewOptions";
            this.dataGridViewOptions.RowHeadersWidth = 51;
            this.dataGridViewOptions.Size = new System.Drawing.Size(494, 188);
            this.dataGridViewOptions.TabIndex = 7;
            this.dataGridViewOptions.Text = "dataGridView1";
            this.dataGridViewOptions.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewOptions_RowsAdded);
            this.dataGridViewOptions.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewOptions_KeyDown);
            // 
            // LinkExpressionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 405);
            this.Controls.Add(this.dataGridViewOptions);
            this.Controls.Add(this.chkBxSkipEval);
            this.Controls.Add(this.tbQuestionTitle);
            this.Controls.Add(this.tbHint);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.lblQuestionTitle);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LinkExpressionEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Link expression editor";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOptions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblQuestionTitle;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.TextBox tbHint;
        private System.Windows.Forms.TextBox tbQuestionTitle;
        private System.Windows.Forms.CheckBox chkBxSkipEval;
        private System.Windows.Forms.DataGridView dataGridViewOptions;
    }
}