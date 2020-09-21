using Microsoft.ML.Data;

namespace Philips.Chatbots.ML.Interfaces
{
    /// <summary>
    /// ML model prediction output result.
    /// </summary>
    public class PredictionOutput : IMlData
    {
        [ColumnName("PredictedLabel")]
        public string _id { set; get; }
    }

    /// <summary>
    /// ML model both input and output interface.
    /// </summary>
    public interface IMlData
    {
        public string _id { get; set; }
    }
}
