using Microsoft.ML;
using Philips.Chatbots.ML.Interfaces;

namespace Philips.Chatbots.ML
{
    /// <summary>
    /// Abstract implementation of ML Prediction model.
    /// </summary>
    /// <typeparam name="Input"></typeparam>
    /// <typeparam name="Output"></typeparam>
    public abstract class AbstractPredictModel<Input, Output> : IPredictModel<Input, Output>
        where Input : class, IMlData
        where Output : class, IMlData, new()
    {
        protected MLContext _mlContext = new MLContext();
        protected PredictionEngine<Input, Output> _predEngine = null;

        public abstract string ModelOutputPath { get; }

        public void Initialize(string modelPath = null)
        {
            ITransformer loadedModel = _mlContext.Model.Load(modelPath ?? ModelOutputPath, out var modelInputSchema);
            _predEngine = _mlContext.Model.CreatePredictionEngine<Input, Output>(loadedModel);
        }

        public Output Predict(Input input) => _predEngine?.Predict(input);
    }
}
