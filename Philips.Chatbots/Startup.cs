using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
using Philips.Chatbots.Bots;
using Philips.Chatbots.Database.MongoDB;
using Philips.Chatbots.ML.Interfaces;
using Philips.Chatbots.ML.Models;
using System;
using System.IO;

//[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]

namespace Philips.Chatbots
{
    /// <summary>
    /// Startup service class.
    /// </summary>
    public class Startup
    {
        public const string LogConfigFile = "log4net.config";

        public const string AppSettingsFile = "appsettings.json";

        IWebHostEnvironment webHostEnv;

        public Startup(IWebHostEnvironment webHostEnv)
        {
            this.webHostEnv = webHostEnv;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(webHostEnv.ContentRootPath)
                .AddJsonFile(AppSettingsFile, true, true)
                .AddEnvironmentVariables().Build();
            services.AddSingleton(configuration);
            services.AddBot<BotAlpha>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(configuration);
            });
            services.AddSingleton(ConfigureLog4Net());

            services.AddPredictionEnginePool<NeuralTrainInput, PredictionOutput>()
                .FromFile(modelName: nameof(NeuralPredictionEngine), filePath: NeuralPredictionEngine.ModelFilePath, watchForChanges: true);

            MongoDbProvider.Connect();
        }

        private ILoggerFactory ConfigureLog4Net(string logConfigFileName = LogConfigFile)
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            var logFolderPath = webHostEnv.ContentRootPath;
            log4net.GlobalContext.Properties["LogFolderPath"] = logFolderPath; //log folder path
            loggerFactory.AddLog4Net(Path.Combine(logFolderPath, logConfigFileName), true);
            return loggerFactory;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.UseBotFramework();
        }
    }
}
