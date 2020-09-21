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
    /// Neural train model.
    /// </summary>
    public class NeuralTrainInput : IMlData
    {
        public string _id { set; get; }
        public string Text { set; get; }
    }

    /// <summary>
    /// Neural train engine.
    /// </summary>
    public class NeuralTrainEngine : AbstractTrainModel<NeuralTrainInput, PredictionOutput>
    {
        private const string dataFolder = "data";
        private static string dataFolderPath = Path.Combine(Environment.CurrentDirectory, dataFolder);

        public static string ModelFilePath = Path.Combine(dataFolderPath, $"{nameof(NeuralTrainEngine).ToLower()}.zip");

        public override string ModelOutputPath => ModelFilePath;

        /// <summary>
        /// Load data from DB.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<NeuralTrainInput> LoadData()
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
            var processedData = _mlContext.Transforms.Conversion.MapValueToKey(inputColumnName: nameof(NeuralTrainInput._id), outputColumnName: "Label")
                .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: nameof(NeuralTrainInput.Text), outputColumnName: fText)
                .Append(_mlContext.Transforms.Concatenate("Features", fText))
                .AppendCacheCheckpoint(_mlContext)   //Remove for large datasets.
                );

            var result = processedData.Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
         .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));  //Build pipeline

            return result;
        }
    }

}
