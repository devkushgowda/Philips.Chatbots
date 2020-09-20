using Microsoft.Bot.Schema;
using Philips.Chatbots.Data.Models;
using Philips.Chatbots.Data.Models.Neural;
using Philips.Chatbots.Database.Common;
using Philips.Chatbots.Session;
using System.Collections.Generic;
using System.Linq;
using static Philips.Chatbots.Database.Common.DbAlias;

namespace Philips.Chatbots.Engine.Request.Extensions
{

    /// <summary>
    /// Extension class for response suggestions and actions.
    /// </summary>
    public static class SuggestionExtension
    {
        private static string FeedBackOptions => StringsProvider.TryGet(BotResourceKeyConstants.FeedBackOptions);
        private static string CommonActionOptions => StringsProvider.TryGet(BotResourceKeyConstants.CommonActionOptions);

        private static List<CardAction> ParseActionsFromColonFormatString(string input) => input?.Split(",").Select(item =>
        {
            var keyValue = item.Split(":");
            return new CardAction { Title = keyValue[0], Value = keyValue[1], Type = ActionTypes.ImBack };
        }).ToList();

        /// <summary>
        /// Get children nodes suggestion based on ranking.
        /// </summary>
        /// <param name="curLink"></param>
        /// <param name="appendCommonActions"></param>
        /// <returns></returns>
        public static SuggestedActions GetChildSuggestionActions(this NeuraLinkModel curLink, bool appendCommonActions = true)
        {
            curLink.CildrenRank.Sort((x, y) => y.Value.CompareTo(x.Value));

            var result = new SuggestedActions()
            {
                Actions = curLink.CildrenRank.Select(id =>
                {
                    var name = DbLinkCollection.GetFieldValue(id.Key, x => x.Name).Result;
                    return new CardAction { Title = name, Value = id.Key, Type = ActionTypes.PostBack };
                }).ToList()
            };

            if (appendCommonActions)
                result.AppendActions(ParseActionsFromColonFormatString(CommonActionOptions));

            return result;
        }

        /// <summary>
        /// Append actions for the suggestion item.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="cardActions"></param>
        /// <returns></returns>
        public static SuggestedActions AppendActions(this SuggestedActions action, List<CardAction> cardActions)
        {
            if (action.Actions == null)
                action.Actions = new List<CardAction>();

            cardActions.ForEach(item => action.Actions.Add(item));
            return action;
        }

        /// <summary>
        /// Get suggestion from Hint property.
        /// </summary>
        /// <param name="curLink"></param>
        /// <param name="appendCommonActions"></param>
        /// <returns></returns>
        public static SuggestedActions GetHintSuggestionActions(this NeuraLinkModel curLink, bool appendCommonActions = true)
        {
            var result = new SuggestedActions()
            {
                Actions = ParseActionsFromColonFormatString(curLink.NeuralExp.Hint)
            };

            if (appendCommonActions)
                result.AppendActions(ParseActionsFromColonFormatString(CommonActionOptions));

            return result;
        }

        /// <summary>
        /// Get feedback suggestion items.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static SuggestedActions GetFeedbackSuggestionActions(string input = null) => new SuggestedActions()
        {
            Actions = ParseActionsFromColonFormatString(input ?? FeedBackOptions)
        };
    }
}
