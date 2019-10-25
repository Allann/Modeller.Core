using Hy.Modeller.Generator;
using Hy.Modeller.Generator.Outputs;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Loaders;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Hy.Modeller.CoreTests
{
    public static class LoggerUtils
    {
        public static Mock<ILogger<T>> LoggerMock<T>() where T : class
        {
            return new Mock<ILogger<T>>();
        }

        /// <summary>
        /// Returns an <pre>ILogger<T></pre> as used by the Microsoft.Logging framework.
        /// You can use this for constructors that require an ILogger parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ILogger<T> Logger<T>() where T : class
        {
            return LoggerMock<T>().Object;
        }

        public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel level, string message, string failMessage = null)
        {
            loggerMock.VerifyLog(level, message, Times.Once(), failMessage);
        }
        public static void VerifyLog<T>(this Mock<ILogger<T>> loggerMock, LogLevel level, string message, Times times, string failMessage = null)
        {
            loggerMock.Verify(l => l.Log<object>(level, It.IsAny<EventId>(), It.Is<object>(o => o.ToString().StartsWith(message)), null, It.IsAny<Func<object, Exception, string>>()), times, failMessage);
        }
    }

    public class FinalIntegrationFacts
    {
        //[Fact]
        //public void RunBuild()
        //{
        //    var fileWriter = new Mock<IFileWriter>().Object;
        //    var fileCreators = new List<IFileCreator> { new CreateFile(fileWriter), new CreateProject(fileWriter) };

        //    var packageLogger = LoggerUtils.LoggerMock<IPackageService>();
        //    var contextLogger = LoggerUtils.LoggerMock<IContext>();
        //    var builderLogger = LoggerUtils.LoggerMock<IBuilder>();
        //    var codeGenLogger = LoggerUtils.LoggerMock<ICodeGenerator>();

        //    var jsonSettings = new JsonSettingsLoader();
        //    var moduleLoader = new JsonModuleLoader();
        //    var generatorLoader = new GeneratorLoader();
        //    var packageLoader = new PackageFileLoader();
        //    var packageService = new PackageService(packageLoader, packageLogger.Object);
        //    var context = new Context(jsonSettings, moduleLoader, generatorLoader, packageService, contextLogger.Object);

        //    var codeGen = new CodeGenerator(codeGenLogger.Object);
        //    var output = new OutputStrategy(fileCreators);

        //    IBuilder _builder = new Builder(context, codeGen, output, builderLogger.Object);

        //    IGeneratorConfiguration config = new GeneratorConfiguration()
        //    {
        //        Verbose = true,
        //        Overwrite = false
        //    };

        //    config.GeneratorName = "Domain";
        //    config.OutputPath = "e:\\dev\\test\\FreightRate";
        //    config.SourceModel = "e:\\dev\\FreightRate2_model.json";

        //    _builder.Create(config);

        //    packageLogger.VerifyLog(LogLevel.Information, "Using Package");
        //    packageLogger.VerifyLog(LogLevel.Information, "Loaded");
        //    contextLogger.VerifyLog(LogLevel.Information, "Registered");
        //    builderLogger.VerifyLog(LogLevel.Information, "Generation started:");
        //    builderLogger.VerifyLog(LogLevel.Information, "Generation complete:");
        //    codeGenLogger.VerifyLog(LogLevel.Information, "Server Folder");

        //    codeGenLogger.VerifyLog(LogLevel.Error, "", Times.Never());
        //}
    }
}
