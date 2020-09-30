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
            this.lblSuggestions = new System.Windows.Forms.Label();
            this.tbSuggestions = new System.Windows.Forms.TextBox();
            this.tbQuestionTitle = new System.Windows.Forms.TextBox();
            this.chkBxSkipEval = new System.Windows.Forms.CheckBox();
            this.dataGridViewOptions = new System.Windows.Forms.DataGridView();
            this.lnkHintEditor = new System.Windows.Forms.LinkLabel();
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
            // lblSuggestions
            // 
            this.lblSuggestions.AutoSize = true;
            this.lblSuggestions.Location = new System.Drawing.Point(33, 65);
            this.lblSuggestions.Name = "lblSuggestions";
            this.lblSuggestions.Size = new System.Drawing.Size(89, 20);
            this.lblSuggestions.TabIndex = 2;
            this.lblSuggestions.Text = "Suggestions";
            // 
            // tbSuggestions
            // 
            this.tbSuggestions.Location = new System.Drawing.Point(166, 62);
            this.tbSuggestions.Name = "tbSuggestions";
            this.tbSuggestions.Size = new System.Drawing.Size(402, 27);
            this.tbSuggestions.TabIndex = 4;
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
            // lnkHintEditor
            // 
            this.lnkHintEditor.AutoSize = true;
            this.lnkHintEditor.Location = new System.Drawing.Point(574, 65);
            this.lnkHintEditor.Name = "lnkHintEditor";
            this.lnkHintEditor.Size = new System.Drawing.Size(77, 20);
            this.lnkHintEditor.TabIndex = 8;
            this.lnkHintEditor.TabStop = true;
            this.lnkHintEditor.Text = "Use editor";
            this.lnkHintEditor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSuggestionsEditor_LinkClicked);
            // 
            // LinkExpressionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(701, 405);
            this.Controls.Add(this.lnkHintEditor);
            this.Controls.Add(this.dataGridViewOptions);
            this.Controls.Add(this.chkBxSkipEval);
            this.Controls.Add(this.tbQuestionTitle);
            this.Controls.Add(this.tbSuggestions);
            this.Controls.Add(this.lblSuggestions);
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
        private System.Windows.Forms.Label lblSuggestions;
        private System.Windows.Forms.TextBox tbSuggestions;
        private System.Windows.Forms.TextBox tbQuestionTitle;
        private System.Windows.Forms.CheckBox chkBxSkipEval;
        private System.Windows.Forms.DataGridView dataGridViewOptions;
        private System.Windows.Forms.LinkLabel lnkHintEditor;
    }
}