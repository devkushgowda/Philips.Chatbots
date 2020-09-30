using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Data.Models.Neural;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MongoDB.Driver;
using Philips.Chatbots.Desktop.Portal.Data;

namespace Philips.Chatbots.Desktop.Portal
{
    public partial class LinkExpressionEditor : Form
    {
        private const string ActionNewBelow = "New below";
        private const string ActionMoveUp = "Move up";
        private const string ActionMoveDown = "Move down";
        private const string ActionDelete = "Delete";

        private List<ILinkInfo> linkCache;
        LinkExpression expression;
        public LinkExpressionEditor(LinkExpression expression)
        {
            this.expression = expression;
            InitializeComponent();
            LoadData();
            AddContextMenu();
        }

        private void LoadData()
        {
            tbQuestionTitle.Text = expression.QuestionTitle;
            tbSuggestions.Text = expression.Hint;
            chkBxSkipEval.Checked = expression.SkipEvaluation;

            DataGridViewTextBoxColumn dvTbTitle = new DataGridViewTextBoxColumn();
            dvTbTitle.Name = nameof(dvTbTitle);
            dvTbTitle.HeaderText = "Title";
            dvTbTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvTbTitle.FillWeight = 100;
            dvTbTitle.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridViewOptions.Columns.Add(dvTbTitle);



            DataGridViewTextBoxColumn dvTbValue = new DataGridViewTextBoxColumn();
            dvTbValue.Name = nameof(dvTbValue);
            dvTbValue.HeaderText = "Value";
            dvTbValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvTbValue.FillWeight = 50;
            dvTbValue.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewOptions.Columns.Add(dvTbValue);


            var bindingSource = new BindingSource();
            linkCache = DataProviders.GetAllNeuralNodesLinks(new LinkType[] { LinkType.NeuralResource });
            bindingSource.DataSource = linkCache;
            DataGridViewComboBoxColumn dvCbxLinkId = new DataGridViewComboBoxColumn();
            dvCbxLinkId.HeaderText = "Result node";
            dvCbxLinkId.Name = nameof(dvCbxLinkId);
            dvCbxLinkId.MaxDropDownItems = 4;
            dvCbxLinkId.DataSource = bindingSource;
            dvCbxLinkId.DisplayMember = nameof(ILinkInfo.Name);
            dvCbxLinkId.ValueMember = nameof(ILinkInfo._id);

            dvCbxLinkId.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvCbxLinkId.FillWeight = 150;
            dvCbxLinkId.SortMode = DataGridViewColumnSortMode.NotSortable;

            var aLinkIndex = dataGridViewOptions.Columns.Add(dvCbxLinkId);

            expression.Options?.ForEach(item => dataGridViewOptions.Rows.Add(new object[] { item.Item.Title, item.Item.Value, item.Link.LinkId }));

            LoadRowNumbers();
        }

        private void AddContextMenu()
        {
            var actionMenu = new ContextMenuStrip();

            //Add the menu items to the menu.
            actionMenu.Items.AddRange(new ToolStripMenuItem[] {
                new ToolStripMenuItem() { Text = ActionNewBelow },
                new ToolStripMenuItem() { Text = ActionMoveUp },
                new ToolStripMenuItem() { Text = ActionMoveDown },
                new ToolStripMenuItem() { Text = ActionDelete },
            });

            actionMenu.ItemClicked += actionMenu_Clicked;
            dataGridViewOptions.ContextMenuStrip = actionMenu;
        }

        private void LoadRowNumbers()
        {
            for (int i = 0; i < dataGridViewOptions.Rows.Count; i++)
            {
                dataGridViewOptions.Rows[i].HeaderCell.Value = $"{i + 1}";
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            expression.QuestionTitle = tbQuestionTitle.Text;
            expression.Hint = tbSuggestions.Text;
            expression.SkipEvaluation = chkBxSkipEval.Checked;
            expression.Options = new List<ActionOption>();
            foreach (DataGridViewRow row in dataGridViewOptions.Rows)
            {
                var title = row.Cells[0].Value as string;
                var value = row.Cells[1].Value as string;
                var id = row.Cells[2].Value as string;
                if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(value) && !string.IsNullOrWhiteSpace(id))
                {
                    var link = linkCache.FirstOrDefault(x => x._id == id);
                    if (link != null)
                    {
                        var linkType = link.GetLinkType();
                        expression.Options.Add(new ActionOption { Item = new ActionItem { Title = title, Value = value }, Link = new ActionLink { LinkId = link._id, Type = linkType } });
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private void dataGridViewOptions_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            LoadRowNumbers();
        }


        private void actionMenu_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var clickedMenu = e.ClickedItem.AccessibilityObject.Name;
            MenuAction(clickedMenu);
        }

        private void MenuAction(string clickedMenu)
        {
            bool reload = false;
            if (dataGridViewOptions.CurrentRow != null && dataGridViewOptions.SelectedRows.Count < 2 && dataGridViewOptions.CurrentRow.Index < dataGridViewOptions.Rows.Count - 1)
            {
                switch (clickedMenu)
                {
                    case ActionNewBelow:
                        {
                            var dr = dataGridViewOptions.Rows;
                            var row = (DataGridViewRow)dataGridViewOptions.Rows[0].Clone();
                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                row.Cells[i].Value = "";
                            }
                            dr.Insert(dataGridViewOptions.CurrentRow.Index + 1, row);
                            reload = true;
                        }
                        break;
                    case ActionMoveUp:
                        {
                            if (dataGridViewOptions.Rows.Count > 1 && dataGridViewOptions.CurrentRow.Index != 0 && dataGridViewOptions.CurrentRow.Index != dataGridViewOptions.Rows.Count - 1)
                            {
                                var index = dataGridViewOptions.CurrentRow.Index;

                                var dr = dataGridViewOptions.Rows;
                                var row = (DataGridViewRow)dataGridViewOptions.Rows[0].Clone();
                                for (int i = 0; i < row.Cells.Count; i++)
                                {
                                    row.Cells[i].Value = dataGridViewOptions.Rows[index].Cells[i].Value;
                                }
                                dr.RemoveAt(index);
                                dr.Insert(index - 1, row);
                                reload = true;
                            }
                        }
                        break;
                    case ActionMoveDown:
                        {
                            if (dataGridViewOptions.Rows.Count > 1 && dataGridViewOptions.CurrentRow.Index != dataGridViewOptions.Rows.Count - 2)
                            {
                                var index = dataGridViewOptions.CurrentRow.Index;

                                var dr = dataGridViewOptions.Rows;
                                var row = (DataGridViewRow)dataGridViewOptions.Rows[0].Clone();
                                for (int i = 0; i < row.Cells.Count; i++)
                                {
                                    row.Cells[i].Value = dataGridViewOptions.Rows[index].Cells[i].Value;
                                }
                                dr.RemoveAt(index);
                                dr.Insert(index + 1, row);
                                reload = true;
                            }
                        }
                        break;
                    case ActionDelete:
                        {

                            if (dataGridViewOptions.Rows.Count > 1)
                            {
                                var index = dataGridViewOptions.CurrentRow.Index;
                                var dr = dataGridViewOptions.Rows;
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

        private void dataGridViewOptions_KeyDown(object sender, KeyEventArgs e)
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

        private void lnkSuggestionsEditor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var suggestionsList = tbSuggestions.Text?.Split(',')?.Where(item => item.Contains(":"))?.Select(item =>
             {
                 var parts = item.Split(':');
                 return new KeyValuePair<string, string>(parts[0], parts[1]);
             }).ToList();
            var res = new KeyValueEditor("Suggestions formatter", suggestionsList).ShowDialog();
            if (res == DialogResult.OK)
            {
                suggestionsList.ForEach(item => tbSuggestions.Text += $"{item.Key}:{item.Value},");
            }
        }
    }
}
