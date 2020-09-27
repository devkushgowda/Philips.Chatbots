namespace Philips.Chatbots.Desktop.Portal
{
    partial class DecisionExpressionEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DecisionExpressionEditor));
            this.btnSave = new System.Windows.Forms.Button();
            this.lblQuestionTitle = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.tbQuestionTitle = new System.Windows.Forms.TextBox();
            this.tbHint = new System.Windows.Forms.TextBox();
            this.gbExpression = new System.Windows.Forms.GroupBox();
            this.cbxFallbackActionNode = new System.Windows.Forms.ComboBox();
            this.cbxForwardActionNode = new System.Windows.Forms.ComboBox();
            this.dataGridViewActionItems = new System.Windows.Forms.DataGridView();
            this.cbxForwardActionType = new System.Windows.Forms.ComboBox();
            this.cbxFallbackActionType = new System.Windows.Forms.ComboBox();
            this.lblFallbackAction = new System.Windows.Forms.Label();
            this.lblForwardAction = new System.Windows.Forms.Label();
            this.chkBxSkipEval = new System.Windows.Forms.CheckBox();
            this.lblDataType = new System.Windows.Forms.Label();
            this.cbxDataType = new System.Windows.Forms.ComboBox();
            this.gbExpression.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewActionItems)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(606, 471);
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
            this.lblQuestionTitle.Location = new System.Drawing.Point(12, 31);
            this.lblQuestionTitle.Name = "lblQuestionTitle";
            this.lblQuestionTitle.Size = new System.Drawing.Size(98, 20);
            this.lblQuestionTitle.TabIndex = 1;
            this.lblQuestionTitle.Text = "Question title";
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Location = new System.Drawing.Point(12, 67);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(37, 20);
            this.lblHint.TabIndex = 2;
            this.lblHint.Text = "Hint";
            // 
            // tbQuestionTitle
            // 
            this.tbQuestionTitle.Location = new System.Drawing.Point(123, 28);
            this.tbQuestionTitle.Name = "tbQuestionTitle";
            this.tbQuestionTitle.Size = new System.Drawing.Size(593, 27);
            this.tbQuestionTitle.TabIndex = 5;
            // 
            // tbHint
            // 
            this.tbHint.Location = new System.Drawing.Point(123, 64);
            this.tbHint.Name = "tbHint";
            this.tbHint.Size = new System.Drawing.Size(593, 27);
            this.tbHint.TabIndex = 4;
            // 
            // gbExpression
            // 
            this.gbExpression.Controls.Add(this.cbxDataType);
            this.gbExpression.Controls.Add(this.lblDataType);
            this.gbExpression.Controls.Add(this.cbxFallbackActionNode);
            this.gbExpression.Controls.Add(this.cbxForwardActionNode);
            this.gbExpression.Controls.Add(this.dataGridViewActionItems);
            this.gbExpression.Controls.Add(this.cbxForwardActionType);
            this.gbExpression.Controls.Add(this.cbxFallbackActionType);
            this.gbExpression.Controls.Add(this.lblFallbackAction);
            this.gbExpression.Controls.Add(this.lblForwardAction);
            this.gbExpression.Controls.Add(this.chkBxSkipEval);
            this.gbExpression.Location = new System.Drawing.Point(123, 108);
            this.gbExpression.Name = "gbExpression";
            this.gbExpression.Size = new System.Drawing.Size(593, 357);
            this.gbExpression.TabIndex = 6;
            this.gbExpression.TabStop = false;
            this.gbExpression.Text = "Expression";
            // 
            // cbxFallbackActionNode
            // 
            this.cbxFallbackActionNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFallbackActionNode.FormattingEnabled = true;
            this.cbxFallbackActionNode.Location = new System.Drawing.Point(294, 111);
            this.cbxFallbackActionNode.Name = "cbxFallbackActionNode";
            this.cbxFallbackActionNode.Size = new System.Drawing.Size(283, 28);
            this.cbxFallbackActionNode.TabIndex = 8;
            // 
            // cbxForwardActionNode
            // 
            this.cbxForwardActionNode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForwardActionNode.FormattingEnabled = true;
            this.cbxForwardActionNode.Location = new System.Drawing.Point(294, 69);
            this.cbxForwardActionNode.Name = "cbxForwardActionNode";
            this.cbxForwardActionNode.Size = new System.Drawing.Size(283, 28);
            this.cbxForwardActionNode.TabIndex = 8;
            // 
            // dataGridViewActionItems
            // 
            this.dataGridViewActionItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewActionItems.Location = new System.Drawing.Point(23, 154);
            this.dataGridViewActionItems.Name = "dataGridViewActionItems";
            this.dataGridViewActionItems.RowHeadersWidth = 51;
            this.dataGridViewActionItems.Size = new System.Drawing.Size(554, 188);
            this.dataGridViewActionItems.TabIndex = 7;
            this.dataGridViewActionItems.Text = "dataGridView2";
            this.dataGridViewActionItems.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.dataGridViewActionItems_RowsAdded);
            this.dataGridViewActionItems.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewActionItems_KeyDown);
            // 
            // cbxForwardActionType
            // 
            this.cbxForwardActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxForwardActionType.FormattingEnabled = true;
            this.cbxForwardActionType.Location = new System.Drawing.Point(137, 69);
            this.cbxForwardActionType.Name = "cbxForwardActionType";
            this.cbxForwardActionType.Size = new System.Drawing.Size(151, 28);
            this.cbxForwardActionType.TabIndex = 8;
            this.cbxForwardActionType.SelectedIndexChanged += new System.EventHandler(this.cbxForwardActionType_SelectedIndexChanged);
            // 
            // cbxFallbackActionType
            // 
            this.cbxFallbackActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFallbackActionType.FormattingEnabled = true;
            this.cbxFallbackActionType.Location = new System.Drawing.Point(137, 111);
            this.cbxFallbackActionType.Name = "cbxFallbackActionType";
            this.cbxFallbackActionType.Size = new System.Drawing.Size(151, 28);
            this.cbxFallbackActionType.TabIndex = 9;
            this.cbxFallbackActionType.SelectedIndexChanged += new System.EventHandler(this.cbxFallbackActionType_SelectedIndexChanged);
            // 
            // lblFallbackAction
            // 
            this.lblFallbackAction.AutoSize = true;
            this.lblFallbackAction.Location = new System.Drawing.Point(24, 114);
            this.lblFallbackAction.Name = "lblFallbackAction";
            this.lblFallbackAction.Size = new System.Drawing.Size(107, 20);
            this.lblFallbackAction.TabIndex = 11;
            this.lblFallbackAction.Text = "Fallback action";
            // 
            // lblForwardAction
            // 
            this.lblForwardAction.AutoSize = true;
            this.lblForwardAction.Location = new System.Drawing.Point(23, 72);
            this.lblForwardAction.Name = "lblForwardAction";
            this.lblForwardAction.Size = new System.Drawing.Size(108, 20);
            this.lblForwardAction.TabIndex = 10;
            this.lblForwardAction.Text = "Forward action";
            // 
            // chkBxSkipEval
            // 
            this.chkBxSkipEval.AutoSize = true;
            this.chkBxSkipEval.Location = new System.Drawing.Point(294, 33);
            this.chkBxSkipEval.Name = "chkBxSkipEval";
            this.chkBxSkipEval.Size = new System.Drawing.Size(132, 24);
            this.chkBxSkipEval.TabIndex = 6;
            this.chkBxSkipEval.Text = "Skip evaluation";
            this.chkBxSkipEval.UseVisualStyleBackColor = true;
            // 
            // lblDataType
            // 
            this.lblDataType.AutoSize = true;
            this.lblDataType.Location = new System.Drawing.Point(23, 34);
            this.lblDataType.Name = "lblDataType";
            this.lblDataType.Size = new System.Drawing.Size(74, 20);
            this.lblDataType.TabIndex = 12;
            this.lblDataType.Text = "Data type";
            // 
            // cbxDataType
            // 
            this.cbxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDataType.FormattingEnabled = true;
            this.cbxDataType.Location = new System.Drawing.Point(137, 31);
            this.cbxDataType.Name = "cbxDataType";
            this.cbxDataType.Size = new System.Drawing.Size(151, 28);
            this.cbxDataType.TabIndex = 13;
            // 
            // DecisionExpressionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 511);
            this.Controls.Add(this.gbExpression);
            this.Controls.Add(this.tbHint);
            this.Controls.Add(this.tbQuestionTitle);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.lblQuestionTitle);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DecisionExpressionEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Decision expression editor";
            this.gbExpression.ResumeLayout(false);
            this.gbExpression.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewActionItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblQuestionTitle;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.TextBox tbQuestionTitle;
        private System.Windows.Forms.TextBox tbHint;
        private System.Windows.Forms.GroupBox gbExpression;
        private System.Windows.Forms.DataGridView dataGridViewActionItems;
        private System.Windows.Forms.ComboBox cbxForwardActionType;
        private System.Windows.Forms.ComboBox cbxFallbackActionType;
        private System.Windows.Forms.Label lblFallbackAction;
        private System.Windows.Forms.Label lblForwardAction;
        private System.Windows.Forms.CheckBox chkBxSkipEval;
        private System.Windows.Forms.ComboBox cbxFallbackActionNode;
        private System.Windows.Forms.ComboBox cbxForwardActionNode;
        private System.Windows.Forms.ComboBox cbxDataType;
        private System.Windows.Forms.Label lblDataType;
    }
}