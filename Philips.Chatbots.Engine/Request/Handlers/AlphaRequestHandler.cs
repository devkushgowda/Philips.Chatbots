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

            switch (turnContext.Activity.Text)
            {
                case "create":
                    await BotDbTestClass.Feed(requestState.BotId);
                    break;
                case "exit":
                    {
                        res = ResponseType.End;
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
            var link = requestState.CurrentLink;

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
                            requestState.CurrentLink = link = nextLink;
                            await SendReplyWithSuggestion(turnContext, requestState);
                        }
                    }
                    break;
                case ChatStateType.End:
                    {

                    }
                    break;
                case ChatStateType.InvalidInput:
                    {

                    }
                    break;
                case ChatStateType.RecordFeedback:
                    {

                    }
                    break;
                case ChatStateType.SolutionFound:
                    {

                    }
                    break;
                case ChatStateType.ExpInput:
                    {
                        ActionLink res;
                        var op = link.NeuralExp.Next(text, out res);
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
                                                if (action != null)
                                                {
                                                    await turnContext.SendActivityAsync(action.ApplyFormat(action.Title));
                                                    foreach (var rId in action.Resources)
                                                    {
                                                        var resource = await DbResourceCollection.FindOneById(rId);
                                                        await turnContext.SendActivityAsync($"{resource.ApplyFormat(resource.Title)}\n {resource.Location} ");
                                                    }
                                                }
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
                            case ExpEvalResultType.Empty:
                            default:
                                {
                                    await turnContext.SendActivityAsync("Invalid input, Try again!");
                                }
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
            var link = requestState.CurrentLink;
            Activity reply;
            if (link.NeuralExp != null && !link.NeuralExp.SkipEvaluation)
            {
                reply = turnContext.Activity.CreateReply(link.ApplyFormat(link.NeuralExp.QuestionTitle));
                reply.Type = ActivityTypes.Message;
                reply.TextFormat = TextFormatTypes.Plain;
                if (link.NeuralExp.Hint != null)
                    reply.SuggestedActions = new SuggestedActions()
                    {
                        Actions = link.NeuralExp.Hint.Split(",").Select(item =>
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
                if (link.Notes.Count == 0)
                {
                    reply = turnContext.Activity.CreateReply(link.ApplyFormat(link.Title));
                    reply.Type = ActivityTypes.Message;
                    reply.TextFormat = TextFormatTypes.Plain;
                    if (link.NeuralExp == null)
                        reply.SuggestedActions = new SuggestedActions()
                        {
                            Actions = link.Children.Select(id =>
                            {
                                var name = DbLinkCollection.GetFieldValue(id, x => x.Name).Result;
                                return new CardAction { Title = name, Value = id, Type = ActionTypes.ImBack };
                            }).ToList()
                        };
                    await turnContext.SendActivityAsync(reply);
                }
                else
                {
                    await turnContext.SendActivityAsync(link.ApplyFormat(link.Title));
                    int count = 1;
                    foreach (var note in link.Notes)
                    {
                        if (count++ == link.Notes.Count)
                        {
                            reply = turnContext.Activity.CreateReply(link.ApplyFormat(note));
                            reply.Type = ActivityTypes.Message;
                            reply.TextFormat = TextFormatTypes.Plain;
                            if (link.NeuralExp == null)
                                reply.SuggestedActions = new SuggestedActions()
                                {
                                    Actions = link.Children.Select(id =>
                                    {
                                        var name = DbLinkCollection.GetFieldValue(id, x => x.Name).Result;
                                        return new CardAction { Title = name, Value = id, Type = ActionTypes.ImBack };
                                    }).ToList()
                                };
                            await turnContext.SendActivityAsync(reply);
                        }
                        else
                        {
                            await turnContext.SendActivityAsync(link.ApplyFormat(note));
                        }
                    }
                    requestState.CurrentState = link.NeuralExp != null ? ChatStateType.ExpInput : ChatStateType.PickNode;

                }

                if (link.NeuralExp != null && link.NeuralExp.SkipEvaluation)
                {
                    var res = link.NeuralExp.GetDefaultLink();
                    switch (res.Type)
                    {
                        case LinkType.NeuralLink:
                            break;
                        case LinkType.ActionLink:
                            {
                                var action = await DbActionCollection.FindOneById(res.Id);
                                if (action != null)
                                {
                                    await turnContext.SendActivityAsync(action.ApplyFormat(action.Title));
                                    foreach (var rId in action.Resources)
                                    {
                                        var resource = await DbResourceCollection.FindOneById(rId);
                                        await turnContext.SendActivityAsync($"{resource.ApplyFormat(resource.Title)}\n {resource.Location} ");
                                    }
                                }
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

