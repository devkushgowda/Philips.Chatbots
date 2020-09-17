namespace Philips.Chatbots.Data.Models.Interfaces
{
    /// <summary>
    /// Neural link interface.
    /// </summary>
    public interface ILinkInfo
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        string _id { get; set; }

        /// <summary>
        /// Is current link archived.
        /// </summary>
        bool IsArchived { get; set; }

        /// <summary>
        /// Is current link marked for deletion.
        /// </summary>
        bool IsMarkedForDeletion { get; set; }

        /// <summary>
        /// Name of the link, AKA short title.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Title of the link.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Used by ML questionnaire.
        /// </summary>
        string QuestionTitle { get; set; }

        /// <summary>
        /// Description for the link.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Serialize to string.
        /// </summary>
        /// <returns></returns>
        string toString();
    }
}
