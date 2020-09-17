namespace Philips.Chatbots.Data.Models.Interfaces
{
    /// <summary>
    /// Supported resource types.
    /// </summary>
    public enum ResourceType { Guide, Document, Image, Video, Website }

    /// <summary>
    /// Neual resource interface.
    /// </summary>
    public interface INeuralResourceModel
    {
        /// <summary>
        /// Type of the resource.
        /// </summary>
        ResourceType Type { get; set; }

        /// <summary>
        /// Local/Remote resource.
        /// </summary>
        bool IsLocal { get; set; }

        /// <summary>
        /// Path or url of the resource, depends on type of the resource.
        /// </summary>
        string Location { get; set; }
    }
}
