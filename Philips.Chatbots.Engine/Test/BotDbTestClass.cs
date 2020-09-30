using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Database.Extension;
using Philips.Chatbots.Database.MongoDB;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Philips.Chatbots.Database.Common.DbAlias;

namespace Philips.Chatbots.Engine.Test
{
    /// <summary>
    /// Test class for testing bot database.
    /// </summary>
    public static class BotDbTestClass
    {
        /// <summary>
        /// Call this to create and feed test database
        /// </summary>
        /// <param name="botId"></param>
        /// <param name="dropDatabase"></param>
        /// <param name="profileName"></param>
        /// <returns></returns>
        public async static Task Feed(string botId, bool dropDatabase, string profileName = BotChatProfile.DefaultProfile)
        {
            if (dropDatabase)
            {
                await MongoDbProvider.DropDatabase();
                await DbBotCollection.InsertNewOrUpdate(new BotModel { _id = BotAlphaName, Configuration = new BotConfiguration { ActiveProfile = profileName, ChatProfiles = new List<BotChatProfile> {  } } });
            }

            await DbBotCollection.AddOrUpdateChatProfileById(BotAlphaName, new BotChatProfile { Name = profileName });
            await DbBotCollection.SetActiveChatProfileById(BotAlphaName, profileName);

            await SyncChatProfile();


            var superRootNode = await DbLinkCollection.InsertNew(new NeuralLinkModel
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
            await DbBotCollection.SetRootNodeById(botId, superRootNode._id, profileName);



            var nodeSimpleChatMode = await DbLinkCollection.InsertChildById(superRootNode._id, new NeuralLinkModel
            {
                Name = "SimpleChatNode",
                Title = $"[{BotResourceKeyConstants.WhatIssue}]"
            });

            //Link super root forward action to simple chat mode
            await DbLinkCollection.SetNeuralExpForwardLinkById(superRootNode._id, new ActionLink { Type = LinkType.NeuralLink, LinkId = nodeSimpleChatMode._id });



            var nodeSound = await DbLinkCollection.InsertChildById(nodeSimpleChatMode._id, new NeuralLinkModel
            {
                Name = "Sound/Speaker",
                Title = $"[{BotResourceKeyConstants.SelectedIssue}]",
                Notes = new List<string> { $"[{BotResourceKeyConstants.WeHelpYou}]", $"[{BotResourceKeyConstants.WhatIssue}]" }
            });

            #region sound


            var resourceMute = await DbResourceCollection.InsertNew(new NeuralResourceModel
            {
                Name = "NoSound",
                IsLocal = false,
                Type = ResourceType.Video,
                Location = "https://www.youtube.com/watch?v=8Y8HzSBMujQ",
                Title = "How to fix sound problem on any android"
            });

            var actionMute = await DbActionCollection.InsertNew(new NeuralActionModel
            {
                Name = "NoSoundAction",
                Title = $"[{BotResourceKeyConstants.FoundSolution}]",
                Resources = new List<string> { $"{resourceMute._id}" }
            });

            var resourceUnpleasantSound = await DbResourceCollection.InsertNew(new NeuralResourceModel
            {
                Name = "UnpleasantSound0",
                IsLocal = false,
                Type = ResourceType.Video,
                Location = "https://www.youtube.com/watch?v=Y_hEEEt-Rb0",
                Title = "[Solution] Mobile speaker producing noisy (crackling) sound fixed without​ replacing speaker"
            });
            var resourceUnpleasantSound1 = await DbResourceCollection.InsertNew(new NeuralResourceModel
            {
                Name = "UnpleasantSound1",
                IsLocal = false,
                Type = ResourceType.ImageJPG,
                Location = "https://1.bp.blogspot.com/-74qhJFnbScQ/Xme-O_OkPnI/AAAAAAAADmw/v3iLjmTSBVYoRWH6HVenl5WYrddv2rd2QCLcBGAsYHQ/s320/Samsung%2BGT-E1207T%2BEar%2BSpeaker%2BJumpur%2BSolution.jpg",
                Title = "[Solution] Mobile speaker producing noisy (crackling) sound fixed without​ replacing speaker"
            });

            var actionUnpleasantSound = await DbActionCollection.InsertNew(new NeuralActionModel
            {
                Name = "UnpleasantSoundAction",
                Title = $"[{BotResourceKeyConstants.FoundSolution}]",
                Resources = new List<string> { resourceUnpleasantSound._id, resourceUnpleasantSound1._id }
            });

            var resourceCustomerSupport = await DbResourceCollection.InsertNew(new NeuralResourceModel
            {
                Name = "customersupport ",
                IsLocal = false,
                Type = ResourceType.WebsiteUrl,
                Location = "https://www.philips.co.in/c-w/support-home/support-contact-page.html",
                Title = "Please reach us @support site"
            });

            var actionCustomerSupport = await DbActionCollection.InsertNew(new NeuralActionModel
            {
                Name = "customersupportAction",
                Title = $"[{BotResourceKeyConstants.FoundSolution}]",
                Resources = new List<string> { resourceCustomerSupport._id }
            });

            var nodeNoSound = await DbLinkCollection.InsertChildById(nodeSound._id, new NeuralLinkModel
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
                        LinkId = actionMute._id
                    }
                }
            });

            var nodeUnpleasantSound = await DbLinkCollection.InsertChildById(nodeSound._id, new NeuralLinkModel
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
                        LinkId = actionUnpleasantSound._id
                    },
                    FallbackAction = new ActionLink
                    {
                        Type = LinkType.ActionLink,
                        LinkId = actionCustomerSupport._id
                    }
                }
            }); ;

            #endregion
            var nodeDisplay = await DbLinkCollection.InsertChildById(nodeSimpleChatMode._id, new NeuralLinkModel
            {
                Name = "Display/Broken screen",
                Title = $"[{BotResourceKeyConstants.SelectedIssue}]",
                Notes = new List<string> { $"[{BotResourceKeyConstants.WeHelpYou}]", $"[{BotResourceKeyConstants.WhatIssue}]" }
            });

            #region display

            #endregion
            var nodeBattery = await DbLinkCollection.InsertChildById(nodeSimpleChatMode._id, new NeuralLinkModel
            {
                Name = "Battery/Charging",
                Title = $"[{BotResourceKeyConstants.SelectedIssue}]",
                Notes = new List<string> { $"[{BotResourceKeyConstants.WeHelpYou}]", $"[{BotResourceKeyConstants.WhatIssue}]" }
            });

            //await DbLinkCollection.UnLinkParentChild(nodeSimpleChatMode._id, nodeBattery._id);

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

            await DbBotCollection.AddStringResourceBatchById(botId, stringRes);
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

            await DbTrainDataCollection.InsertNew(speakerTrainModel);

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

            await DbTrainDataCollection.InsertNew(displayTrainModel);

            #endregion
        }
    }
}
