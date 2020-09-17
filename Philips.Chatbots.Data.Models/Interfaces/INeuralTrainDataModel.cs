using System.Collections.Generic;

namespace Philips.Chatbots.Data.Models.Interfaces
{
    /// <summary>
    /// Neural train data model interface.
    /// </summary>
    public interface INeuralTrainDataModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        string _id { get; set; }

        /// <summary>
        /// Is current train model archived.
        /// </summary>
        bool IsArchived { get; set; }

        /// <summary>
        /// Training data set.
        /// </summary>
        List<string> Dataset { get; set; }
    }
}
