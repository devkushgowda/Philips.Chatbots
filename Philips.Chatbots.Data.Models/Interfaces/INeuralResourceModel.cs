namespace Philips.Chatbots.Data.Models.Interfaces
{
    /// <summary>
    /// Supported resource types.
    /// </summary>
    public enum ResourceType
    {
        DocumentPDF = 0,
        Text = 1,
        ImagePNG = 3,
        ImageJPG = 4,
        ImageGIF = 5,
        Video = 6,
        WebsiteUrl = 7,
        Audio = 8,
        Script = 9,
        Json = 10
    }

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
