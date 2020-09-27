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
    public partial class ListEditor : Form
    {
        private const string ActionNewBelow = "New below";
        private const string ActionMoveUp = "Move up";
        private const string ActionMoveDown = "Move down";
        private const string ActionDelete = "Delete";

        public List<String> Result { get; set; } = new List<string>();
        public ListEditor(string title, List<String> list)
        {
            list = list ?? new List<string>();
            InitializeComponent();
            LoadDataGridView(list);
            AddContextMenu();
            lblTitle.Text = title;

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
            dataGridView.ContextMenuStrip = actionMenu;
        }

        private void LoadDataGridView(List<string> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Note");
            dataGridView.DataSource = dt;
            list.ForEach(item => dt.Rows.Add(new object[] { item }));
            LoadRowNumbers();

            dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView.Columns[0].FillWeight = 100;
            dataGridView.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void LoadRowNumbers()
        {
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                dataGridView.Rows[i].HeaderCell.Value = $"{i + 1}";
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            foreach (DataRow row in ((DataTable)dataGridView.DataSource).Rows)
            {
                string data = null;
                var dataObj = row?.ItemArray[0];
                if (dataObj is string)
                    data = (string)dataObj;
                if (!string.IsNullOrWhiteSpace(data))
                    Result.Add(data);
            }
            this.Close();
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            LoadRowNumbers();
        }

        private void dataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
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
            var dt = ((DataTable)dataGridView.DataSource);
            bool reload = false;
            if (dataGridView.CurrentRow != null)
            {
                switch (clickedMenu)
                {
                    case ActionNewBelow:
                        {
                            if (dataGridView.SelectedRows.Count <= 1)
                            {
                                DataRow dRow = dt.NewRow();
                                dRow[0] = "";
                                dt.Rows.InsertAt(dRow, dataGridView.CurrentRow.Index + 1);
                                reload = true;
                            }
                        }
                        break;
                    case ActionMoveUp:
                        {
                            if (dataGridView.Rows.Count > 1 && dataGridView.CurrentRow.Index != 0 && dataGridView.SelectedRows.Count <= 1 && dataGridView.CurrentRow.Index != dataGridView.Rows.Count - 1)
                            {
                                var index = dataGridView.CurrentRow.Index;
                                DataRow dRow = dt.NewRow();
                                dRow[0] = dt.Rows[index][0];
                                dt.Rows.RemoveAt(index);
                                dt.Rows.InsertAt(dRow, index - 1);
                                reload = true;
                            }
                        }
                        break;
                    case ActionMoveDown:
                        {
                            if (dataGridView.Rows.Count > 1 && dataGridView.CurrentRow.Index != dataGridView.Rows.Count - 2 && dataGridView.SelectedRows.Count <= 1)
                            {
                                var index = dataGridView.CurrentRow.Index;
                                DataRow dRow = dt.NewRow();
                                dRow[0] = dt.Rows[index][0];
                                dt.Rows.RemoveAt(index);
                                dt.Rows.InsertAt(dRow, index + 1);
                                reload = true;
                            }
                        }
                        break;
                    case ActionDelete:
                        {
                            if (dataGridView.Rows.Count > 1)
                            {
                                if (dataGridView.SelectedRows.Count > 0)
                                {
                                    foreach (DataGridViewRow row in dataGridView.SelectedRows)
                                    {
                                        var castedRow = (row.DataBoundItem as DataRowView).Row;
                                        dt.Rows.Remove(castedRow);
                                    }
                                }
                                else if (dataGridView.CurrentRow.Index < dataGridView.Rows.Count - 1)
                                {
                                    var index = dataGridView.CurrentRow.Index;
                                    dt.Rows.RemoveAt(index);
                                }
                            }
                            reload = true;
                        }
                        break;
                    default:
                        break;
                }
                if (reload)
                    LoadRowNumbers();
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
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
    }
}
