using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using MongoDB.Driver;
using Philips.Chatbots.ML.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using static Philips.Chatbots.Database.Common.DbAlias;

namespace Philips.Chatbots.ML.Models
{
    /// <summary>
    /// Neural training data model.
    /// </summary>
    public class NeuralTrainInput : IMlData
    {
        public string _id { set; get; }
        public string Text { set; get; }
    }

    /// <summary>
    /// Neural nodes training engine.
    /// </summary>
    public class NeuralTrainingEngine : AbstractTrainModel<NeuralTrainInput, PredictionOutput>
    {

        private const string dataFolder = "data";
        private static string dataFolderPath = Path.Combine(Environment.CurrentDirectory, dataFolder);
        private static string dbDataFolderPath => BotConfiguration().Result?.Configuration?.DataFolder;

        public static string ModelFilePath => Path.Combine(dbDataFolderPath ?? dataFolderPath, $"{DbLinkCollection.CollectionNamespace}.zip");

        public override string ModelOutputPath => ModelFilePath;

        /// <summary>
        /// Load data from DB.
        /// </summary>
        /// <returns></returns>
        public override List<NeuralTrainInput> LoadData()
        {
            List<NeuralTrainInput> result = new List<NeuralTrainInput>();
            DbTrainDataCollection.Find(exp => true).ToList()
                .ForEach(trainData => trainData.Dataset
                .ForEach(text => result.Add(new NeuralTrainInput { _id = trainData._id, Text = text })
                ));
            return result;
        }

        /// <summary>
        /// Transform and build train pipeline.
        /// </summary>
        /// <returns></returns>
        public override EstimatorChain<KeyToValueMappingTransformer> TransformAndBuildPipeline()
        {
            var fText = $"{nameof(NeuralTrainInput.Text)}Featurized";
            var transformedData = _mlContext.Transforms.Text.FeaturizeText(inputColumnName: nameof(NeuralTrainInput.Text), outputColumnName: fText)
                .Append(_mlContext.Transforms.Concatenate("Features", fText));


            if (_enableCache)
                transformedData.AppendCacheCheckpoint(_mlContext);   //Remove for large datasets.

            var processedData = _mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: nameof(NeuralTrainInput._id), outputColumnName: "Label")
                .Append(transformedData);

            var result = processedData.Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
         .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));  //Build pipeline

            return result;
        }
    }

}
