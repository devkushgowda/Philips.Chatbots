using System.Collections.Generic;

namespace Philips.Chatbots.Data.Models.Interfaces
{
    /// <summary>
    /// Neural action type.
    /// </summary>
    public enum NeuraActionType
    {
        Support = 0,
        Script = 1,
        Others = 2
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
        NeuraActionType Type { get; set; }
    }
}
