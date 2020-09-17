using Microsoft.Bot.Builder;
using Philips.Chatbots.Engine.Session;
using System.Threading.Tasks;

namespace Philips.Chatbots.Engine.Interfaces
{
    /// <summary>
    /// Responses from the request handler.
    /// </summary>
    public enum ResponseType { End, Error, Continue }

    /// <summary>
    /// Request handler interface.
    /// Used by request pipeline.
    /// </summary>
    public interface IRequestHandler
    {
        Task<ResponseType> Execute(ITurnContext turnContext, RequestState requestState);
    }
}