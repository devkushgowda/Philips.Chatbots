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
                case "exit":
                    {
                        res = ResponseType.End;
                    }
                    break;
                case "back":
                    {
                        if (!requestState.StepBack())
                            await turnContext.SendActivityAsync(StringsProvider.TryGet(BotResourceKeyConstants.CannotMoveBack));

                        res = await TakeAction(turnContext, requestState);
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
                                    NeuraLinkModel parentLink = null;
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
                        switch (actionResult.Type)
                        {
                            case LinkType.NeuralLink:
                                {
                                    var link = await DbLinkCollection.FindOneById(actionResult.Id);
                                    if (link != null)
                                    {
                                        curLink = link;
                                        requestState.StepForward(curLink);
                                        await SendResponseForCurrentNode(turnContext, requestState);
                                    }
                                    else
                                    {
                                        //Invalid expression evaluation link id.
                                    }

                                }
                                break;
                            case LinkType.ActionLink:
                                {
                                    var action = await DbActionCollection.FindOneById(actionResult.Id);
                                    (await action.BuildActionRespose(turnContext)).ForEach(item => turnContext.SendActivityAsync(item));
                                    var reply = turnContext.Activity.CreateReply(StringsProvider.TryGet(BotResourceKeyConstants.Feedback));
                                    reply.SuggestedActions = SuggestionExtension.GetFeedbackSuggestionActions();
                                    await turnContext.SendActivityAsync(reply);
                                    requestState.CurrentState = ChatStateType.RecordFeedback;
                                }
                                break;
                            case LinkType.NeuralResource:
                                break;
                            default:
                                break;
                        }
                    }
                    res = true;
                    break;
                case ExpEvalResultType.Exception:
                case ExpEvalResultType.Invalid:
                    {
                        await turnContext.SendActivityAsync("Invalid input, Try again!");
                        requestState.CurrentState = ChatStateType.InvalidInput;
                    }
                    break;
                case ExpEvalResultType.Empty://TODO
                default:

                    break;
            }
            return res;
        }
        private async Task<bool> EvaluateExpression(ITurnContext turnContext, RequestState requestState)
        {
            var res = false;
            var curLink = requestState.CurrentLink;
            Activity reply;
            if (curLink?.NeuralExp != null)
            {
                if (curLink.NeuralExp.SkipEvaluation)
                {
                    await EvaluateExpressionInput(turnContext, requestState);//Default expression
                }
                else if (curLink.NeuralExp.Hint != null && curLink.NeuralExp.Hint.Contains(":"))
                {
                    var title = curLink.NeuralExp.QuestionTitle;
                    reply = turnContext.Activity.CreateReply(curLink.ApplyFormat(title));
                    reply.Type = ActivityTypes.Message;
                    reply.TextFormat = TextFormatTypes.Plain;
                    reply.SuggestedActions = curLink.GetHintSuggestionActions();
                    await turnContext.SendActivityAsync(reply);
                    requestState.CurrentState = ChatStateType.ExpInput;
                }
                res = true;
            }
            return res;
        }
        private async Task SendResponseForCurrentNode(ITurnContext turnContext, RequestState requestState)
        {
            var curLink = requestState.CurrentLink;
            Activity reply;

            var isExpression = await EvaluateExpression(turnContext, requestState);

            if (!isExpression)
            {
                if (curLink.Notes?.Count == 0)
                {
                    reply = turnContext.Activity.CreateReply(curLink.ApplyFormat(curLink.Title));
                    reply.Type = ActivityTypes.Message;
                    reply.TextFormat = TextFormatTypes.Plain;
                    if (curLink.NeuralExp == null)
                    {
                        reply.SuggestedActions = curLink.GetChildSuggestionActions();
                        await turnContext.SendActivityAsync(reply);
                    }
                }
                else
                {
                    await turnContext.SendActivityAsync(curLink.ApplyFormat(curLink.Title));
                    int count = 1;
                    foreach (var note in curLink.Notes)
                    {
                        if (count++ == curLink.Notes.Count) //Last note
                        {
                            reply = turnContext.Activity.CreateReply(curLink.ApplyFormat(note));
                            reply.Type = ActivityTypes.Message;
                            reply.TextFormat = TextFormatTypes.Plain;
                            reply.SuggestedActions = curLink.GetChildSuggestionActions();
                            await turnContext.SendActivityAsync(reply);
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



