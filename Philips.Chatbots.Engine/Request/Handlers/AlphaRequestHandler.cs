using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Database.Extension;
using System.Threading.Tasks;
using Philips.Chatbots.Engine.Interfaces;
using Philips.Chatbots.Engine.Session;
using Philips.Chatbots.Session;
using Philips.Chatbots.Engine.Request.Extensions;
using Philips.Chatbots.Data.Models;
using static Philips.Chatbots.Database.Common.DbAlias;
using System.Linq;
using System;

namespace Philips.Chatbots.Engine.Requst.Handlers
{
    /// <summary>
    /// Simple request handler for Bot Alpha
    /// </summary>
    public class AlphaRequestHandler : IRequestHandler
    {
        public async Task<ResponseType> Execute(ITurnContext turnContext, RequestState requestState)
        {
            var res = ResponseType.Continue;

            switch (turnContext.Activity.Text?.ToLower())
            {
                case BotResourceKeyConstants.CommandExit:
                    {

                        res = ResponseType.End;
                    }
                    break;
                case BotResourceKeyConstants.CommandBack:
                    {
                        if (!requestState.StepBack())
                            await turnContext.SendActivityAsync(StringsProvider.TryGet(BotResourceKeyConstants.CannotMoveBack));

                        res = await TakeAction(turnContext, requestState);
                    }
                    break;
                case BotResourceKeyConstants.CommandAdvanceChat:
                    {
                        requestState.CurrentState = ChatStateType.AdvanceChat;
                        await turnContext.SendActivityAsync(StringsProvider.TryGet(BotResourceKeyConstants.AdvanceChatQuery));

                    }
                    break;
                default:
                    {
                        res = await TakeAction(turnContext, requestState);
                    }
                    break;
            }
            return res;
        }
        private async Task<ResponseType> TakeAction(ITurnContext turnContext, RequestState requestState)
        {
            var ret = ResponseType.Continue;
            var text = turnContext.Activity.Text;
            var curLink = requestState.CurrentLink;

            switch (requestState.CurrentState)
            {
                case ChatStateType.Start:
                    {

                        await SendResponseForCurrentNode(turnContext, requestState);
                    }
                    break;
                case ChatStateType.AdvanceChat:
                    {
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            await SendReply(turnContext, StringsProvider.TryGet(BotResourceKeyConstants.InvalidInput), SuggestionExtension.GetCommonSuggestionActions());
                        }
                        else
                        {
                            string nextNodeId = null;
                            try
                            {
                                nextNodeId = requestState.PredictNode(text);
                                var nextLink = await DbLinkCollection.FindOneById(nextNodeId);
                                if (nextLink != null)
                                {
                                    requestState.StepForward(nextLink);
                                    await SendResponseForCurrentNode(turnContext, requestState);
                                }
                                else
                                {
                                    await SendReply(turnContext, StringsProvider.TryGet(BotResourceKeyConstants.NoMatchFound), SuggestionExtension.GetCommonSuggestionActions());
                                }
                            }
                            catch (Exception e)
                            {
                                await SendReply(turnContext, $"ML prediction returned an error: '{e.Message}'. please report this issue to the bot administrator.", SuggestionExtension.GetCommonSuggestionActions("Exit:exit"));
                            }
                        }
                    }
                    break;
                case ChatStateType.PickNode:
                    {
                        var nextLink = await DbLinkCollection.FindOneById(text);
                        if (nextLink == null)
                        {
                            await turnContext.SendActivityAsync(StringsProvider.TryGet(BotResourceKeyConstants.InvalidInput));
                            await SendResponseForCurrentNode(turnContext, requestState);
                        }
                        else
                        {
                            curLink = nextLink;
                            requestState.StepForward(curLink);
                            await SendResponseForCurrentNode(turnContext, requestState);
                        }
                    }
                    break;
                case ChatStateType.RecordFeedback:
                    {
                        switch (text.ToLower())
                        {
                            case "yes":
                                {
                                    var childLink = curLink;
                                    NeuralLinkModel parentLink = null;
                                    while (requestState.LinkHistory.TryPop(out parentLink))
                                    {
                                        await DbLinkCollection.UpdateNeuralRankById(parentLink._id, childLink._id);
                                        childLink = parentLink;
                                    }
                                    ret = ResponseType.End;
                                }
                                break;
                            case "no":
                                ret = ResponseType.End;
                                break;
                            default:
                                await turnContext.SendActivityAsync(StringsProvider.TryGet(BotResourceKeyConstants.InvalidInput));
                                await SendReply(turnContext, StringsProvider.TryGet(BotResourceKeyConstants.Feedback), SuggestionExtension.GetFeedbackSuggestionActions());
                                break;
                        }
                        if (ret == ResponseType.End)
                            await turnContext.SendActivityAsync(StringsProvider.TryGet(BotResourceKeyConstants.ThankYouFeedback));
                    }
                    break;
                case ChatStateType.InvalidInput:
                case ChatStateType.ExpInput:
                    {
                        await EvaluateExpressionInput(turnContext, requestState);
                    }
                    break;
                default:
                    break;
            }
            return ret;

        }
        private async Task<bool> EvaluateExpressionInput(ITurnContext turnContext, RequestState requestState)
        {
            var res = false;
            var text = turnContext.Activity.Text;
            var curLink = requestState.CurrentLink;
            ActionLink actionResult;
            ExpEvalResultType op = curLink.NeuralExp.Next(text, out actionResult);
            switch (op)
            {
                case ExpEvalResultType.Skipped:
                case ExpEvalResultType.False:
                case ExpEvalResultType.True:
                    {
                        if (op == ExpEvalResultType.Skipped)
                            await SendNotes(curLink, turnContext);
                        switch (actionResult.Type)
                        {
                            case LinkType.NeuralLink:
                                {
                                    var link = await DbLinkCollection.FindOneById(actionResult.LinkId);
                                    if (link != null)
                                    {
                                        curLink = link;
                                        requestState.StepForward(curLink);
                                        await SendResponseForCurrentNode(turnContext, requestState);
                                    }
                                    else
                                    {
                                        //Invalid expression evaluation link id.
                                        await SendReply(turnContext, $"Invalid node information : '{actionResult.LinkId}',please report this issue to the bot administrator.", SuggestionExtension.GetCommonSuggestionActions());
                                    }

                                }
                                break;
                            case LinkType.ActionLink:
                                {
                                    var action = await DbActionCollection.FindOneById(actionResult.LinkId);
                                    await turnContext.SendActivityAsync(action.BuildActionRespose(turnContext));
                                    requestState.CurrentState = ChatStateType.RecordFeedback;
                                }
                                break;
                            case LinkType.NeuralResource:
                                {
                                    var resource = await DbResourceCollection.FindOneById(actionResult.LinkId);
                                    await turnContext.SendActivityAsync(resource.BuildResourceResponse(turnContext));
                                    requestState.CurrentState = ChatStateType.RecordFeedback;
                                }
                                break;
                            default:
                                break;
                        }
                        if (requestState.CurrentState == ChatStateType.RecordFeedback)
                            await Task.Delay(1000).ContinueWith(async ct =>
                            {
                                await SendReply(turnContext, StringsProvider.TryGet(BotResourceKeyConstants.Feedback), SuggestionExtension.GetFeedbackSuggestionActions());
                            });
                        res = true;
                    }
                    break;
                case ExpEvalResultType.Exception:
                case ExpEvalResultType.Invalid:
                    {
                        await SendReply(turnContext, "Invalid input, please try again!", SuggestionExtension.GetCommonSuggestionActions("Exit:exit"));
                        requestState.CurrentState = ChatStateType.InvalidInput;
                    }
                    break;
                case ExpEvalResultType.Empty://TODO
                    {
                        await SendReply(turnContext, $"Evaluated empty expression of node : '{curLink._id}',please report this issue to the bot administrator.", SuggestionExtension.GetCommonSuggestionActions());
                    }
                    break;
                default:

                    break;
            }
            return res;
        }

        private async Task SendNotes(NeuralLinkModel curLink, ITurnContext turnContext)
        {
            foreach (var note in curLink.Notes)
                await turnContext.SendActivityAsync(curLink.ApplyFormat(note));
        }

        private async Task SendReply(ITurnContext turnContext, string message, SuggestedActions suggestedActions = null)
        {
            var reply = turnContext.Activity.CreateReply(message);
            reply.Type = ActivityTypes.Message;
            reply.TextFormat = TextFormatTypes.Plain;
            reply.SuggestedActions = suggestedActions;
            await turnContext.SendActivityAsync(reply);
        }

        private async Task<bool> EvaluateExpression(ITurnContext turnContext, RequestState requestState)
        {
            var res = false;
            var curLink = requestState.CurrentLink;

            if (curLink?.NeuralExp != null)
            {
                if (curLink.NeuralExp.SkipEvaluation)
                {
                    res = await EvaluateExpressionInput(turnContext, requestState);//Default expression
                }
                else
                {
                    await SendNotes(curLink, turnContext);

                    var title = curLink.NeuralExp.QuestionTitle;
                    SuggestedActions suggestedActions = null;

                    if (curLink.NeuralExp.Hint != null && curLink.NeuralExp.Hint.Contains(":"))
                        suggestedActions = curLink.GetHintSuggestionActions(false);

                    if (curLink.NeuralExp is LinkExpression)
                    {
                        var card = new HeroCard
                        {
                            Text = title,
                            Buttons = (curLink.NeuralExp as LinkExpression).ActionItems.Select(action => new CardAction(ActionTypes.PostBack, title: action.Title, value: action.Value)).ToList()
                        };
                        var reply = MessageFactory.Attachment(card.ToAttachment());
                        reply.SuggestedActions = suggestedActions;
                        await turnContext.SendActivityAsync(reply);
                    }
                    else
                    {
                        await SendReply(turnContext, curLink.ApplyFormat(title), suggestedActions);
                    }

                    requestState.CurrentState = ChatStateType.ExpInput;
                    res = true;
                }
            }
            return res;
        }
        private async Task SendResponseForCurrentNode(ITurnContext turnContext, RequestState requestState)
        {
            var curLink = requestState.CurrentLink;

            var isExpression = await EvaluateExpression(turnContext, requestState);

            if (!isExpression)
            {
                if (curLink.Notes?.Count == 0)
                {
                    await SendReply(turnContext, curLink.ApplyFormat(curLink.Title), curLink.GetChildSuggestionActions());
                }
                else
                {
                    await turnContext.SendActivityAsync(curLink.ApplyFormat(curLink.Title));
                    int count = 1;
                    foreach (var note in curLink.Notes)
                    {
                        if (count++ == curLink.Notes.Count) //Last note
                        {
                            await SendReply(turnContext, curLink.ApplyFormat(note), curLink.GetChildSuggestionActions());
                        }
                        else
                        {
                            await turnContext.SendActivityAsync(curLink.ApplyFormat(note));
                        }
                    }
                }
                requestState.CurrentState = ChatStateType.PickNode;
            }
        }

    }

}



