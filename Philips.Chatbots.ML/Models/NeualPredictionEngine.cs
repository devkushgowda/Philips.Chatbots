using Philips.Chatbots.ML.Interfaces;

namespace Philips.Chatbots.ML.Models
{
    /// <summary>
    /// Neural prediction engine.
    /// </summary>
    public class NeualPredictionEngine : AbstractPredictModel<NeuralTrainInput, PredictionOutput>
    {
        private string _path;

        public NeualPredictionEngine() : this(NeuralTrainEngine.ModelFilePath)
        {

        }

        public NeualPredictionEngine(string path = null, bool initilize = false)
        {
            _path = path ?? NeuralTrainEngine.ModelFilePath;
            if (initilize)
                Initilize();
        }
        public override string ModelOutputPath => _path;
    }

}
