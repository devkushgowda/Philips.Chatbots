using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Data.Models.Interfaces;
using Philips.Chatbots.Database.Common;
using Philips.Chatbots.Database.Extension;
using System.Linq;
using System.Threading.Tasks;
using Philips.Chatbots.Engine.Interfaces;
using Philips.Chatbots.Engine.Test;
using Philips.Chatbots.Engine.Session;
using Philips.Chatbots.Session;
using static Philips.Chatbots.Database.Common.DbAlias;
using Philips.Chatbots.Engine.Request.Extensions;
using Philips.Chatbots.Data.Models;
using System;
using System.Collections.Generic;

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

            switch (turnContext.Activity.Text.ToLower())
            {
                case "exit":
                    {
                        res = ResponseType.End;
                    }
                    break;
                case "back":
                    {
                        if (requestState.StepBack())
                            await TakeInputAction(turnContext, requestState);
                        else
                            await turnContext.SendActivityAsync(StringsProvider.TryGet(BotResourceKeyConstants.CannotMoveBack));
                    }
                    break;
                default:
                    {
                        await TakeInputAction(turnContext, requestState);
                    }
                    break;
            }
            return res;
        }
        private async Task TakeInputAction(ITurnContext turnContext, RequestState requestState)
        {
            var text = turnContext.Activity.Text;
            var curLink = requestState.CurrentLink;

            switch (requestState.CurrentState)
            {
                case ChatStateType.Start:
                    {

                        await SendReplyWithSuggestion(turnContext, requestState);
                    }
                    break;
                case ChatStateType.PickNode:
                    {
                        var nextLink = await DbLinkCollection.FindOneById(text);
                        if (nextLink == null)
                        {
                            await turnContext.SendActivityAsync("Invalid input, Please try again!");
                            await SendReplyWithSuggestion(turnContext, requestState);
                        }
                        else
                        {
                            curLink = nextLink;
                            requestState.StepForward(curLink);
                            await SendReplyWithSuggestion(turnContext, requestState);
                        }
                    }
                    break;
                case ChatStateType.End:
                    {

                    }
                    break;
                case ChatStateType.RecordFeedback:
                    {

                    }
                    break;
                case ChatStateType.InvalidInput:
                case ChatStateType.ExpInput:
                    {
                        ActionLink res;
                        var op = curLink.NeuralExp.Next(text, out res);
                        switch (op)
                        {
                            case ExpEvalResultType.False:
                            case ExpEvalResultType.True:
                                {
                                    switch (res.Type)
                                    {
                                        case LinkType.NeuralLink:
                                            break;
                                        case LinkType.ActionLink:
                                            {
                                                var action = await DbActionCollection.FindOneById(res.Id);
                                                (await action.BuildActionRespose(turnContext)).ForEach(item => turnContext.SendActivityAsync(item));
                                            }
                                            break;
                                        case LinkType.NeuralResource:
                                            break;
                                        default:
                                            break;
                                    }
                                }
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
                    }
                    break;
                default:
                    break;
            }

        }

        public async Task SendReplyWithSuggestion(ITurnContext turnContext, RequestState requestState)
        {
            var curLink = requestState.CurrentLink;
            Activity reply;
            if (curLink.NeuralExp != null && !curLink.NeuralExp.SkipEvaluation)
            {
                reply = turnContext.Activity.CreateReply(curLink.ApplyFormat(curLink.NeuralExp.QuestionTitle));
                reply.Type = ActivityTypes.Message;
                reply.TextFormat = TextFormatTypes.Plain;
                if (curLink.NeuralExp.Hint != null)
                    reply.SuggestedActions = new SuggestedActions()
                    {
                        Actions = curLink.NeuralExp.Hint.Split(",").Select(item =>
                        {
                            var keyValue = item.Split(":");
                            return new CardAction { Title = keyValue[0], Value = keyValue[1], Type = ActionTypes.ImBack };
                        }).ToList()
                    };
                await turnContext.SendActivityAsync(reply);
                requestState.CurrentState = ChatStateType.ExpInput;
            }
            else
            {
                if (curLink.Notes.Count == 0)
                {
                    reply = turnContext.Activity.CreateReply(curLink.ApplyFormat(curLink.Title));
                    reply.Type = ActivityTypes.Message;
                    reply.TextFormat = TextFormatTypes.Plain;
                    if (curLink.NeuralExp == null)
                    {
                        curLink.CildrenRank.Sort((x, y) => x.Value.CompareTo(y.Value));
                        reply.SuggestedActions = new SuggestedActions()
                        {
                            Actions = curLink.CildrenRank.Select(id =>
                                 {
                                     var name = DbLinkCollection.GetFieldValue(id.Key, x => x.Name).Result;
                                     return new CardAction { Title = name, Value = id.Key, Type = ActionTypes.PostBack };
                                 }).ToList()
                        };
                        await turnContext.SendActivityAsync(reply);
                    }
                }
                else
                {
                    await turnContext.SendActivityAsync(curLink.ApplyFormat(curLink.Title));
                    int count = 1;
                    foreach (var note in curLink.Notes)
                    {
                        if (count++ == curLink.Notes.Count)
                        {
                            reply = turnContext.Activity.CreateReply(curLink.ApplyFormat(note));
                            reply.Type = ActivityTypes.Message;
                            reply.TextFormat = TextFormatTypes.Plain;
                            if (curLink.NeuralExp == null)
                            {
                                curLink.CildrenRank.Sort((x, y) => x.Value.CompareTo(y.Value));
                                reply.SuggestedActions = new SuggestedActions()
                                {
                                    Actions = curLink.CildrenRank.Select(id =>
                                    {
                                        var name = DbLinkCollection.GetFieldValue(id.Key, x => x.Name).Result;
                                        return new CardAction { Title = name, Value = id.Key, Type = ActionTypes.PostBack };
                                    }).ToList()
                                };
                                await turnContext.SendActivityAsync(reply);
                            }
                            else
                            {
                                await turnContext.SendActivityAsync(curLink.ApplyFormat(note));
                            }
                        }
                        requestState.CurrentState = curLink.NeuralExp != null ? ChatStateType.ExpInput : ChatStateType.PickNode;

                    }

                    if (curLink.NeuralExp != null && curLink.NeuralExp.SkipEvaluation)
                    {
                        var res = curLink.NeuralExp.GetDefaultLink();
                        switch (res.Type)
                        {
                            case LinkType.NeuralLink:
                                break;
                            case LinkType.ActionLink:
                                {
                                    var action = await DbActionCollection.FindOneById(res.Id);
                                    (await action.BuildActionRespose(turnContext)).ForEach(item => turnContext.SendActivityAsync(item));
                                }
                                break;
                            case LinkType.NeuralResource:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

    }
}

