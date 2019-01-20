using Hy.Modeller.Interfaces;
using Hy.Modeller.Outputs;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Hy.Modeller.CoreTests
{
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
