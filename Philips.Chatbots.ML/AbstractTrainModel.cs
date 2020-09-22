using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using Philips.Chatbots.ML.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
        protected long _dataCount = 0;
        protected MLContext _mlContext = new MLContext();
        protected IDataView _trainingDataView;
        protected ITransformer _trainedModel;

        protected bool _enableCache => _dataCount < 100000;
        public abstract string ModelOutputPath { get; }

        private void Save(string filePath)
        {
            _mlContext.Model.Save(_trainedModel, _trainingDataView.Schema, filePath);   //Save
        }

        private void BuildAndTrainModel()
        {
            var trainingPipeline = TransformAndBuildPipeline();
            _trainedModel = trainingPipeline.Fit(_trainingDataView);    //Train
        }

        public void BuildAndSaveModel(string outputPath = null)
        {
            var trainData = LoadData();
            _dataCount = trainData.Count;
            _trainingDataView = _mlContext.Data.LoadFromEnumerable<Input>(trainData);
            BuildAndTrainModel();
            Save(outputPath ?? ModelOutputPath);
        }

        public abstract List<Input> LoadData();
        public abstract EstimatorChain<KeyToValueMappingTransformer> TransformAndBuildPipeline();

    }
}
