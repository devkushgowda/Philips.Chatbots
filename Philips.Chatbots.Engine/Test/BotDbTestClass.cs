using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Database.Extension;
using Philips.Chatbots.Database.MongoDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Philips.Chatbots.Engine.Test
{
    /// <summary>
    /// Test class for testing bot database.
    /// </summary>
    public static class BotDbTestClass
    {
        private static bool initilized = false;
        public async static Task Feed(string botId)
        {
            if (initilized)
                return;

            await MongoDbProvider.DropDatabase();
            var linkCollection = MongoDbProvider.GetCollection<NeuraLinkModel>();
            var resourceCollection = MongoDbProvider.GetCollection<NeuralResourceModel>();
            var actionCollection = MongoDbProvider.GetCollection<NeuralActionModel>();
            var trainDataCollection = MongoDbProvider.GetCollection<NeuraTrainDataModel>();
            var botCollection = MongoDbProvider.GetCollection<BotModel>();

            var botConfiguration = await botCollection.InsertNew(new BotModel
            {
                _id = botId,
                Description = "Simple bot",
                EndPoint = "https://localhost:44388/api/messages",
                Configuration = new BotConfiguration { }
            }, botId);

            //RootNode

            var rootNode = await linkCollection.InsertNew(new NeuraLinkModel
            {
                Name = "RootNode",
                Title = "Welcome to philips chatbot mobile device assistant beta version!\n" +
                "Note: Do not type anything unless asked for it:)",
                Notes = new List<string> { $"[{BotResourceKeyConstants.WhatIssue}]" }
            });

            //configure it as root node
            await botCollection.SetRootNodeById(botId, rootNode._id);
            var nodeSound = await linkCollection.InsertChildById(rootNode._id, new NeuraLinkModel
            {
                Name = "Sound/Speaker",
                Title = $"[{BotResourceKeyConstants.SelectedIssue}]",
                Notes = new List<string> { $"[{BotResourceKeyConstants.WeHelpYou}]", $"[{BotResourceKeyConstants.WhatIssue}]" }
            });

            #region sound


            var resourceMute = await resourceCollection.InsertNew(new NeuralResourceModel
            {
                Name = "NoSound",
                IsLocal = false,
                Type = ResourceType.Video,
                Location = "https://www.youtube.com/watch?v=8Y8HzSBMujQ",
                Title = "How to fix sound problem on any android"
            });

            var actionMute = await actionCollection.InsertNew(new NeuralActionModel
            {
                Name = "NoSoundAction",
                Title = $"[{BotResourceKeyConstants.FoundSolution}]",
                Resources = new List<string> { $"{resourceMute._id}" }
            });

            var resourceUnpleasantSound = await resourceCollection.InsertNew(new NeuralResourceModel
            {
                Name = "UnpleasantSound ",
                IsLocal = false,
                Type = ResourceType.Video,
                Location = "https://www.youtube.com/watch?v=Y_hEEEt-Rb0",
                Title = "[Solution] Mobile speaker producing noisy (crackling) sound fixed without​ replacing speaker"
            });

            var actionUnpleasantSound = await actionCollection.InsertNew(new NeuralActionModel
            {
                Name = "UnpleasantSoundAction",
                Title = $"[{BotResourceKeyConstants.FoundSolution}]",
                Resources = new List<string> { $"{resourceUnpleasantSound._id}" }
            });

            var resourceCustomerSupport = await resourceCollection.InsertNew(new NeuralResourceModel
            {
                Name = "customersupport ",
                IsLocal = false,
                Type = ResourceType.Video,
                Location = "https://www.philips.co.in/c-w/support-home/support-contact-page.html",
                Title = "Please reach us @support site"
            });

            var actionCustomerSupport = await actionCollection.InsertNew(new NeuralActionModel
            {
                Name = "customersupportAction",
                Title = $"[{BotResourceKeyConstants.FoundSolution}]",
                Resources = new List<string> { $"{resourceCustomerSupport._id}" }
            });

            var nodeNoSound = await linkCollection.InsertChildById(nodeSound._id, new NeuraLinkModel
            {
                Name = "No sound",
                Title = "You have selected {Name} category issues!",
                Notes = new List<string> { $"This might help you fixing it!" },
                NeuralExp = new DecisionExpression
                {
                    SkipEvaluation = true,
                    ForwardAction = new ActionLink
                    {
                        Type = LinkType.ActionLink,
                        Id = actionMute._id
                    }
                }
            });

            var nodeUnpleasantSound = await linkCollection.InsertChildById(nodeSound._id, new NeuraLinkModel
            {
                Name = "Unpleasant sound",
                Title = $"[{BotResourceKeyConstants.SelectedIssue}]",
                Notes = new List<string> { $"[{BotResourceKeyConstants.WeHelpYou}]", $"[{BotResourceKeyConstants.WhatIssue}]" },
                NeuralExp = new DecisionExpression
                {
                    Hint = "Yes:Yes,No:No",
                    QuestionTitle = "Did your device fall?",
                    ExpressionTree = ExpressionBuilder.Build().EQ("yes"),
                    ForwardAction = new ActionLink
                    {
                        Type = LinkType.ActionLink,
                        Id = actionUnpleasantSound._id
                    },
                    FallbackAction = new ActionLink
                    {
                        Type = LinkType.ActionLink,
                        Id = actionCustomerSupport._id
                    }
                }
            }); ;

            #endregion
            var nodeDisplay = await linkCollection.InsertChildById(rootNode._id, new NeuraLinkModel
            {
                Name = "Display/Broken screen",
                Title = $"[{BotResourceKeyConstants.SelectedIssue}]",
                Notes = new List<string> { $"[{BotResourceKeyConstants.WeHelpYou}]", $"[{BotResourceKeyConstants.WhatIssue}]" }
            });
            #region display

            #endregion
            var nodeBattery = await linkCollection.InsertChildById(rootNode._id, new NeuraLinkModel
            {
                Name = "Battery/Charging",
                Title = $"[{BotResourceKeyConstants.SelectedIssue}]",
                Notes = new List<string> { $"[{BotResourceKeyConstants.WeHelpYou}]", $"[{BotResourceKeyConstants.WhatIssue}]" }
            });

            #region battery

            #endregion


            var stringRes = new List<KeyValuePair<string, string>> {
            new KeyValuePair<string, string>(BotResourceKeyConstants.ThankYou, "Thank you, Have a great day!"),
             new KeyValuePair<string, string>(BotResourceKeyConstants.WhatIssue, "What in the following are you facing issue with?"),
             new KeyValuePair<string, string>(BotResourceKeyConstants.SelectedIssue, "You have selected '{Name}' category issues!"),
             new KeyValuePair<string, string>(BotResourceKeyConstants.WeHelpYou, "That's terrific!\n Do not worry, we are here to help you."),
             new KeyValuePair<string, string>(BotResourceKeyConstants.FoundSolution, "Here we found few solutions for you")};

            await botCollection.AddStringResourceBatchById(botId, stringRes);
            initilized = true;
        }
    }
}
