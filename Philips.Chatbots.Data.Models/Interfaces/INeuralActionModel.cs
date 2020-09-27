using System.Collections.Generic;

namespace Philips.Chatbots.Data.Models.Interfaces
{
    /// <summary>
    /// Neural action type.
    /// </summary>
    public enum ActionType
    {
        Guide = 0,
        ChatSupport = 1,
        CallSupport = 2,
        Script = 3,
        Service = 4,
        Others = 5
    }

    /// <summary>
    /// Neural action model interface.
    /// </summary>
    public interface INeuralActionModel
    {
        /// <summary>
        /// Collection of resource id's.
        /// </summary>
        List<string> Resources { get; set; }

        /// <summary>
        /// Action type.
        /// </summary>
        ActionType Type { get; set; }
    }
}
