
namespace Philips.Chatbots.ML.Interfaces
{
    /// <summary>
    /// Train engine interface.
    /// </summary>
    /// <typeparam name="Input"></typeparam>
    /// <typeparam name="Output"></typeparam>
    public interface ITrainModel<Input, Output>
        where Input : IMlData
        where Output : IMlData
    {
        /// <summary>
        /// Build and save the model.
        /// Checkout the reference from https://docs.microsoft.com/en-us/dotnet/machine-learning/tutorials/github-issue-classification
        /// 
        /// </summary>
        /// <param name="outputPath"></param>
        public void BuildAndSaveModel(string outputPath);
    }
}
