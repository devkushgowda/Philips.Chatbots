using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Database.Extension;
using Philips.Chatbots.Desktop.Portal.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Philips.Chatbots.Database.Common.DbAlias;

namespace Philips.Chatbots.Desktop.Portal
{
    public partial class NeuralResourcesEditor : Form
    {

        private const string MenuActionNew = "New";
        private const string MenuActionDelete = "Delete";

        List<NeuralResourceModel> cacheResources = new List<NeuralResourceModel>();
        public NeuralResourcesEditor()
        {
            InitializeComponent();
            LoadDataSource();
            AddContextMenu();
        }

        private void AddContextMenu()
        {
            // Create the ContextMenuStrip.
            var actionMenu = new ContextMenuStrip();

            //Add the menu items to the menu.
            actionMenu.Items.AddRange(new ToolStripMenuItem[] { new ToolStripMenuItem() { Text = MenuActionNew },
                new ToolStripMenuItem() { Text = MenuActionDelete }
            });

            actionMenu.ItemClicked += actionMenu_Clicked;

            treeViewNeuralresources.ContextMenuStrip = actionMenu;
        }

        private async void actionMenu_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var clicked = e.ClickedItem.AccessibilityObject.Name;
            {
                switch (clicked)
                {
                    case MenuActionNew:
                        {
                            var resourceModel = await DbResourceCollection.InsertNew(new NeuralResourceModel() { Name = "New resource" });
                            treeViewNeuralresources.SelectedNode = AddNewNode(resourceModel);

                        }
                        break;
                    case MenuActionDelete:
                        {
                            if (treeViewNeuralresources.SelectedNode != null)
                            {
                                await DbResourceCollection.RemoveOneById(treeViewNeuralresources.SelectedNode.Name);
                                treeViewNeuralresources.SelectedNode.Remove();
                                ReFillData();
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void LoadDataSource()
        {
            var resourceTypeBindingSource = new BindingSource();
            resourceTypeBindingSource.DataSource = DataProviders.GetEnumDictinorary(typeof(ResourceType));
            cbResourceType.DataSource = resourceTypeBindingSource;
            cbResourceType.DisplayMember = "Key";
            cbResourceType.ValueMember = "Value";

        }

        private TreeNode AddNewNode(NeuralResourceModel node)
        {
            var name = $"{node.Name} ({(string.IsNullOrWhiteSpace(node.Location) ? 0 : 1)}:{(node.IsLocal ? 'L' : 'R')})";
            var treeNode = treeViewNeuralresources.Nodes.Add(node._id, name);
            treeNode.Tag = node;
            treeNode.ImageKey = node.Type.GetEnumValueName();
            return treeNode;
        }

        private async Task LoadData(string selectId)
        {
            ResetForm();
            treeViewNeuralresources.Nodes.Clear();
            treeViewNeuralresources.ImageList = DataProviders.LoadNeuralResourceTypesImageList();

            (await LinkType.NeuralResource.GetAllLinks()).ForEach(resource =>
            {
                var treeNode = AddNewNode((NeuralResourceModel)resource);
                if (!string.IsNullOrWhiteSpace(selectId) && selectId == resource._id)
                {
                    treeViewNeuralresources.SelectedNode = treeNode;
                }

            });
        }

        private async void NeuralResourcesEditor_Load(object sender, EventArgs e)
        {
            await LoadData(treeViewNeuralresources.SelectedNode?.Name);
        }

        private async void lnkLabels_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (treeViewNeuralresources.SelectedNode != null)
            {
                var node = (NeuralResourceModel)treeViewNeuralresources.SelectedNode.Tag;
                var editor = new ListEditor("Neural resource label editor", node.Labels);
                var res = editor.ShowDialog();
                if (res == DialogResult.OK)
                {
                    node.Labels = editor.Result;
                    await DbResourceCollection.ReplaceOneById(node._id, node);
                    treeViewNeuralresources.SelectedNode.Tag = node;
                    ReFillData();
                }
            }
        }

        private void ReFillData()
        {
            if (treeViewNeuralresources.SelectedNode != null)
            {
                gbNeuralResourceConfiguration.Enabled = true;
                var node = (NeuralResourceModel)treeViewNeuralresources.SelectedNode.Tag;
                tbId.Text = node._id;
                tbName.Text = node.Name;
                tbDescription.Text = node.Description;
                tbQuestionTitle.Text = node.QuestionTitle;
                tbTitle.Text = node.Title;
                tbLocation.Text = node.Location;
                chkBxIsLocal.Checked = node.IsLocal;
                cbResourceType.SelectedValue = node.Type;
                lnkLabels.Text = $"Edit labels ({(node.Labels == null ? 0 : node.Labels.Count)})";
            }
            else
            {
                ResetForm();
            }
        }

        private void ResetForm()
        {
            gbNeuralResourceConfiguration.Enabled = false;
            tbId.Text = "";
            tbName.Text = "";
            tbDescription.Text = "";
            tbQuestionTitle.Text = "";
            tbTitle.Text = "";
            tbLocation.Text = "";
            chkBxIsLocal.Checked = false;
            lnkLabels.Enabled = true;
            lnkLabels.Text = $"Edit labels";
        }

        private void treeViewNeuralresources_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ReFillData();
            e.Node.SelectedImageKey = e.Node.ImageKey;
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            if (treeViewNeuralresources.SelectedNode != null)
            {
                var node = (NeuralResourceModel)treeViewNeuralresources.SelectedNode.Tag;
                node.Name = tbName.Text;
                node.Description = tbDescription.Text;
                node.QuestionTitle = tbQuestionTitle.Text;
                node.Title = tbTitle.Text;
                node.Location = tbLocation.Text;
                node.IsLocal = chkBxIsLocal.Checked;
                node.Type = (ResourceType)cbResourceType.SelectedValue;

                await DbResourceCollection.ReplaceOneById(node._id, node);

                await LoadData(treeViewNeuralresources.SelectedNode.Name);
            }
        }
    }
}
