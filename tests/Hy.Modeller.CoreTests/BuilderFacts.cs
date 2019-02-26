using FluentAssertions;
using Hy.Modeller.Core.Outputs;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Outputs;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hy.Modeller.CoreTests
{
    public class XunitLogger<T> : ILogger<T>, IDisposable
    {
        private StringBuilder _output;

        public XunitLogger(StringBuilder output)
        {
            _output = output;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _output.AppendLine(state.ToString());
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        public void Dispose()
        {
        }
    }

//    public class PresenterFacts
//    {
//        [Fact]
//        public void Presentor_Display_CanListAllGenerators()
//        {
//            var list = new List<GeneratorItem>();

//            var sb = new StringBuilder();
//            var config = new Mock<IGeneratorConfiguration>();
//            config.SetupGet(c => c.LocalFolder).Returns("D:\\Files\\Generators");
//            config.SetupGet(c => c.Target).Returns("netstandard2.0");
//            config.SetupGet(c => c.Verbose).Returns(true);
//            var logger = new XunitLogger<IPresenter>(sb);
//            var loader = new Mock<IGeneratorLoader>();
//            loader.Setup(c => c.TryLoad("D:\\Files\\Generators\\netstandard2.0", out var list)).Returns(true);

//            var presenter = new Presenter(config.Object, loader.Object, logger);
//            presenter.Display();

//            sb.ToString().Should().Be(@"Available generators
//  location: D:\Files\Generators\netstandard2.0


//");
//        }
//    }

    public class BuilderFacts
    {
        [Fact]
        public void Builder_Allows_Create()
        {
            var op = System.IO.Path.GetTempPath();

            var snippet = new Mock<ISnippet>();

            var context = new Mock<IContext>();
            context.Setup(c => c.GeneratorConfiguration.OutputPath).Returns(op);
            var codeGenerator = new Mock<ICodeGenerator>();
            codeGenerator.Setup(c => c.Create(It.IsAny<IContext>())).Returns(snippet.Object);
            var outputStrategy = new Mock<IOutputStrategy>();
            var logger = new Mock<ILogger<IBuilder>>();

            var builder = new Builder(context.Object, codeGenerator.Object, outputStrategy.Object, logger.Object);
            builder.Create();

            codeGenerator.Verify(c => c.Create(It.IsAny<IContext>()), Times.Once);
            outputStrategy.Verify(c => c.Create(It.IsAny<IOutput>(), It.IsAny<IGeneratorConfiguration>()), Times.Once);
        }

        //[Fact]
        //public void Builder_Real_Test()
        //{
        //    var config = new GeneratorConfiguration();
        //    config.GeneratorName = "MVCSolution";
        //    config.LocalFolder = "F:\\Repos\\Modeller.Generators.git\\Generators";
        //    config.OutputPath = "F:\\dev\\test\\modeller";
        //    config.SourceModel = "F:\\dev\\modeller_model.json";

        //    var logger = new Mock<ILogger<IPackageService>>();
        //    var settingLoader = new JsonSettingsLoader();
        //    var moduleLoader = new JsonModuleLoader();
        //    var generatorLoader = new GeneratorLoader();
        //    var packageLoader = new PackageFileLoader();
        //    var packageService = new PackageService(packageLoader, logger.Object);

        //    var context = new Context(config, settingLoader, moduleLoader, generatorLoader, packageService);

        //    var loggerCG = new Mock<ILogger<ICodeGenerator>>();
        //    var loggerFW = new Mock<ILogger<FileWriter>>();
        //    var loggerB = new Mock<ILogger<IBuilder>>();
        //    var codeGenerator = new CodeGenerator(loggerCG.Object);
            
        //    var fileWriter = new FileWriter(loggerFW.Object);
        //    var fc1 = new CreateFile(fileWriter);
        //    var fc2 = new CreateFileGroup(fileWriter);
        //    var fc3 = new CreateProject(fileWriter);
        //    var fc4 = new CreateSnippet(fileWriter);
        //    var fc5 = new CreateSolution(fileWriter);
        //    var list = new List<IFileCreator> { fc1, fc2, fc3, fc4, fc5 };
        //    var outputStrategy = new OutputStrategy(list);

        //    var builder = new Builder(context, codeGenerator, outputStrategy, loggerB.Object);
        //    builder.Create();
        //}
    }

    public class OutputStrategyFacts
    {
        [Fact]
        public void OutputStrategy_Returns_Expected()
        {
            var mockFileWriter = new Mock<IFileWriter>();
            var fileWriter = mockFileWriter.Object;
            var strategies = new List<IFileCreator> { new CreateSnippet(fileWriter), new CreateProject(fileWriter) };
            var config = new Mock<IGeneratorConfiguration>();
            config.SetupGet(c => c.OutputPath).Returns(System.IO.Path.GetTempPath());
            var snippet = new Snippet("MySnippet","My Content " + Guid.NewGuid().ToString("N")); 

            var outputStrategy = new OutputStrategy(strategies);

            outputStrategy.Create(snippet, config.Object);
        }
    }
}
