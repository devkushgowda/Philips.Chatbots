using MongoDB.Bson;
using MongoDB.Driver;
using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Database.Extension;
using Philips.Chatbots.Database.MongoDB;
using Philips.Chatbots.Desktop.Portal.Configuration;
using Philips.Chatbots.Desktop.Portal.Data;
using Philips.Chatbots.Desktop.Portal.Forms.Loader;
using Philips.Chatbots.ML.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Philips.Chatbots.Database.Common.DbAlias;

namespace Philips.Chatbots.Desktop.Portal
{
    public partial class Main : Form
    {
        private const string MenuActionNew = "New child";
        private const string MenuActionDelete = "Delete";
        private const string MenuActionMapChild = "Map new child";
        private const string MenuActionUnmapChild = "Unmap from parent";
        private const string MenuActionNewProfile = "New chat profile";
        private const string MenuActionDeleteProfile = "Delete chat profile";

        private const string ActionResultDeleteProfile = "Browse output location and verify trained model result";

        private const string ExpressionNone = "None";

        public Main()
        {
            InitializeComponent();
            AddContextMenu();
            LoadExpressionTypes();
        }

        private async Task LoadChatProfiles()
        {
            cbxChatProfiles.Items.Clear();
            (await DataProviders.GetChatProfiles()).ForEach(item => cbxChatProfiles.Items.Add(item));
            var activeProfile = await DataProviders.GetActiveProfile();
            if (!cbxChatProfiles.Items.Contains(activeProfile))
            {
                activeProfile = cbxChatProfiles.Items.Count > 0 ? cbxChatProfiles.Items[0].ToString() : null;
            }
            cbxChatProfiles.SelectedItem = activeProfile;
        }

        private async Task LoadOtherActionsData()
        {
            await UpdateResourceLabelTextAsync();
            await UpdateActionLabelTextAsync();
            await UpdateTrainModelLabelTextAsync();
        }

        private async Task UpdateActionLabelTextAsync()
        {
            lnkNeuralActions.Text = $"Neural actions ({await DbActionCollection.CountDocumentsAsync(new BsonDocument())})";
        }

        private async Task UpdateTrainModelLabelTextAsync()
        {
            lnkTrainModel.Text = await GetTrainModelText();
            var trainDataCount = await DbTrainDataCollection.Find(i => true).CountDocumentsAsync();
            lnkTrainModel.Enabled = trainDataCount > 1;
        }

        private async Task UpdateResourceLabelTextAsync()
        {
            lnkNeuralResources.Text = $"Neural resources ({ await DbResourceCollection.CountDocumentsAsync(new BsonDocument())})";
        }

        private async Task<string> GetTrainModelText()
        {
            var res = await CurrentModelFileExists();
            return res ? $"Retrain model" : "Train model";
        }

        private async Task<bool> CurrentModelFileExists()
        {
            var path = await DataProviders.GetCurrentModelFilePath();
            return !string.IsNullOrWhiteSpace(path) && File.Exists(path);
        }

        public static Dictionary<string, Type> supportedTypes = new Dictionary<string, Type> { { typeof(DecisionExpression).Name, typeof(DecisionExpression) }, { typeof(LinkExpression).Name, typeof(LinkExpression) }, { ExpressionNone, null } };

        private void LoadExpressionTypes()
        {

            var bindingSource = new BindingSource();
            bindingSource.DataSource = supportedTypes.Select(item => item.Key);
            cbxExpressionTypes.DataSource = bindingSource;
        }

        private void AddContextMenu()
        {
            var actionMenu = new ContextMenuStrip();

            actionMenu.Items.AddRange(new ToolStripMenuItem[] { new ToolStripMenuItem() { Text = MenuActionNew },
                new ToolStripMenuItem() { Text = MenuActionMapChild },
                new ToolStripMenuItem() { Text = MenuActionUnmapChild },
                new ToolStripMenuItem() { Text = MenuActionDelete },
                new ToolStripMenuItem() { Text = MenuActionNewProfile },
                new ToolStripMenuItem() { Text = MenuActionDeleteProfile }
            });

            actionMenu.ItemClicked += actionMenu_Clicked;
            neuralTree.ContextMenuStrip = actionMenu;
        }

        private async Task CreateNewChatProfile()
        {
            var picker = new SingleInputForm("Enter profile name", "", "Create");
            var res = picker.ShowDialog();
            if (res == DialogResult.OK && !string.IsNullOrWhiteSpace(picker.ResultString))
            {
                await DbBotCollection.AddOrUpdateChatProfileById(BotAlphaName, new BotChatProfile { Name = picker.ResultString });
                await DbBotCollection.SetActiveChatProfileById(BotAlphaName, picker.ResultString);
                await LoadChatProfiles();
            }
        }

        private async Task DeleteCurrentChatProfile()
        {
            if (cbxChatProfiles.Items.Count > 1)
            {
                var profile = cbxChatProfiles.SelectedItem?.ToString();
                if (DataProviders.ConfirmDialog($"Do you want to permanently delete chat profile '{profile}'?\n\n" +
                    $"This action will automatically set next available profile as active profile.", "MenuActionDelete confirmation", MessageBoxIcon.Warning))
                {
                    await DbBotCollection.RemoveChatProfileById(BotAlphaName, profile);
                    await DbBotCollection.SetActiveChatProfileById(BotAlphaName, null); //Set active profile to null
                    await LoadChatProfiles();
                    await DbBotCollection.SetActiveChatProfileById(BotAlphaName, cbxChatProfiles.SelectedItem?.ToString());
                }
            }
        }

        private async Task FillForm()
        {
            if (neuralTree.SelectedNode != null)
            {
                gbNeuralNodeConfiguration.Enabled = true;

                var node = (NeuralLinkModel)neuralTree.SelectedNode.Tag;
                tbId.Text = node._id;
                tbName.Text = node.Name;
                tbTitle.Text = node.Title;
                tbQuestionTitle.Text = node.QuestionTitle;
                tbDescription.Text = node.Description;

                SetExpressionType(node);

                var nodeID = neuralTree.SelectedNode.Name;
                var trainData = await DbTrainDataCollection.FindOneById(nodeID);
                string state = (trainData != null ? trainData.Dataset.Count.ToString() : "NA");

                lnkTrainData.Text = $"Train data ({state})";
                lnkLabels.Text = $"Edit labels ({(node.Labels == null ? 0 : node.Labels.Count)})";
                lnkNotes.Text = $"Edit notes ({(node.Notes == null ? 0 : node.Notes.Count)})";
                lnkNeuralExpression.Enabled = node.NeuralExp != null;
            }
            else
            {
                ResetForm();
            }
        }

        private void SetExpressionType(NeuralLinkModel node)
        {
            if (node.NeuralExp != null)
            {
                var type = node.NeuralExp.GetType();
                cbxExpressionTypes.SelectedItem = type.Name;
            }
            else
            {
                cbxExpressionTypes.SelectedItem = ExpressionNone;
            }
        }

        private async Task ShowNeuralExpressionEditor()
        {
            var type = (string)cbxExpressionTypes.SelectedItem;
            var node = (NeuralLinkModel)neuralTree.SelectedNode.Tag;

            if (neuralTree.SelectedNode != null && type != ExpressionNone && supportedTypes.ContainsKey(type) && node.NeuralExp?.GetType() == supportedTypes[type])
            {
                if (supportedTypes[type] == typeof(LinkExpression))
                {
                    var exp = node.NeuralExp as LinkExpression;
                    var picker = new LinkExpressionEditor(exp);
                    var res = picker.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        await DbLinkCollection.ReplaceOneById(node._id, node);
                    }
                }
                else if (supportedTypes[type] == typeof(DecisionExpression))
                {
                    var exp = node.NeuralExp as DecisionExpression;
                    var picker = new DecisionExpressionEditor(exp);
                    var res = picker.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        await DbLinkCollection.ReplaceOneById(node._id, node);
                    }
                }
            }
        }

        private void ReloadTree()
        {
            ResetForm();
            var selectedNodeId = neuralTree?.SelectedNode?.Name;
            Task.Run(() => neuralTree.LoadTree(selectedNodeId));
        }

        private void ResetForm()
        {
            tbDescription.Text = "";
            tbId.Text = "";
            tbName.Text = "";
            tbQuestionTitle.Text = "";
            tbTitle.Text = "";
            cbxExpressionTypes.SelectedItem = ExpressionNone;

            lnkTrainData.Text = "Train data";
            lnkNotes.Text = "Edit notes";
            lnkLabels.Text = "Edit labels";

            gbNeuralNodeConfiguration.Enabled = false;
        }

        private async void lnLabelTrainData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (neuralTree.SelectedNode != null)
            {
                bool isNew = false;
                var nodeID = neuralTree.SelectedNode.Name;
                var trainData = await DbTrainDataCollection.FindOneById(nodeID);
                if (trainData == null)
                {
                    trainData = new NeuraTrainDataModel { _id = nodeID, Dataset = new List<string> { } };
                    isNew = true;
                }
                var editor = new ListEditor("Neural train data editor", trainData.Dataset);
                var res = editor.ShowDialog();
                if (res == DialogResult.OK)
                {
                    trainData.Dataset = editor.Result;
                    if (isNew)
                        await DbTrainDataCollection.InsertNew(trainData);
                    else
                        await DbTrainDataCollection.ReplaceOneById(nodeID, trainData);
                    lnkTrainData.Text = $"Train data ({trainData.Dataset.Count})";


                }
            }
        }

        private async void lnkLabelRefreshTree_LinkClicked(object sender, EventArgs e)
        {
            await LoadChatProfiles();
        }

        private async void buttonApply_Click(object sender, EventArgs e)
        {
            if (neuralTree.SelectedNode != null)
            {
                var node = (NeuralLinkModel)neuralTree.SelectedNode.Tag;
                neuralTree.SelectedNode.Text = node.Name = tbName.Text;
                node.Title = tbTitle.Text;
                node.QuestionTitle = tbQuestionTitle.Text;
                node.Description = tbDescription.Text;
                bool showNeuralExpEditor = false;
                var type = (string)cbxExpressionTypes.SelectedItem;
                var curType = node.NeuralExp?.GetType().Name ?? ExpressionNone;
                if (curType != type && supportedTypes.ContainsKey(type))
                {
                    if (type == ExpressionNone)
                    {
                        node.NeuralExp = null;
                    }
                    else
                    {
                        node.NeuralExp = type == null ? null : (INeuralExpression)Activator.CreateInstance(supportedTypes[type]);
                        showNeuralExpEditor = true;
                    }

                }

                await DbLinkCollection.ReplaceOneById(node._id, node);

                if (showNeuralExpEditor)
                    await ShowNeuralExpressionEditor();

                ReloadTree();
            }
        }

        private async void lnlLabelNeuralExp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            await ShowNeuralExpressionEditor();
        }

        private async void neuralTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            await FillForm();
            e.Node.SelectedImageKey = e.Node.ImageKey;
        }

        private async void actionMenu_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var clicked = e.ClickedItem.AccessibilityObject.Name;
            {

                if (neuralTree.SelectedNode != null)
                {
                    switch (clicked)
                    {
                        case MenuActionNew:
                            {
                                var newNode = await DbLinkCollection.InsertChildById(neuralTree.SelectedNode.Name, new NeuralLinkModel { Name = "New node" });
                                var newTreeNode = neuralTree.SelectedNode.Nodes.Add(newNode._id, newNode.Name);
                                newTreeNode.Tag = newNode;
                                neuralTree.SelectedNode = newTreeNode;
                                ReloadTree();
                            }
                            break;
                        case MenuActionDelete:
                            {
                                if (DataProviders.ConfirmDialog($"Do you want to permanently delete node '{neuralTree.SelectedNode.Text}'?\n\n" +
                                    $"Warning: This will also deletes from all the references to this node in other parent nodes of this.\n\n" +
                                    $"Note: Refresh tree to view the updated changes.", "MenuActionDelete confirmation", MessageBoxIcon.Warning))
                                {

                                    var node = (NeuralLinkModel)neuralTree.SelectedNode.Tag;
                                    await DbLinkCollection.RemoveAndUnlinkFromParents(node);
                                    await DbTrainDataCollection.RemoveOneById(node._id);
                                    neuralTree.SelectedNode.Remove();
                                    ReloadTree();
                                }
                            }
                            break;
                        case MenuActionMapChild:
                            {
                                var selectedNode = (NeuralLinkModel)neuralTree.SelectedNode.Tag;
                                var picker = new NodePicker(LinkType.NeuralLink, selectedNode._id, $"Map child to : {selectedNode.Name}");
                                var res = picker.ShowDialog();
                                if (res == DialogResult.OK && picker.NodeId != selectedNode._id)
                                {
                                    var node = await DbLinkCollection.FindOneById(picker.NodeId);
                                    if (node != null)
                                    {
                                        await DbLinkCollection.LinkParentChild(selectedNode._id, picker.NodeId);
                                        var newTreeNode = neuralTree.SelectedNode.Nodes.Add(node._id, node.Name);
                                        newTreeNode.Tag = node;
                                        ReloadTree();
                                    }
                                }
                            }
                            break;
                        case MenuActionUnmapChild:
                            {
                                await DbLinkCollection.UnLinkParentChild(neuralTree.SelectedNode.Parent.Name, neuralTree.SelectedNode.Name);
                                neuralTree.SelectedNode.Remove();
                                ReloadTree();
                            }
                            break;
                        default:
                            break;
                    }
                }

                switch (clicked)
                {
                    case MenuActionDeleteProfile:
                        await DeleteCurrentChatProfile();
                        break;
                    case MenuActionNewProfile:
                        await CreateNewChatProfile();
                        break;
                    case MenuActionNew:
                        {
                            if (neuralTree.Nodes.Count == 0)
                            {
                                var rootNode = await DbLinkCollection.InsertNew(new NeuralLinkModel { Name = "RootNode" });
                                await DbBotCollection.SetRootNodeById(BotAlphaName, rootNode._id, await DataProviders.GetActiveProfile());

                                neuralTree.ImageList = DataProviders.LoadNeuralLinkValidationImageList();
                                var treeNode = neuralTree.Nodes.Add(rootNode._id, rootNode.Name);
                                treeNode.Tag = rootNode;
                                treeNode.ImageKey = rootNode.GetNodeImage();
                                neuralTree.SelectedNode = treeNode;
                                ReloadTree();
                            }
                        }
                        break;
                    default:
                        break;
                }

                neuralTree.ExpandAll();

            }
        }

        private async void cbxChatProfiles_TextChanged(object sender, EventArgs e)
        {
            var chatProfile = cbxChatProfiles.Text;
            if (!string.IsNullOrWhiteSpace(chatProfile))
            {
                await DataProviders.ChangeProfile(chatProfile);
                await LoadOtherActionsData();
                ReloadTree();
                lnkProgressResultAction.Visible = false;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadDatabases();
            lnkClone.Enabled = cbxDataBases.Items.Count > 1;
        }

        private void LoadDatabases()
        {
            var config = Program.AppConfiguration;
            cbxDataBases.Items.AddRange(config.DbConnections.Select(x => x.Key).ToArray());
            cbxDataBases.Text = config.ActiveDb;
        }

        private async void lnkTrainModel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var count = await DbTrainDataCollection.Find(i => true).CountDocumentsAsync();
            if (count > 1)
            {
                cbxDataBases.Enabled = false;
                cbxChatProfiles.Enabled = false;
                lnkProgressResultAction.Visible = true;
                lnkProgressResultAction.Enabled = false;
                lnkProgressResultAction.Text = $"Trainig the model for profile {cbxChatProfiles.SelectedItem}";
                lnkTrainModel.Enabled = false;
                var location = new FormLocation { Location = Location, Height = Height, Width = Width };
                await Task.Run(() =>
                {
                    var loader = new Dialog("Training your data model", "Good things take time!", "Cancel", "Continue editing",
                          () => new NeuralTrainingEngine().BuildAndSaveModel(), location);
                    loader.ShowDialog();
                }).ConfigureAwait(true);

                lnkTrainModel.Text = await GetTrainModelText();
                var traindataCreated = await CurrentModelFileExists();
                if (traindataCreated)
                {
                    lnkProgressResultAction.Enabled = true;
                    lnkProgressResultAction.Text = ActionResultDeleteProfile;
                }
                else
                {
                    lnkProgressResultAction.Visible = false;
                }
                lnkTrainModel.Enabled = true;
                cbxChatProfiles.Enabled = true;
                cbxDataBases.Enabled = true;
            }
        }

        private async void lnkNeuralResources_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new NeuralResourcesEditor().ShowDialog();
            await UpdateResourceLabelTextAsync();
        }

        private async void lnkNeuralActions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new NeuralActionsEditor().ShowDialog();
            await UpdateActionLabelTextAsync();
        }

        private async void lnkBotConfigurations_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var botModel = await BotConfiguration();
            if (botModel != null)
            {
                var editor = new BotConfigurationEditor(botModel);
                var res = editor.ShowDialog();
                if (res == DialogResult.OK)
                {
                    await DbBotCollection.InsertNewOrUpdate(botModel);
                    await LoadChatProfiles();
                }
            }
        }

        private async void lnkProgressResultAction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            switch (lnkProgressResultAction.Text)
            {
                case ActionResultDeleteProfile:
                    {
                        System.Diagnostics.Process.Start("explorer.exe", (await BotConfiguration())?.Configuration?.DataFolder);
                        lnkProgressResultAction.Visible = false;
                    }
                    break;
                default:
                    break;
            }
        }

        private async void lnkLabelNotes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (neuralTree.SelectedNode != null)
            {
                var node = (NeuralLinkModel)neuralTree.SelectedNode.Tag;
                var editor = new ListEditor("Neural node notes editor", node.Notes);
                var res = editor.ShowDialog();
                if (res == DialogResult.OK)
                {
                    node.Notes = editor.Result;
                    await DbLinkCollection.ReplaceOneById(node._id, node);
                    ReloadTree();
                }
            }
        }

        private async void lnkLabels_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (neuralTree.SelectedNode != null)
            {
                var node = (NeuralLinkModel)neuralTree.SelectedNode.Tag;
                var editor = new ListEditor("Neural node labels editor", node.Labels);
                var res = editor.ShowDialog();
                if (res == DialogResult.OK)
                {
                    node.Labels = editor.Result;
                    await DbLinkCollection.ReplaceOneById(node._id, node);
                    ReloadTree();
                }
            }
        }

        private async void cbDataBases_TextChanged(object sender, EventArgs e)
        {
            var config = Program.AppConfiguration;
            var dataBase = cbxDataBases.Text;
            if (!string.IsNullOrWhiteSpace(dataBase))
            {
                if (config.ActiveDb != dataBase)
                {
                    config.ActiveDb = dataBase;
                    AppSettings.SaveConfiguration(config);
                }
                gbOtherConfigurations.Enabled = false;
                gbNeuralNodeConfiguration.Enabled = false;
                cbxChatProfiles.Enabled = false;
                MongoDbProvider.Connect(config.DbConnections[dataBase]);
                await SyncChatProfile();
                await LoadChatProfiles();
                gbOtherConfigurations.Enabled = true;
                cbxChatProfiles.Enabled = true;
            }
        }

        private async void lnkClone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (DialogResult.OK == new CloneDatabase().ShowDialog())
            {
                await LoadChatProfiles();
            }
        }
    }
}
