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
    public partial class NeuralActionsEditor : Form
    {
        private const string ActionNewBelow = "New below";
        private const string ActionMoveUp = "Move up";
        private const string ActionMoveDown = "Move down";
        private const string ActionDelete = "Delete";

        private const string MenuActionNew = "New";
        private const string MenuActionDelete = "Delete";

        public NeuralActionsEditor()
        {
            InitializeComponent();
            AddDataViewContextMenu();
            AddTreeViewContextMenu();
            LoadDataSource();
        }

        private async Task LoadDataView()
        {
            var bindingSource = new BindingSource();
            bindingSource.DataSource = await LinkType.NeuralResource.GetAllLinks();
            DataGridViewComboBoxColumn dvCbxResources = new DataGridViewComboBoxColumn();
            dvCbxResources.HeaderText = "Resource";
            dvCbxResources.Name = nameof(dvCbxResources);
            dvCbxResources.MaxDropDownItems = 4;
            dvCbxResources.DataSource = bindingSource;
            dvCbxResources.DisplayMember = nameof(ILinkInfo.Name);
            dvCbxResources.ValueMember = nameof(ILinkInfo._id);

            dvCbxResources.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvCbxResources.FillWeight = 150;
            dvCbxResources.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridViewResources.Columns.Add(dvCbxResources);

        }
        private void AddTreeViewContextMenu()
        {
            // Create the ContextMenuStrip.
            var actionMenu = new ContextMenuStrip();

            //Add the menu items to the menu.
            actionMenu.Items.AddRange(new ToolStripMenuItem[] { new ToolStripMenuItem() { Text = MenuActionNew },
                new ToolStripMenuItem() { Text = MenuActionDelete }
            });

            actionMenu.ItemClicked += actionTreeViewMenu_Clicked;

            treeViewNeuralActions.ContextMenuStrip = actionMenu;
        }

        private void dataViewActionMenu_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var clickedMenu = e.ClickedItem.AccessibilityObject.Name;
            MenuAction(clickedMenu);
        }

        private async void actionTreeViewMenu_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var clicked = e.ClickedItem.AccessibilityObject.Name;
            {
                switch (clicked)
                {
                    case MenuActionNew:
                        {
                            var actionModel = await DbActionCollection.InsertNew(new NeuralActionModel() { Name = "New action" });
                            treeViewNeuralActions.SelectedNode = AddNewNode(actionModel);

                        }
                        break;
                    case MenuActionDelete:
                        {
                            if (treeViewNeuralActions.SelectedNode != null)
                            {
                                await DbActionCollection.RemoveOneById(treeViewNeuralActions.SelectedNode.Name);
                                treeViewNeuralActions.SelectedNode.Remove();
                                ReFillData();
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void AddDataViewContextMenu()
        {
            var actionMenu = new ContextMenuStrip();

            //Add the menu items to the menu.
            actionMenu.Items.AddRange(new ToolStripMenuItem[] {
                new ToolStripMenuItem() { Text = ActionNewBelow },
                new ToolStripMenuItem() { Text = ActionMoveUp },
                new ToolStripMenuItem() { Text = ActionMoveDown },
                new ToolStripMenuItem() { Text = ActionDelete },
            });

            actionMenu.ItemClicked += dataViewActionMenu_Clicked;
            dataGridViewResources.ContextMenuStrip = actionMenu;
        }

        private void LoadRowNumbers()
        {
            for (int i = 0; i < dataGridViewResources.Rows.Count; i++)
            {
                dataGridViewResources.Rows[i].HeaderCell.Value = $"{i + 1}";
            }
        }

        private void MenuAction(string clickedMenu)
        {
            bool reload = false;
            if (dataGridViewResources.CurrentRow != null && dataGridViewResources.SelectedRows.Count < 2 && dataGridViewResources.CurrentRow.Index < dataGridViewResources.Rows.Count - 1)
            {
                switch (clickedMenu)
                {
                    case ActionNewBelow:
                        {
                            var dr = dataGridViewResources.Rows;
                            var row = (DataGridViewRow)dataGridViewResources.Rows[0].Clone();
                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                row.Cells[i].Value = "";
                            }
                            dr.Insert(dataGridViewResources.CurrentRow.Index + 1, row);
                            reload = true;
                        }
                        break;
                    case ActionMoveUp:
                        {
                            if (dataGridViewResources.Rows.Count > 1 && dataGridViewResources.CurrentRow.Index != 0 && dataGridViewResources.CurrentRow.Index != dataGridViewResources.Rows.Count - 1)
                            {
                                var index = dataGridViewResources.CurrentRow.Index;

                                var dr = dataGridViewResources.Rows;
                                var row = (DataGridViewRow)dataGridViewResources.Rows[0].Clone();
                                for (int i = 0; i < row.Cells.Count; i++)
                                {
                                    row.Cells[i].Value = dataGridViewResources.Rows[index].Cells[i].Value;
                                }
                                dr.RemoveAt(index);
                                dr.Insert(index - 1, row);
                                reload = true;
                            }
                        }
                        break;
                    case ActionMoveDown:
                        {
                            if (dataGridViewResources.Rows.Count > 1 && dataGridViewResources.CurrentRow.Index != dataGridViewResources.Rows.Count - 2)
                            {
                                var index = dataGridViewResources.CurrentRow.Index;

                                var dr = dataGridViewResources.Rows;
                                var row = (DataGridViewRow)dataGridViewResources.Rows[0].Clone();
                                for (int i = 0; i < row.Cells.Count; i++)
                                {
                                    row.Cells[i].Value = dataGridViewResources.Rows[index].Cells[i].Value;
                                }
                                dr.RemoveAt(index);
                                dr.Insert(index + 1, row);
                                reload = true;
                            }
                        }
                        break;
                    case ActionDelete:
                        {

                            if (dataGridViewResources.Rows.Count > 1)
                            {
                                var index = dataGridViewResources.CurrentRow.Index;
                                var dr = dataGridViewResources.Rows;
                                dr.RemoveAt(index);
                                reload = true;
                            }
                        }
                        break;
                    default:
                        break;
                }
                if (reload)
                    LoadRowNumbers();
            }
        }

        private void dataGridViewResources_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:
                        MenuAction(ActionDelete);
                        break;
                    case Keys.N:
                        MenuAction(ActionNewBelow);
                        break;
                    case Keys.Up:
                        MenuAction(ActionMoveUp);
                        break;
                    case Keys.Down:
                        MenuAction(ActionMoveDown);
                        break;
                    default:
                        break;
                }
            }
        }

        private void LoadDataSource()
        {
            var resourceTypeBindingSource = new BindingSource();
            resourceTypeBindingSource.DataSource = DataProviders.GetEnumDictinorary(typeof(ActionType));
            cbActionType.DataSource = resourceTypeBindingSource;
            cbActionType.DisplayMember = "Key";
            cbActionType.ValueMember = "Value";

        }

        private TreeNode AddNewNode(NeuralActionModel node)
        {
            var name = $"{node.Name} ({(int)(node.Resources?.Count)})";
            var treeNode = treeViewNeuralActions.Nodes.Add(node._id, name);
            treeNode.Tag = node;
            treeNode.ImageKey = node.Type.GetEnumValueName();
            return treeNode;
        }

        private async Task LoadData(string selectId)
        {
            ResetForm();
            treeViewNeuralActions.Nodes.Clear();
            treeViewNeuralActions.ImageList = DataProviders.LoadNeuralActionTypesImageList();

            (await LinkType.ActionLink.GetAllLinks()).ForEach(resource =>
            {
                var treeNode = AddNewNode((NeuralActionModel)resource);
                if (!string.IsNullOrWhiteSpace(selectId) && selectId == resource._id)
                {
                    treeViewNeuralActions.SelectedNode = treeNode;
                }

            });
        }

        private async void lnkLabels_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (treeViewNeuralActions.SelectedNode != null)
            {
                var node = (NeuralActionModel)treeViewNeuralActions.SelectedNode.Tag;
                var editor = new ListEditor("Neural action label editor", node.Labels);
                var res = editor.ShowDialog();
                if (res == DialogResult.OK)
                {
                    node.Labels = editor.Result;
                    await DbActionCollection.ReplaceOneById(node._id, node);
                    treeViewNeuralActions.SelectedNode.Tag = node;
                    ReFillData();
                }
            }
        }

        private void ReFillData()
        {
            if (treeViewNeuralActions.SelectedNode != null)
            {
                gbNeuralActionConfiguration.Enabled = true; ;
                var node = (NeuralActionModel)treeViewNeuralActions.SelectedNode.Tag;
                tbId.Text = node._id;
                tbName.Text = node.Name;
                tbDescription.Text = node.Description;
                tbQuestionTitle.Text = node.QuestionTitle;
                tbTitle.Text = node.Title;
                cbActionType.SelectedValue = node.Type;
                lnkLabels.Text = $"Edit labels ({(node.Labels == null ? 0 : node.Labels.Count)})";

                dataGridViewResources.Rows.Clear();
                node.Resources?.ForEach(item => dataGridViewResources.Rows.Add(new object[] { item }));
            }
            else
            {
                ResetForm();
            }
        }

        private void ResetForm()
        {
            gbNeuralActionConfiguration.Enabled = false;
            tbId.Text = "";
            tbName.Text = "";
            tbDescription.Text = "";
            tbQuestionTitle.Text = "";
            tbTitle.Text = "";
            lnkLabels.Text = $"Edit labels";
            dataGridViewResources.Rows.Clear();
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            if (treeViewNeuralActions.SelectedNode != null)
            {
                var node = (NeuralActionModel)treeViewNeuralActions.SelectedNode.Tag;
                node.Name = tbName.Text;
                node.Description = tbDescription.Text;
                node.QuestionTitle = tbQuestionTitle.Text;
                node.Title = tbTitle.Text;
                node.Type = (ActionType)cbActionType.SelectedValue;

                if (node.Resources == null)
                {
                    node.Resources = new List<string>();
                }
                else
                {
                    node.Resources.Clear();
                }

                foreach (DataGridViewRow row in dataGridViewResources.Rows)
                {
                    var resId = row.Cells[0].Value as string;
                    if (!string.IsNullOrWhiteSpace(resId) && !node.Resources.Contains(resId))
                        node.Resources.Add(resId);
                }

                await DbActionCollection.ReplaceOneById(node._id, node);

                await LoadData(treeViewNeuralActions.SelectedNode.Name);
            }
        }

        private void treeViewNeuralActions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ReFillData();
            e.Node.SelectedImageKey = e.Node.ImageKey;
        }

        private async void NeuralActionsEditor_Load(object sender, EventArgs e)
        {
            await LoadDataView();
            await LoadData(treeViewNeuralActions.SelectedNode?.Name);
        }

        private void dataGridViewResources_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            LoadRowNumbers();
        }
    }
}
