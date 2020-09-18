using System.Collections.Generic;

namespace Philips.Chatbots.Data.Models.Interfaces
{
    /// <summary>
    /// Neural link model interface.
    /// </summary>
    public interface INeuraLinkModel
    {
        /// <summary>
        /// Neural expression.
        /// </summary>
        public INeuralExpression NeuralExp { get; set; }

        /// <summary>
        /// Key notes to be sent individually.
        /// </summary>
        List<string> Notes { get; set; }

        /// <summary>
        /// _id's of the parent links, used when deleting the node.
        /// </summary>
        List<string> Parents { get; set; }

        /// <summary>
        /// Child link and their rank.
        /// </summary>
        List<KeyValuePair<string, long>> CildrenRank { get; set; }

        /// <summary>
        /// Labels for this link, used for searching and categorizing purposes.
        /// </summary>
        List<string> Labels { get; set; }
    }
}
