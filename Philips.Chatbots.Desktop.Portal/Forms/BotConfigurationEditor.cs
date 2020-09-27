using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Desktop.Portal.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Philips.Chatbots.Desktop.Portal
{
    public partial class BotConfigurationEditor : Form
    {
        private const string ActionDelete = "Delete";

        private readonly BotModel botModel;
        public BotConfigurationEditor(BotModel botModel)
        {
            InitializeComponent();
            this.botModel = botModel;
            LoadData();
            AddContextMenu();
        }

        private void AddContextMenu()
        {
            var actionMenu = new ContextMenuStrip();

            //Add the menu items to the menu.
            actionMenu.Items.AddRange(new ToolStripMenuItem[] {
                new ToolStripMenuItem() { Text = ActionDelete }
            });

            actionMenu.ItemClicked += actionMenu_Clicked;
            dataGridViewChatProfiles.ContextMenuStrip = actionMenu;
        }

        private void actionMenu_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var clickedMenu = e.ClickedItem.AccessibilityObject.Name;
            switch (clickedMenu)
            {
                case ActionDelete:
                    {
                        if (dataGridViewChatProfiles.CurrentRow != null && dataGridViewChatProfiles.Rows.Count > 1)
                        {
                            var index = dataGridViewChatProfiles.CurrentRow.Index;
                            var dr = dataGridViewChatProfiles.Rows;
                            dr.RemoveAt(index);
                            LoadRowNumbers();
                        }
                    }
                    break;
                default:
                    break;
            }

        }

        private void LoadData()
        {
            tbBotId.Text = botModel._id;
            tbEndpoint.Text = botModel.EndPoint;
            tbDescription.Text = botModel.Description;
            tbDataFolder.Text = botModel.Configuration?.DataFolder;

            UpdateResourceStringsLinkLabel();

            BindingSource profilesBindingSource = new BindingSource();
            profilesBindingSource.DataSource = botModel.Configuration?.ChatProfiles?.Select(x => x);
            cbxActiveProfile.DataSource = profilesBindingSource;
            cbxActiveProfile.DisplayMember = nameof(BotChatProfile.Name);
            cbxActiveProfile.ValueMember = nameof(BotChatProfile.Name);
            cbxActiveProfile.SelectedValue = botModel.Configuration?.ActiveProfile;

            LoadDataView();
        }

        private void UpdateResourceStringsLinkLabel()
        {
            var resCount = (int)botModel.Configuration?.ResourceStrings?.Count;
            lnkResourceStrings.Text = $"Resource strings ({resCount})";
        }

        private void LoadDataView()
        {
            DataGridViewTextBoxColumn dvName = new DataGridViewTextBoxColumn();
            dvName.Name = nameof(dvName);
            dvName.HeaderText = nameof(BotChatProfile.Name);
            dvName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvName.FillWeight = 50;
            dvName.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridViewChatProfiles.Columns.Add(dvName);

            DataGridViewTextBoxColumn dvDescription = new DataGridViewTextBoxColumn();
            dvDescription.Name = nameof(dvDescription);
            dvDescription.HeaderText = nameof(BotChatProfile.Description);
            dvDescription.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvDescription.FillWeight = 75;
            dvDescription.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridViewChatProfiles.Columns.Add(dvDescription);


            DataGridViewTextBoxColumn dvRoot = new DataGridViewTextBoxColumn();
            dvRoot.Name = nameof(dvRoot);
            dvRoot.HeaderText = nameof(BotChatProfile.Root);
            dvRoot.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvRoot.FillWeight = 100;
            dvRoot.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridViewChatProfiles.Columns.Add(dvRoot);

            botModel.Configuration?.ChatProfiles.ForEach(
                profile =>
                dataGridViewChatProfiles.Rows
                .Add(new object[] { profile.Name, profile.Description, profile.Root })
                );

            LoadRowNumbers();
        }

        private void LoadRowNumbers()
        {
            for (int i = 0; i < dataGridViewChatProfiles.Rows.Count; i++)
            {
                dataGridViewChatProfiles.Rows[i].HeaderCell.Value = $"{i + 1}";
            }
        }

        private void lnkResourceStrings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (botModel.Configuration?.ResourceStrings == null)
                botModel.Configuration.ResourceStrings = new List<KeyValuePair<string, string>>();
            var editor = new KeyValueEditor("Bot resource strings editor", botModel.Configuration.ResourceStrings);
            editor.ShowDialog();
            UpdateResourceStringsLinkLabel();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            botModel.EndPoint = tbEndpoint.Text;
            botModel.Description = tbDescription.Text;
            if (botModel.Configuration == null)
                botModel.Configuration = new BotConfiguration();
            botModel.Configuration.DataFolder = tbDataFolder.Text;
            botModel.Configuration.ActiveProfile = cbxActiveProfile.Text;

            if (botModel.Configuration.ChatProfiles == null)
                botModel.Configuration.ChatProfiles = new List<BotChatProfile>();
            else
                botModel.Configuration.ChatProfiles.Clear();

            foreach (DataGridViewRow row in dataGridViewChatProfiles.Rows)
            {
                var name = row.Cells[0].Value as string;
                var desc = row.Cells[1].Value as string;
                var root = row.Cells[2].Value as string;
                if (!string.IsNullOrWhiteSpace(name))
                {
                    botModel.Configuration.ChatProfiles.Add(new BotChatProfile { Name = name, Description = desc, Root = root });
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void dataGridViewChatProfiles_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            LoadRowNumbers();
        }
    }
}
