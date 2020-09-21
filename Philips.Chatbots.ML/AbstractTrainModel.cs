using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using Philips.Chatbots.ML.Interfaces;
using System.Collections.Generic;

namespace Philips.Chatbots.ML
{
    /// <summary>
    /// Abstract implementation of ML train model.
    /// </summary>
    /// <typeparam name="Input"></typeparam>
    /// <typeparam name="Output"></typeparam>
    public abstract class AbstractTrainModel<Input, Output> : ITrainModel<Input, Output>
        where Input : class, IMlData
        where Output : class, IMlData
    {
        protected MLContext _mlContext = new MLContext();
        protected IDataView _trainingDataView;
        protected ITransformer _trainedModel;
        public abstract string ModelOutputPath { get; }

        private void Save(string filePath)
        {
            _mlContext.Model.Save(_trainedModel, _trainingDataView.Schema, filePath);   //Save
        }

        private void BuildAndTrainModel()
        {
            var trainingPipeline = TransformAndBuildPipeline();
            _trainedModel = trainingPipeline.Fit(_trainingDataView); //Train
        }

        public void BuildAndSaveModel(string outputPath = null)
        {
            var trainData = LoadData();
            _trainingDataView = _mlContext.Data.LoadFromEnumerable<Input>(trainData);
            BuildAndTrainModel();
            Save(outputPath ?? ModelOutputPath);
        }

        public abstract IEnumerable<Input> LoadData();
        public abstract EstimatorChain<KeyToValueMappingTransformer> TransformAndBuildPipeline();

    }
}
