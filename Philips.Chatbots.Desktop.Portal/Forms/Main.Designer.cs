namespace Philips.Chatbots.Desktop.Portal
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.neuralTree = new System.Windows.Forms.TreeView();
            this.gbNeuralNodeConfiguration = new System.Windows.Forms.GroupBox();
            this.gbQuickLinks = new System.Windows.Forms.GroupBox();
            this.lnkLabels = new System.Windows.Forms.LinkLabel();
            this.lnkNotes = new System.Windows.Forms.LinkLabel();
            this.lnkNeuralExpression = new System.Windows.Forms.LinkLabel();
            this.lnkTrainData = new System.Windows.Forms.LinkLabel();
            this.cbxExpressionTypes = new System.Windows.Forms.ComboBox();
            this.lblName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblExpressionType = new System.Windows.Forms.Label();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.tbQuestionTitle = new System.Windows.Forms.TextBox();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tbId = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblQuestionTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.lblProfile = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lnkProgressResultAction = new System.Windows.Forms.LinkLabel();
            this.lnkTrainModel = new System.Windows.Forms.LinkLabel();
            this.lnkBotConfigurations = new System.Windows.Forms.LinkLabel();
            this.lnkNeuralActions = new System.Windows.Forms.LinkLabel();
            this.lnkNeuralResources = new System.Windows.Forms.LinkLabel();
            this.lnkLabelRefreshTree = new System.Windows.Forms.LinkLabel();
            this.cbxChatProfiles = new System.Windows.Forms.ComboBox();
            this.gbNeuralNodeConfiguration.SuspendLayout();
            this.gbQuickLinks.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // neuralTree
            // 
            this.neuralTree.LineColor = System.Drawing.Color.DarkRed;
            this.neuralTree.Location = new System.Drawing.Point(12, 43);
            this.neuralTree.Name = "neuralTree";
            this.neuralTree.Size = new System.Drawing.Size(355, 619);
            this.neuralTree.TabIndex = 1;
            this.neuralTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.neuralTree_AfterSelect);
            // 
            // gbNeuralNodeConfiguration
            // 
            this.gbNeuralNodeConfiguration.Controls.Add(this.gbQuickLinks);
            this.gbNeuralNodeConfiguration.Controls.Add(this.cbxExpressionTypes);
            this.gbNeuralNodeConfiguration.Controls.Add(this.lblName);
            this.gbNeuralNodeConfiguration.Controls.Add(this.tbName);
            this.gbNeuralNodeConfiguration.Controls.Add(this.lblExpressionType);
            this.gbNeuralNodeConfiguration.Controls.Add(this.tbTitle);
            this.gbNeuralNodeConfiguration.Controls.Add(this.tbQuestionTitle);
            this.gbNeuralNodeConfiguration.Controls.Add(this.tbDescription);
            this.gbNeuralNodeConfiguration.Controls.Add(this.tbId);
            this.gbNeuralNodeConfiguration.Controls.Add(this.lblDescription);
            this.gbNeuralNodeConfiguration.Controls.Add(this.lblQuestionTitle);
            this.gbNeuralNodeConfiguration.Controls.Add(this.lblTitle);
            this.gbNeuralNodeConfiguration.Controls.Add(this.lblId);
            this.gbNeuralNodeConfiguration.Controls.Add(this.buttonApply);
            this.gbNeuralNodeConfiguration.Location = new System.Drawing.Point(389, 12);
            this.gbNeuralNodeConfiguration.Name = "gbNeuralNodeConfiguration";
            this.gbNeuralNodeConfiguration.Size = new System.Drawing.Size(592, 520);
            this.gbNeuralNodeConfiguration.TabIndex = 2;
            this.gbNeuralNodeConfiguration.TabStop = false;
            this.gbNeuralNodeConfiguration.Text = "Neural Link Configuration";
            // 
            // gbQuickLinks
            // 
            this.gbQuickLinks.Controls.Add(this.lnkLabels);
            this.gbQuickLinks.Controls.Add(this.lnkNotes);
            this.gbQuickLinks.Controls.Add(this.lnkNeuralExpression);
            this.gbQuickLinks.Controls.Add(this.lnkTrainData);
            this.gbQuickLinks.Location = new System.Drawing.Point(157, 316);
            this.gbQuickLinks.Name = "gbQuickLinks";
            this.gbQuickLinks.Size = new System.Drawing.Size(418, 136);
            this.gbQuickLinks.TabIndex = 6;
            this.gbQuickLinks.TabStop = false;
            this.gbQuickLinks.Text = "Quick links";
            // 
            // lnkLabels
            // 
            this.lnkLabels.AutoSize = true;
            this.lnkLabels.Location = new System.Drawing.Point(271, 43);
            this.lnkLabels.Name = "lnkLabels";
            this.lnkLabels.Size = new System.Drawing.Size(51, 20);
            this.lnkLabels.TabIndex = 5;
            this.lnkLabels.TabStop = true;
            this.lnkLabels.Text = "Labels";
            this.lnkLabels.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLabels_LinkClicked);
            // 
            // lnkNotes
            // 
            this.lnkNotes.AutoSize = true;
            this.lnkNotes.Location = new System.Drawing.Point(53, 43);
            this.lnkNotes.Name = "lnkNotes";
            this.lnkNotes.Size = new System.Drawing.Size(48, 20);
            this.lnkNotes.TabIndex = 5;
            this.lnkNotes.TabStop = true;
            this.lnkNotes.Text = "Notes";
            this.lnkNotes.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLabelNotes_LinkClicked);
            // 
            // lnkNeuralExpression
            // 
            this.lnkNeuralExpression.AutoSize = true;
            this.lnkNeuralExpression.Location = new System.Drawing.Point(53, 85);
            this.lnkNeuralExpression.Name = "lnkNeuralExpression";
            this.lnkNeuralExpression.Size = new System.Drawing.Size(127, 20);
            this.lnkNeuralExpression.TabIndex = 5;
            this.lnkNeuralExpression.TabStop = true;
            this.lnkNeuralExpression.Text = "Neural expression";
            this.lnkNeuralExpression.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnlLabelNeuralExp_LinkClicked);
            // 
            // lnkTrainData
            // 
            this.lnkTrainData.AutoSize = true;
            this.lnkTrainData.Location = new System.Drawing.Point(271, 85);
            this.lnkTrainData.Name = "lnkTrainData";
            this.lnkTrainData.Size = new System.Drawing.Size(75, 20);
            this.lnkTrainData.TabIndex = 5;
            this.lnkTrainData.TabStop = true;
            this.lnkTrainData.Text = "Train data";
            this.lnkTrainData.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnLabelTrainData_LinkClicked);
            // 
            // cbxExpressionTypes
            // 
            this.cbxExpressionTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxExpressionTypes.FormattingEnabled = true;
            this.cbxExpressionTypes.Location = new System.Drawing.Point(157, 256);
            this.cbxExpressionTypes.Name = "cbxExpressionTypes";
            this.cbxExpressionTypes.Size = new System.Drawing.Size(418, 28);
            this.cbxExpressionTypes.TabIndex = 3;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(26, 85);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(49, 20);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(157, 82);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(418, 27);
            this.tbName.TabIndex = 2;
            // 
            // lblExpressionType
            // 
            this.lblExpressionType.AutoSize = true;
            this.lblExpressionType.Location = new System.Drawing.Point(26, 259);
            this.lblExpressionType.Name = "lblExpressionType";
            this.lblExpressionType.Size = new System.Drawing.Size(112, 20);
            this.lblExpressionType.TabIndex = 1;
            this.lblExpressionType.Text = "Expression type";
            // 
            // tbTitle
            // 
            this.tbTitle.Location = new System.Drawing.Point(157, 124);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(418, 27);
            this.tbTitle.TabIndex = 2;
            // 
            // tbQuestionTitle
            // 
            this.tbQuestionTitle.Location = new System.Drawing.Point(157, 167);
            this.tbQuestionTitle.Name = "tbQuestionTitle";
            this.tbQuestionTitle.Size = new System.Drawing.Size(418, 27);
            this.tbQuestionTitle.TabIndex = 2;
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(157, 212);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(418, 27);
            this.tbDescription.TabIndex = 2;
            // 
            // tbId
            // 
            this.tbId.Location = new System.Drawing.Point(157, 38);
            this.tbId.Name = "tbId";
            this.tbId.ReadOnly = true;
            this.tbId.Size = new System.Drawing.Size(418, 27);
            this.tbId.TabIndex = 2;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(26, 215);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(85, 20);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description";
            // 
            // lblQuestionTitle
            // 
            this.lblQuestionTitle.AutoSize = true;
            this.lblQuestionTitle.Location = new System.Drawing.Point(26, 170);
            this.lblQuestionTitle.Name = "lblQuestionTitle";
            this.lblQuestionTitle.Size = new System.Drawing.Size(98, 20);
            this.lblQuestionTitle.TabIndex = 1;
            this.lblQuestionTitle.Text = "Question title";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(26, 127);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(38, 20);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Title";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(26, 41);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(22, 20);
            this.lblId.TabIndex = 1;
            this.lblId.Text = "Id";
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(481, 477);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(94, 29);
            this.buttonApply.TabIndex = 0;
            this.buttonApply.Text = "Update";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(137, 67);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(399, 27);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(137, 244);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(399, 27);
            this.textBox2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Expression type";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(137, 115);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(399, 27);
            this.textBox3.TabIndex = 2;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(137, 158);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(399, 27);
            this.textBox4.TabIndex = 2;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(137, 203);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(399, 27);
            this.textBox5.TabIndex = 2;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(0, 0);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 27);
            this.textBox6.TabIndex = 0;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(137, 67);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(399, 27);
            this.textBox7.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Name";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(137, 244);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(399, 27);
            this.textBox8.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Expression type";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(137, 115);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(399, 27);
            this.textBox9.TabIndex = 2;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(137, 158);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(399, 27);
            this.textBox10.TabIndex = 2;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(137, 203);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(399, 27);
            this.textBox11.TabIndex = 2;
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(0, 0);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(100, 27);
            this.textBox12.TabIndex = 0;
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(137, 67);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(399, 27);
            this.textBox13.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 20);
            this.label5.TabIndex = 1;
            this.label5.Text = "Name";
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(137, 244);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(399, 27);
            this.textBox14.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 247);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "Expression type";
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(137, 115);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(399, 27);
            this.textBox15.TabIndex = 2;
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(137, 158);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(399, 27);
            this.textBox16.TabIndex = 2;
            // 
            // textBox17
            // 
            this.textBox17.Location = new System.Drawing.Point(137, 203);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(399, 27);
            this.textBox17.TabIndex = 2;
            // 
            // textBox18
            // 
            this.textBox18.Location = new System.Drawing.Point(0, 0);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(100, 27);
            this.textBox18.TabIndex = 0;
            // 
            // lblProfile
            // 
            this.lblProfile.AutoSize = true;
            this.lblProfile.Location = new System.Drawing.Point(12, 12);
            this.lblProfile.Name = "lblProfile";
            this.lblProfile.Size = new System.Drawing.Size(98, 20);
            this.lblProfile.TabIndex = 3;
            this.lblProfile.Text = "Active profile";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lnkProgressResultAction);
            this.groupBox2.Controls.Add(this.lnkTrainModel);
            this.groupBox2.Controls.Add(this.lnkBotConfigurations);
            this.groupBox2.Controls.Add(this.lnkNeuralActions);
            this.groupBox2.Controls.Add(this.lnkNeuralResources);
            this.groupBox2.Location = new System.Drawing.Point(389, 538);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(592, 124);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Other actions";
            // 
            // lnkProgressResultAction
            // 
            this.lnkProgressResultAction.AutoSize = true;
            this.lnkProgressResultAction.Location = new System.Drawing.Point(18, 77);
            this.lnkProgressResultAction.Name = "lnkProgressResultAction";
            this.lnkProgressResultAction.Size = new System.Drawing.Size(49, 20);
            this.lnkProgressResultAction.TabIndex = 9;
            this.lnkProgressResultAction.TabStop = true;
            this.lnkProgressResultAction.Text = "Result";
            this.lnkProgressResultAction.Visible = false;
            this.lnkProgressResultAction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkProgressResultAction_LinkClicked);
            // 
            // lnkTrainModel
            // 
            this.lnkTrainModel.AutoSize = true;
            this.lnkTrainModel.Location = new System.Drawing.Point(482, 32);
            this.lnkTrainModel.Name = "lnkTrainModel";
            this.lnkTrainModel.Size = new System.Drawing.Size(88, 20);
            this.lnkTrainModel.TabIndex = 3;
            this.lnkTrainModel.TabStop = true;
            this.lnkTrainModel.Text = "Train model";
            this.lnkTrainModel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkTrainModel_LinkClicked);
            // 
            // lnkBotConfigurations
            // 
            this.lnkBotConfigurations.AutoSize = true;
            this.lnkBotConfigurations.Location = new System.Drawing.Point(329, 32);
            this.lnkBotConfigurations.Name = "lnkBotConfigurations";
            this.lnkBotConfigurations.Size = new System.Drawing.Size(125, 20);
            this.lnkBotConfigurations.TabIndex = 2;
            this.lnkBotConfigurations.TabStop = true;
            this.lnkBotConfigurations.Text = "Bot configuration";
            this.lnkBotConfigurations.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkBotConfigurations_LinkClicked);
            // 
            // lnkNeuralActions
            // 
            this.lnkNeuralActions.AutoSize = true;
            this.lnkNeuralActions.Location = new System.Drawing.Point(183, 32);
            this.lnkNeuralActions.Name = "lnkNeuralActions";
            this.lnkNeuralActions.Size = new System.Drawing.Size(104, 20);
            this.lnkNeuralActions.TabIndex = 1;
            this.lnkNeuralActions.TabStop = true;
            this.lnkNeuralActions.Text = "Neural actions";
            this.lnkNeuralActions.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNeuralActions_LinkClicked);
            // 
            // lnkNeuralResources
            // 
            this.lnkNeuralResources.AutoSize = true;
            this.lnkNeuralResources.Location = new System.Drawing.Point(18, 32);
            this.lnkNeuralResources.Name = "lnkNeuralResources";
            this.lnkNeuralResources.Size = new System.Drawing.Size(119, 20);
            this.lnkNeuralResources.TabIndex = 0;
            this.lnkNeuralResources.TabStop = true;
            this.lnkNeuralResources.Text = "Neural resources";
            this.lnkNeuralResources.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNeuralResources_LinkClicked);
            // 
            // lnkLabelRefreshTree
            // 
            this.lnkLabelRefreshTree.AutoSize = true;
            this.lnkLabelRefreshTree.Location = new System.Drawing.Point(310, 12);
            this.lnkLabelRefreshTree.Name = "lnkLabelRefreshTree";
            this.lnkLabelRefreshTree.Size = new System.Drawing.Size(58, 20);
            this.lnkLabelRefreshTree.TabIndex = 3;
            this.lnkLabelRefreshTree.TabStop = true;
            this.lnkLabelRefreshTree.Text = "Refresh";
            this.lnkLabelRefreshTree.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLabelRefreshTree_LinkClicked);
            // 
            // cbxChatProfiles
            // 
            this.cbxChatProfiles.AllowDrop = true;
            this.cbxChatProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxChatProfiles.FormattingEnabled = true;
            this.cbxChatProfiles.Location = new System.Drawing.Point(121, 9);
            this.cbxChatProfiles.Name = "cbxChatProfiles";
            this.cbxChatProfiles.Size = new System.Drawing.Size(183, 28);
            this.cbxChatProfiles.Sorted = true;
            this.cbxChatProfiles.TabIndex = 7;
            this.cbxChatProfiles.TextChanged += new System.EventHandler(this.cbxChatProfiles_TextChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(995, 674);
            this.Controls.Add(this.cbxChatProfiles);
            this.Controls.Add(this.lnkLabelRefreshTree);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblProfile);
            this.Controls.Add(this.gbNeuralNodeConfiguration);
            this.Controls.Add(this.neuralTree);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Philips Chatbot Assistant Desktop Admin Portal";
            this.Load += new System.EventHandler(this.Main_Load);
            this.gbNeuralNodeConfiguration.ResumeLayout(false);
            this.gbNeuralNodeConfiguration.PerformLayout();
            this.gbQuickLinks.ResumeLayout(false);
            this.gbQuickLinks.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView neuralTree;
        private System.Windows.Forms.GroupBox gbNeuralNodeConfiguration;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblQuestionTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblExpressionType;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.TextBox tbQuestionTitle;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.ComboBox cbxExpressionTypes;
        private System.Windows.Forms.LinkLabel lnkNotes;
        private System.Windows.Forms.LinkLabel lnkNeuralExpression;
        private System.Windows.Forms.LinkLabel lnkTrainData;
        private System.Windows.Forms.GroupBox gbQuickLinks;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel lnkBotConfigurations;
        private System.Windows.Forms.LinkLabel lnkNeuralActions;
        private System.Windows.Forms.LinkLabel lnkNeuralResources;
        private System.Windows.Forms.LinkLabel lnkLabelRefreshTree;
        private System.Windows.Forms.ComboBox cbxChatProfiles;
        private System.Windows.Forms.LinkLabel lnkTrainModel;
        private System.Windows.Forms.LinkLabel lnkProgressResultAction;
        private System.Windows.Forms.LinkLabel lnkLabels;
    }
}

