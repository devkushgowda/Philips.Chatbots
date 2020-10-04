using Philips.Chatbots.ML.Interfaces;

namespace Philips.Chatbots.ML.Models
{
    /// <summary>
    /// Neural prediction engine.
    /// </summary>
    public class NeuralPredictionEngine : AbstractPredictModel<NeuralTrainInput, PredictionOutput>
    {
        public static string ModelFilePath => NeuralTrainingEngine.ModelFilePath;

        private string _path;

        public NeuralPredictionEngine() : this(null, false)
        {

        }

        public NeuralPredictionEngine(string path = null, bool initilize = false)
        {
            _path = path ?? ModelFilePath;
            if (initilize)
                Initialize();
        }
        public override string ModelOutputPath => _path;
    }

}
