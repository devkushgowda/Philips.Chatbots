using MongoDB.Driver;
using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Database.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Philips.Chatbots.Database.Common.DbAlias;

namespace Philips.Chatbots.Desktop.Portal.Data
{
    public static class DataProviders
    {
        private const string NoAction = "EmptyExpression";

        private const string ValidAction = "ValidAction";

        private static string rootNode => CurrentChatProfile().Result?.Root;

        public static Dictionary<string, object> GetEnumDictinorary(Type enumType)
        {
            return enumType.GetEnumValues().Cast<object>().ToDictionary(k => k.ToString().ToUpper(), v => v);
        }

        public async static Task ChangeProfile(string profile)
        {
            await DbBotCollection.SetActiveChatProfileById(BotAlphaName, profile);
            await SyncChatProfile();
        }

        public static async Task<string> GetCurrentModelFilePath()
        {
            var folder = (await BotConfiguration())?.Configuration?.DataFolder;

            return Path.Combine(folder, $"{DbLinkCollection.CollectionNamespace}.zip");
        }

        public async static Task<string> GetActiveProfile() => (await DbBotCollection.FindOneById(BotAlphaName))?.Configuration?.ActiveProfile;

        public async static Task<List<string>> GetChatProfiles()
        {
            var config = await DbBotCollection.FindOneById(BotAlphaName);
            if (config == null)
            {
                var botModel = new BotModel();
                botModel._id = BotAlphaName;
                config = await DbBotCollection.InsertNewOrUpdate(botModel);
            }
            return config?.Configuration?.ChatProfiles?.Select(item => item.Name).ToList();
        }

        public static async Task<List<ILinkInfo>> GetAllLinks(this LinkType type)
        {
            List<ILinkInfo> res = null;
            switch (type)
            {
                case LinkType.NeuralLink:
                    res = (await DbLinkCollection.Find(x => true).ToListAsync()).Select(x => x as ILinkInfo).ToList();
                    break;
                case LinkType.ActionLink:
                    res = (await DbActionCollection.Find(x => true).ToListAsync()).Select(x => x as ILinkInfo).ToList();
                    break;
                case LinkType.NeuralResource:
                    res = (await DbResourceCollection.Find(x => true).ToListAsync()).Select(x => x as ILinkInfo).ToList();
                    break;
                default:
                    break;
            }
            return res;
        }

        public static List<ILinkInfo> GetAllNeuralNodesLinks(LinkType[] ignore = null)
        {
            List<ILinkInfo> res = new List<ILinkInfo>();
            var ignoreLink = ignore?.Contains(LinkType.NeuralLink) ?? false;
            if (!ignoreLink)
                res.AddRange(DbLinkCollection.Find(x => true).ToList()?.Select(x => x as ILinkInfo).ToList());

            var ignoreAction = ignore?.Contains(LinkType.ActionLink) ?? false;
            if (!ignoreAction)
                res.AddRange(DbActionCollection.Find(x => true).ToList()?.Select(x => x as ILinkInfo).ToList());

            var ignoreResources = ignore?.Contains(LinkType.NeuralResource) ?? false;
            if (!ignoreResources)
                res.AddRange(DbResourceCollection.Find(x => true).ToList()?.Select(x => x as ILinkInfo).ToList());

            return res;
        }

        public static LinkType GetLinkType(this ILinkInfo link)
        {
            if (link is NeuralLinkModel)
                return LinkType.NeuralLink;
            else if (link is NeuralActionModel)
                return LinkType.ActionLink;
            else
                return LinkType.NeuralResource;
        }

        public static ImageList LoadNeuralLinkValidationImageList()
        {
            ImageList _imageList;
            _imageList = new ImageList();
            _imageList.Images.Add(NoAction, Properties.Resources.NeuralNodeWarning);
            _imageList.Images.Add(ValidAction, Properties.Resources.ValidNeuralNode);
            return _imageList;
        }

        public static ImageList LoadNeuralResourceTypesImageList()
        {
            ImageList _imageList;
            _imageList = new ImageList();
            _imageList.Images.Add(ResourceType.Audio.GetEnumValueName(), Properties.Resources.res_type_audio);
            _imageList.Images.Add(ResourceType.Video.GetEnumValueName(), Properties.Resources.res_type_video);
            _imageList.Images.Add(ResourceType.ImageGIF.GetEnumValueName(), Properties.Resources.res_type_gif);
            _imageList.Images.Add(ResourceType.ImageJPG.GetEnumValueName(), Properties.Resources.res_type_jpg);
            _imageList.Images.Add(ResourceType.ImagePNG.GetEnumValueName(), Properties.Resources.res_type_png);
            _imageList.Images.Add(ResourceType.DocumentPDF.GetEnumValueName(), Properties.Resources.res_type_pdf);
            _imageList.Images.Add(ResourceType.Json.GetEnumValueName(), Properties.Resources.res_type_json);
            _imageList.Images.Add(ResourceType.Script.GetEnumValueName(), Properties.Resources.res_type_script);
            _imageList.Images.Add(ResourceType.Text.GetEnumValueName(), Properties.Resources.res_type_text);
            _imageList.Images.Add(ResourceType.WebsiteUrl.GetEnumValueName(), Properties.Resources.res_type_url);
            return _imageList;
        }

        public static ImageList LoadNeuralActionTypesImageList()
        {
            ImageList _imageList;
            _imageList = new ImageList();
            _imageList.Images.Add(ActionType.CallSupport.GetEnumValueName(), Properties.Resources.action_type_call);
            _imageList.Images.Add(ActionType.ChatSupport.GetEnumValueName(), Properties.Resources.action_type_chat);
            _imageList.Images.Add(ActionType.Guide.GetEnumValueName(), Properties.Resources.action_type_guide);
            _imageList.Images.Add(ActionType.Others.GetEnumValueName(), Properties.Resources.action_type_others);
            _imageList.Images.Add(ActionType.Script.GetEnumValueName(), Properties.Resources.action_type_script);
            _imageList.Images.Add(ActionType.Service.GetEnumValueName(), Properties.Resources.action_type_service);
            return _imageList;
        }

        public static string GetEnumValueName<T>(this T val) => Enum.GetName(val.GetType(), val);

        public static bool ConfirmDialog(string message, string caption = "Confirmation", MessageBoxIcon icon = MessageBoxIcon.Question)
        {
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, icon);
            return result == DialogResult.Yes;
        }

        public static TreeNode FindChildFromName(this TreeNode rootNode, string nodeId)
        {
            foreach (TreeNode node in rootNode?.Nodes)
            {
                if (node.Name.Equals(nodeId)) return node;
                TreeNode next = node.FindChildFromName(nodeId);
                if (next != null) return next;
            }
            return null;
        }

        public static string GetNodeImage(this NeuralLinkModel link)
        {
            return link.CildrenRank.Count > 0 || link.NeuralExp != null ? ValidAction : NoAction;
        }

        public static async Task LoadTree(this TreeView treeView, string id = null)
        {
            LoadedTreeNodes.Clear();

            treeView.Invoke((MethodInvoker)delegate
            {
                treeView.Nodes.Clear();
            });

            TreeNode treeNode = null;
            var root = await DbLinkCollection.FindOneById(rootNode); ;
            if (root != null)
            {
                treeView.Invoke((MethodInvoker)delegate
                {
                    treeView.Enabled = false;
                    treeView.ImageList = LoadNeuralLinkValidationImageList();
                    // Running on the UI thread
                    treeNode = treeView.Nodes.Add(root._id, root.Name);
                    LoadedTreeNodes.Add(root._id);
                    treeNode.Tag = root;
                    treeNode.ImageKey = root.GetNodeImage();
                    treeView.SelectedNode = treeNode;
                });

                await treeNode?.LoadNodes(treeView);
                treeView.Invoke((MethodInvoker)delegate
                {
                    treeView.ExpandAll();
                    treeView.Enabled = true;
                    if (id != null)
                    {
                        treeView.SelectedNode = treeView.SelectedNode.FindChildFromName(id);
                    }
                    treeView.Focus();
                });
            }
        }


        private static List<string> LoadedTreeNodes = new List<string>();

        private static async Task LoadNodes(this TreeNode treeNode, TreeView treeView)
        {
            TreeNode curTreeNode = null;
            var root = await DbLinkCollection.FindOneById(treeNode.Name);
            if (root != null)
            {
                foreach (var child in root.CildrenRank)
                {
                    var node = await DbLinkCollection.FindOneById(child.Key);
                    if (node != null)
                    {
                        bool isLoopNode = false;

                        if (LoadedTreeNodes.Contains(node._id))
                        {
                            isLoopNode = true;
                        }
                        else
                        {
                            LoadedTreeNodes.Add(node._id);
                        }

                        treeView.Invoke((MethodInvoker)delegate
                        {
                            // Running on the UI thread
                            var nodeName = isLoopNode ? $"{node.Name} ({child.Value})(L)" : $"{node.Name} ({child.Value})";
                            curTreeNode = treeNode.Nodes.Add(node._id, nodeName);
                            curTreeNode.ImageKey = node.GetNodeImage();
                            curTreeNode.Tag = node;
                        });

                        if (!isLoopNode)
                            await curTreeNode.LoadNodes(treeView);

                    }
                }
            }
        }
    }
}
