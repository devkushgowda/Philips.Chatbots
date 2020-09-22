using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Database.Extension;
using Philips.Chatbots.Database.MongoDB;
using System.Collections.Generic;
using System.Threading.Tasks;
using Philips.Chatbots.ML.Models;
using System.Linq;
using Philips.Chatbots.ML.Interfaces;
using Microsoft.Extensions.ML;

namespace Philips.Chatbots.Engine.Test
{
    /// <summary>
    /// Test class for testing bot database.
    /// </summary>
    public static class BotDbTestClass
    {
        private static bool initilized = false;
        public async static Task Feed(string botId, Microsoft.Extensions.ML.PredictionEnginePool<NeuralTrainInput, PredictionOutput> predictionEnginePool)
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


            var superRootNode = await linkCollection.InsertNew(new NeuraLinkModel
            {
                Name = "SuperRoot",
                Notes = new List<string> { "Welcome to philips chatbot mobile device assistant beta version!" },
                NeuralExp = new DecisionExpression
                {
                    QuestionTitle = "Choose the conversation mode:",
                    Hint = $"Simple:simple,Advanced:{BotResourceKeyConstants.CommandAdvanceChat}",
                    ExpressionTree = ExpressionBuilder.Build().EQ("simple")
                }
            });


            //configure it as bot root node
            await botCollection.SetRootNodeById(botId, superRootNode._id);



            var nodeSimpleChatMode = await linkCollection.InsertChildById(superRootNode._id, new NeuraLinkModel
            {
                Name = "SimpleChatNode",
                Title = $"[{BotResourceKeyConstants.WhatIssue}]"
            });

            //Link super root forward action to simple chat mode
            await linkCollection.SetNeuralExpForwardLinkById(superRootNode._id, new ActionLink { Type = LinkType.NeuralLink, Id = nodeSimpleChatMode._id });



            var nodeSound = await linkCollection.InsertChildById(nodeSimpleChatMode._id, new NeuraLinkModel
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
                Name = "UnpleasantSound0",
                IsLocal = false,
                Type = ResourceType.Video,
                Location = "https://www.youtube.com/watch?v=Y_hEEEt-Rb0",
                Title = "[Solution] Mobile speaker producing noisy (crackling) sound fixed without​ replacing speaker"
            });
            var resourceUnpleasantSound1 = await resourceCollection.InsertNew(new NeuralResourceModel
            {
                Name = "UnpleasantSound1",
                IsLocal = false,
                Type = ResourceType.ImageJPG,
                Location = "https://1.bp.blogspot.com/-74qhJFnbScQ/Xme-O_OkPnI/AAAAAAAADmw/v3iLjmTSBVYoRWH6HVenl5WYrddv2rd2QCLcBGAsYHQ/s320/Samsung%2BGT-E1207T%2BEar%2BSpeaker%2BJumpur%2BSolution.jpg",
                Title = "[Solution] Mobile speaker producing noisy (crackling) sound fixed without​ replacing speaker"
            });

            var actionUnpleasantSound = await actionCollection.InsertNew(new NeuralActionModel
            {
                Name = "UnpleasantSoundAction",
                Title = $"[{BotResourceKeyConstants.FoundSolution}]",
                Resources = new List<string> { resourceUnpleasantSound._id, resourceUnpleasantSound1._id }
            });

            var resourceCustomerSupport = await resourceCollection.InsertNew(new NeuralResourceModel
            {
                Name = "customersupport ",
                IsLocal = false,
                Type = ResourceType.WebsiteUrl,
                Location = "https://www.philips.co.in/c-w/support-home/support-contact-page.html",
                Title = "Please reach us @support site"
            });

            var actionCustomerSupport = await actionCollection.InsertNew(new NeuralActionModel
            {
                Name = "customersupportAction",
                Title = $"[{BotResourceKeyConstants.FoundSolution}]",
                Resources = new List<string> { resourceCustomerSupport._id }
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
                    Hint = "Yes:yes,No:no",
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
            var nodeDisplay = await linkCollection.InsertChildById(nodeSimpleChatMode._id, new NeuraLinkModel
            {
                Name = "Display/Broken screen",
                Title = $"[{BotResourceKeyConstants.SelectedIssue}]",
                Notes = new List<string> { $"[{BotResourceKeyConstants.WeHelpYou}]", $"[{BotResourceKeyConstants.WhatIssue}]" }
            });

            #region display

            #endregion
            var nodeBattery = await linkCollection.InsertChildById(nodeSimpleChatMode._id, new NeuraLinkModel
            {
                Name = "Battery/Charging",
                Title = $"[{BotResourceKeyConstants.SelectedIssue}]",
                Notes = new List<string> { $"[{BotResourceKeyConstants.WeHelpYou}]", $"[{BotResourceKeyConstants.WhatIssue}]" }
            });

            //await linkCollection.UnLinkParentChild(nodeSimpleChatMode._id, nodeBattery._id);

            #region battery

            #endregion

            #region resources
            var stringRes = new List<KeyValuePair<string, string>> {
            new KeyValuePair<string, string>(BotResourceKeyConstants.ThankYou, "Thank you, Have a great day!"),
             new KeyValuePair<string, string>(BotResourceKeyConstants.WhatIssue, "What in the following are you facing issue with?"),
             new KeyValuePair<string, string>(BotResourceKeyConstants.SelectedIssue, "You have selected '{Name}' category issues."),
             new KeyValuePair<string, string>(BotResourceKeyConstants.WeHelpYou, "That's terrific!\n do not worry, we are here to help you."),
             new KeyValuePair<string, string>(BotResourceKeyConstants.CannotMoveBack, "Cannont move back, No history recorded yet."),
             new KeyValuePair<string, string>(BotResourceKeyConstants.Error, "Encountered an error while processing request, please contact bot administrator."),
             new KeyValuePair<string, string>(BotResourceKeyConstants.FoundSolution, "Here we found few matching solutions"),
             new KeyValuePair<string, string>(BotResourceKeyConstants.InvalidInput, "Invalid input, please try again."),
             new KeyValuePair<string, string>(BotResourceKeyConstants.ThankYouFeedback, "Kudos for your feedback."),
             new KeyValuePair<string, string>(BotResourceKeyConstants.StartAgain, "Facing another issue?:start"),
             new KeyValuePair<string, string>(BotResourceKeyConstants.AdvanceChatQuery, "Tell us about your issue:"),
             new KeyValuePair<string, string>(BotResourceKeyConstants.NoMatchFound, "No match found, try again in different words."),
             new KeyValuePair<string, string>(BotResourceKeyConstants.Feedback, "Please help us improve our service, Was the soulution helpful?"),

                new KeyValuePair<string, string>(BotResourceKeyConstants.FeedBackOptions, "Yes:yes,No:no,Exit:exit"),
                new KeyValuePair<string, string>(BotResourceKeyConstants.CommonActionOptions, "Back:back,Exit:exit")
            };

            await botCollection.AddStringResourceBatchById(botId, stringRes);
            #endregion

            #region ML model test data
            var speakerTrainModel = new NeuraTrainDataModel
            {
                _id = nodeSound._id,
                Dataset = new List<string> {
                    "Speaker is not working",
                    "Mobile sound issue",
                    "No sound",
                    "Unpleasant sound from speaker" ,
                    "Cant hear sound",
                    "Noisy music"
                }
            };

            await trainDataCollection.InsertNew(speakerTrainModel);

            var displayTrainModel = new NeuraTrainDataModel
            {
                _id = nodeDisplay._id,
                Dataset = new List<string> {
                    "Display is not working",
                    "Mobile display issue",
                    "Broken screen",
                    "Screen cracked" ,
                    "Lines on display",
                    "Issue with mobile display"
                }
            };

            await trainDataCollection.InsertNew(displayTrainModel);

            //Build and tran the model
            new NeuralTrainingEngine().BuildAndSaveModel();

            List<string> mlTestData = new List<string> { "my mobile screen is broken", "my speaker is not working", "broken screen" };
            var output = mlTestData.Select(item => linkCollection.FindOneById(predictionEnginePool.Predict(nameof(NeuralPredictionEngine), new NeuralTrainInput { Text = item })._id).Result.Name).ToList();
            #endregion
            initilized = true;
        }
    }
}
