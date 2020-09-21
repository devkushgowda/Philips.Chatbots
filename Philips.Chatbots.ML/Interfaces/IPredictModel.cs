
namespace Philips.Chatbots.ML.Interfaces
{
    /// <summary>
    /// Prediction model interface.
    /// </summary>
    /// <typeparam name="Input"></typeparam>
    /// <typeparam name="Output"></typeparam>
    public interface IPredictModel<Input, Output>
        where Input : IMlData
        where Output : IMlData
    {
        /// <summary>
        /// Predict function to evaluate input.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Output Predict(Input input);

        /// <summary>
        /// Initilize prediction engine.
        /// </summary>
        /// <param name="modelPath"></param>
        void Initilize(string modelPath);

    }
}
