using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Philips.Chatbots.Desktop.Portal
{
    public partial class KeyValueEditor : Form
    {
        private const string ActionNewBelow = "New below";
        private const string ActionMoveUp = "Move up";
        private const string ActionMoveDown = "Move down";
        private const string ActionDelete = "Delete";

        private readonly List<KeyValuePair<string, string>> keyValueList;
        public KeyValueEditor(string title, List<KeyValuePair<string, string>> keyValueList)
        {
            this.keyValueList = keyValueList;
            InitializeComponent();
            gbTitle.Text = title;
            LoadData();
            AddContextMenu();
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
            dataGridViewKeyValues.ContextMenuStrip = actionMenu;
        }

        private void LoadData()
        {
            DataGridViewTextBoxColumn dvKey = new DataGridViewTextBoxColumn();
            dvKey.Name = nameof(dvKey);
            dvKey.HeaderText = "Key";
            dvKey.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvKey.FillWeight = 30;
            dvKey.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridViewKeyValues.Columns.Add(dvKey);

            DataGridViewTextBoxColumn dvValue = new DataGridViewTextBoxColumn();
            dvValue.Name = nameof(dvValue);
            dvValue.HeaderText = "Value";
            dvValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvValue.FillWeight = 70;
            dvValue.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridViewKeyValues.Columns.Add(dvValue);

            keyValueList.ForEach(item => dataGridViewKeyValues.Rows.Add(new object[] { item.Key, item.Value }));

            LoadRowNumbers();
        }

        private void LoadRowNumbers()
        {
            for (int i = 0; i < dataGridViewKeyValues.Rows.Count; i++)
            {
                dataGridViewKeyValues.Rows[i].HeaderCell.Value = $"{i + 1}";
            }
        }

        private void dataGridViewKeyValues_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            LoadRowNumbers();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            keyValueList.Clear();
            foreach (DataGridViewRow row in dataGridViewKeyValues.Rows)
            {
                var key = row.Cells[0].Value as string;
                var value = row.Cells[1].Value as string;
                if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
                {
                    keyValueList.Add(new KeyValuePair<string, string>(key, value));
                }
            }
            DialogResult = DialogResult.OK;
        }

        private void actionMenu_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var clickedMenu = e.ClickedItem.AccessibilityObject.Name;
            MenuAction(clickedMenu);
        }

        private void MenuAction(string clickedMenu)
        {
            bool reload = false;
            if (dataGridViewKeyValues.CurrentRow != null && dataGridViewKeyValues.SelectedRows.Count < 2 && dataGridViewKeyValues.CurrentRow.Index < dataGridViewKeyValues.Rows.Count - 1)
            {
                switch (clickedMenu)
                {
                    case ActionNewBelow:
                        {
                            var dr = dataGridViewKeyValues.Rows;
                            var row = (DataGridViewRow)dataGridViewKeyValues.Rows[0].Clone();
                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                row.Cells[i].Value = "";
                            }
                            dr.Insert(dataGridViewKeyValues.CurrentRow.Index + 1, row);
                            reload = true;
                        }
                        break;
                    case ActionMoveUp:
                        {
                            if (dataGridViewKeyValues.Rows.Count > 1 && dataGridViewKeyValues.CurrentRow.Index != 0 && dataGridViewKeyValues.CurrentRow.Index != dataGridViewKeyValues.Rows.Count - 1)
                            {
                                var index = dataGridViewKeyValues.CurrentRow.Index;

                                var dr = dataGridViewKeyValues.Rows;
                                var row = (DataGridViewRow)dataGridViewKeyValues.Rows[0].Clone();
                                for (int i = 0; i < row.Cells.Count; i++)
                                {
                                    row.Cells[i].Value = dataGridViewKeyValues.Rows[index].Cells[i].Value;
                                }
                                dr.RemoveAt(index);
                                dr.Insert(index - 1, row);
                                reload = true;
                            }
                        }
                        break;
                    case ActionMoveDown:
                        {
                            if (dataGridViewKeyValues.Rows.Count > 1 && dataGridViewKeyValues.CurrentRow.Index != dataGridViewKeyValues.Rows.Count - 2)
                            {
                                var index = dataGridViewKeyValues.CurrentRow.Index;

                                var dr = dataGridViewKeyValues.Rows;
                                var row = (DataGridViewRow)dataGridViewKeyValues.Rows[0].Clone();
                                for (int i = 0; i < row.Cells.Count; i++)
                                {
                                    row.Cells[i].Value = dataGridViewKeyValues.Rows[index].Cells[i].Value;
                                }
                                dr.RemoveAt(index);
                                dr.Insert(index + 1, row);
                                reload = true;
                            }
                        }
                        break;
                    case ActionDelete:
                        {

                            if (dataGridViewKeyValues.Rows.Count > 1)
                            {
                                if (dataGridViewKeyValues.SelectedRows.Count > 0)
                                {
                                    foreach (DataGridViewRow row in dataGridViewKeyValues.SelectedRows)
                                    {
                                        dataGridViewKeyValues.Rows.Remove(row);
                                    }
                                }
                                else if (dataGridViewKeyValues.CurrentRow.Index < dataGridViewKeyValues.Rows.Count - 1)
                                {
                                    dataGridViewKeyValues.Rows.Remove(dataGridViewKeyValues.CurrentRow);
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

        private void dataGridViewKeyValues_KeyDown(object sender, KeyEventArgs e)
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
