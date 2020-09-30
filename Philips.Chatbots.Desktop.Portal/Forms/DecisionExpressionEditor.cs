using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Desktop.Portal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Philips.Chatbots.Desktop.Portal
{
    public partial class DecisionExpressionEditor : Form
    {

        private const string ActionNewBelow = "New below";
        private const string ActionMoveUp = "Move up";
        private const string ActionMoveDown = "Move down";
        private const string ActionDelete = "Delete";

        private Dictionary<string, object> opDictionary;
        private Dictionary<string, object> withDictionary;
        private Dictionary<string, int> linkTypeDictionary;

        private Dictionary<string, Type> DataTypeMap = new Dictionary<string, Type> {

            { "BOOL",typeof(bool) },
            { "DATE",typeof(DateTime) },
            { "DOUBLE",typeof(double) },
            { "FLOAT",typeof(float) },
            { "STRING",typeof(string) },
            { "INT",typeof(int) },
            { "LONG",typeof(long) },
        };
        private DecisionExpression expression;


        public DecisionExpressionEditor(DecisionExpression expression)
        {
            this.expression = expression;
            InitializeComponent();

            LoadData();

            AddContextMenu();
        }

        private void BindActionLinks()
        {

            var bindingFallbackActionSource = new BindingSource();
            bindingFallbackActionSource.DataSource = new List<ILinkInfo>();
            cbxFallbackActionNode.DataSource = bindingFallbackActionSource;
            cbxFallbackActionNode.DisplayMember = nameof(ILinkInfo.Name);
            cbxFallbackActionNode.ValueMember = nameof(ILinkInfo._id);

            var bindingForwardActionSource = new BindingSource();
            bindingForwardActionSource.DataSource = new List<ILinkInfo>();
            cbxForwardActionNode.DataSource = bindingForwardActionSource;
            cbxForwardActionNode.DisplayMember = nameof(ILinkInfo.Name);
            cbxForwardActionNode.ValueMember = nameof(ILinkInfo._id);

            var bindingFallbackActionTypeSource = new BindingSource();
            bindingFallbackActionTypeSource.DataSource = linkTypeDictionary;

            cbxFallbackActionType.DataSource = bindingFallbackActionTypeSource;
            cbxFallbackActionType.DisplayMember = "Key";
            cbxFallbackActionType.ValueMember = "Value";

            var bindingForwardActionTypeSource = new BindingSource();
            bindingForwardActionTypeSource.DataSource = linkTypeDictionary;

            cbxForwardActionType.DataSource = bindingForwardActionTypeSource;
            cbxForwardActionType.DisplayMember = "Key";
            cbxForwardActionType.ValueMember = "Value";

            var bindingSourceDataType = new BindingSource();
            bindingSourceDataType.DataSource = DataTypeMap;

            cbxDataType.DataSource = bindingSourceDataType;
            cbxDataType.DisplayMember = "Key";
            cbxDataType.ValueMember = "Value";


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
            dataGridViewActionItems.ContextMenuStrip = actionMenu;
        }

        private void LoadData()
        {
            LoadAllEnumDictionary();

            var bindingSourceOp = new BindingSource();
            bindingSourceOp.DataSource = opDictionary;

            DataGridViewComboBoxColumn dvCbxOp = new DataGridViewComboBoxColumn();
            dvCbxOp.HeaderText = "Operation";
            dvCbxOp.Name = nameof(dvCbxOp);
            dvCbxOp.MaxDropDownItems = 10;
            dvCbxOp.DataSource = bindingSourceOp;
            dvCbxOp.DisplayMember = nameof(KeyValuePair<string, object>.Key);
            dvCbxOp.ValueMember = nameof(KeyValuePair<string, object>.Value);

            dvCbxOp.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvCbxOp.FillWeight = 50;
            dvCbxOp.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewActionItems.Columns.Add(dvCbxOp);

            DataGridViewTextBoxColumn dvRval = new DataGridViewTextBoxColumn();
            dvRval.Name = nameof(dvRval);
            dvRval.HeaderText = "RVal";
            dvRval.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvRval.FillWeight = 100;
            dvRval.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridViewActionItems.Columns.Add(dvRval);


            var bindingSourceWith = new BindingSource();
            bindingSourceWith.DataSource = withDictionary;

            DataGridViewComboBoxColumn dvCbxWith = new DataGridViewComboBoxColumn();
            dvCbxWith.HeaderText = "With";
            dvCbxWith.Name = nameof(dvCbxWith);
            dvCbxWith.MaxDropDownItems = 10;
            dvCbxWith.DataSource = bindingSourceWith;
            dvCbxWith.DisplayMember = nameof(KeyValuePair<string, object>.Key);
            dvCbxWith.ValueMember = nameof(KeyValuePair<string, object>.Value);

            dvCbxWith.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dvCbxWith.FillWeight = 50;
            dvCbxWith.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewActionItems.Columns.Add(dvCbxWith);

            expression.ExpressionTree?.Nodes?.ForEach(node =>
            {
                object op = null, rVal = null, with = null;

                op = (node is ArithmeticOp) ? (object)(node as ArithmeticOp).AOp : (object)(node as RelationalOp).ROp;
                rVal = node.RVal.ToString();
                with = node.With;
                dataGridViewActionItems.Rows.Add(new object[] { op, rVal.ToString(), with });
            });

            BindActionLinks();

            var datatype = expression.ExpressionTree?.Nodes?.FirstOrDefault()?.RVal?.GetType();
            if (datatype != null)
                cbxDataType.SelectedValue = datatype;

            tbQuestionTitle.Text = expression.QuestionTitle;
            tbSuggestions.Text = expression.Hint;
            chkBxSkipEval.Checked = expression.SkipEvaluation;

            if (expression?.ForwardAction != null)
            {
                cbxForwardActionType.SelectedValue = (int)expression.ForwardAction.Type;
                cbxForwardActionNode.SelectedValue = expression.ForwardAction.LinkId;
            }
            else
            {
                cbxForwardActionNode.SelectedValue = -1;

            }

            if (expression?.FallbackAction != null)
            {
                cbxFallbackActionType.SelectedValue = (int)expression.FallbackAction.Type;
                cbxFallbackActionNode.SelectedValue = expression.FallbackAction.LinkId;
            }
            else
            {
                cbxFallbackActionType.SelectedValue = -1;
            }
            LoadRowNumbers();

        }

        private void LoadRowNumbers()
        {
            for (int i = 0; i < dataGridViewActionItems.Rows.Count; i++)
            {
                dataGridViewActionItems.Rows[i].HeaderCell.Value = $"{i + 1}";
            }
        }

        private void LoadAllEnumDictionary()
        {
            opDictionary = DataProviders.GetEnumDictinorary(typeof(ArithmeticOpType));
            foreach (var val in DataProviders.GetEnumDictinorary(typeof(RelationalOpType)))
                opDictionary.Add(val.Key, val.Value);

            withDictionary = DataProviders.GetEnumDictinorary(typeof(LogicalOpType));

            linkTypeDictionary = new Dictionary<string, int>();
            linkTypeDictionary.Add("NONE", -1);

            foreach (var val in DataProviders.GetEnumDictinorary(typeof(LinkType)))
                linkTypeDictionary.Add(val.Key, (int)val.Value);

        }

        private void actionMenu_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var clickedMenu = e.ClickedItem.AccessibilityObject.Name;
            MenuAction(clickedMenu);
        }

        private void MenuAction(string clickedMenu)
        {
            bool reload = false;
            if (dataGridViewActionItems.CurrentRow != null && dataGridViewActionItems.SelectedRows.Count < 2 && dataGridViewActionItems.CurrentRow.Index < dataGridViewActionItems.Rows.Count - 1)
            {
                switch (clickedMenu)
                {
                    case ActionNewBelow:
                        {
                            var dr = dataGridViewActionItems.Rows;
                            var row = (DataGridViewRow)dataGridViewActionItems.Rows[0].Clone();
                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                row.Cells[i].Value = "";
                            }
                            dr.Insert(dataGridViewActionItems.CurrentRow.Index + 1, row);
                            reload = true;
                        }
                        break;
                    case ActionMoveUp:
                        {
                            if (dataGridViewActionItems.Rows.Count > 1 && dataGridViewActionItems.CurrentRow.Index != 0 && dataGridViewActionItems.CurrentRow.Index != dataGridViewActionItems.Rows.Count - 1)
                            {
                                var index = dataGridViewActionItems.CurrentRow.Index;

                                var dr = dataGridViewActionItems.Rows;
                                var row = (DataGridViewRow)dataGridViewActionItems.Rows[0].Clone();
                                for (int i = 0; i < row.Cells.Count; i++)
                                {
                                    row.Cells[i].Value = dataGridViewActionItems.Rows[index].Cells[i].Value;
                                }
                                dr.RemoveAt(index);
                                dr.Insert(index - 1, row);
                                reload = true;
                            }
                        }
                        break;
                    case ActionMoveDown:
                        {
                            if (dataGridViewActionItems.Rows.Count > 1 && dataGridViewActionItems.CurrentRow.Index != dataGridViewActionItems.Rows.Count - 2)
                            {
                                var index = dataGridViewActionItems.CurrentRow.Index;

                                var dr = dataGridViewActionItems.Rows;
                                var row = (DataGridViewRow)dataGridViewActionItems.Rows[0].Clone();
                                for (int i = 0; i < row.Cells.Count; i++)
                                {
                                    row.Cells[i].Value = dataGridViewActionItems.Rows[index].Cells[i].Value;
                                }
                                dr.RemoveAt(index);
                                dr.Insert(index + 1, row);
                                reload = true;
                            }
                        }
                        break;
                    case ActionDelete:
                        {

                            if (dataGridViewActionItems.Rows.Count > 1)
                            {
                                var index = dataGridViewActionItems.CurrentRow.Index;
                                var dr = dataGridViewActionItems.Rows;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            expression.QuestionTitle = tbQuestionTitle.Text;
            expression.Hint = tbSuggestions.Text;
            expression.SkipEvaluation = chkBxSkipEval.Checked;

            expression.FallbackAction = null;
            if (cbxFallbackActionNode.SelectedItem != null)
            {
                var id = (cbxFallbackActionNode.SelectedItem as ILinkInfo)._id;
                var type = (LinkType)((KeyValuePair<string, int>)cbxFallbackActionType.SelectedItem).Value;
                expression.FallbackAction = new ActionLink { LinkId = id, Type = type };
            }

            expression.ForwardAction = null;
            if (cbxForwardActionNode.SelectedItem != null)
            {
                var id = (cbxForwardActionNode.SelectedItem as ILinkInfo)._id;
                var type = (LinkType)((KeyValuePair<string, int>)cbxForwardActionType.SelectedItem).Value;
                expression.ForwardAction = new ActionLink { LinkId = id, Type = type };
            }

            expression.ExpressionTree = new ExpressionTree { Nodes = new List<IExpEval>() };
            var dt = ((KeyValuePair<string, Type>)cbxDataType.SelectedItem).Value;  //Data type

            foreach (DataGridViewRow row in dataGridViewActionItems.Rows)
            {
                var op = row.Cells[0].Value;    //Operation
                var rVal = row.Cells[1].Value as string;    //String data
                var with = row.Cells[2].Value;  //Logical with
                if (!string.IsNullOrWhiteSpace(rVal) && op != null && with != null)
                {
                    IExpEval curRes = null;
                    object parsedRVal = Convert((Type)dt, rVal);
                    if (parsedRVal != null)
                    {
                        if (op is RelationalOpType)
                        {
                            curRes = new RelationalOp { With = (LogicalOpType)with, RVal = parsedRVal, ROp = (RelationalOpType)op };
                        }
                        else if (op is ArithmeticOpType)
                        {
                            curRes = new ArithmeticOp { With = (LogicalOpType)with, RVal = parsedRVal, AOp = (ArithmeticOpType)op };
                        }
                    }

                    if (curRes != null)
                        expression.ExpressionTree.Nodes.Add(curRes);
                }
            }
        }

        public object Convert(Type type, string stringVal)
        {
            if (string.IsNullOrWhiteSpace(stringVal))
                return null;

            object boxedObject = null;
            if (type == typeof(bool))
            {
                bool val;
                if (bool.TryParse(stringVal, out val))
                {
                    boxedObject = val;
                }
            }
            else if (type == typeof(int))
            {
                int val;
                if (int.TryParse(stringVal, out val))
                {
                    boxedObject = val;
                }
            }
            else if (type == typeof(float))

            {
                float val;
                if (float.TryParse(stringVal, out val))
                {
                    boxedObject = val;
                }
            }
            else if (type == typeof(long))
            {
                long val;
                if (long.TryParse(stringVal, out val))
                {
                    boxedObject = val;
                }
            }
            else if (type == typeof(double))
            {
                double val;
                if (double.TryParse(stringVal, out val))
                {
                    boxedObject = val;
                }
            }
            else if (type == typeof(DateTime))
            {
                DateTime val;
                if (DateTime.TryParse(stringVal, out val))
                {
                    boxedObject = val;
                }
            }
            else if (type == typeof(string))
            {
                boxedObject = stringVal;
            }
            return boxedObject;
        }

        private void dataGridViewActionItems_KeyDown(object sender, KeyEventArgs e)
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

        private void dataGridViewActionItems_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            LoadRowNumbers();
        }

        private async void cbxForwardActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            var item = ((KeyValuePair<string, int>)cb.SelectedItem);

            if (item.Value < 0)
                ((BindingSource)cbxForwardActionNode.DataSource).DataSource = new List<ILinkInfo>();
            else
            {
                ((BindingSource)cbxForwardActionNode.DataSource).DataSource = await ((LinkType)(item.Value)).GetAllLinks();
            }
        }

        private async void cbxFallbackActionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cb = sender as ComboBox;
            var item = ((KeyValuePair<string, int>)cb.SelectedItem);
            if (item.Value < 0)
                ((BindingSource)cbxFallbackActionNode.DataSource).DataSource = new List<ILinkInfo>();
            else
            {
                ((BindingSource)cbxFallbackActionNode.DataSource).DataSource = await ((LinkType)(item.Value)).GetAllLinks();
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
                tbSuggestions.Text = "";
                suggestionsList.ForEach(item => tbSuggestions.Text += $"{item.Key}:{item.Value},");
            }
        }
    }
}
