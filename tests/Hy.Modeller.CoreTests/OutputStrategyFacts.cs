using Hy.Modeller.Generator;
using Hy.Modeller.Generator.Outputs;
using Hy.Modeller.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Hy.Modeller.Tests
{
    public class OutputStrategyFacts
    {
        //[Fact]
        //public void OutputStrategy_Returns_Expected()
        //{
        //    var mockFileWriter = new Mock<IFileWriter>();
        //    var fileWriter = mockFileWriter.Object;
        //    var strategies = new List<IFileCreator> { new CreateSnippet(fileWriter), new CreateProject(fileWriter) };
        //    var config = new Mock<IGeneratorConfiguration>();
        //    config.SetupGet(c => c.OutputPath).Returns(System.IO.Path.GetTempPath());
        //    var snippet = new Snippet("MySnippet", "My Content " + Guid.NewGuid().ToString("N"));

        //    var outputStrategy = new OutputStrategy(strategies);

        //    outputStrategy.Create(snippet, config.Object);
        //}
    }
}
