using FluentAssertions;
using Hy.Modeller.CoreTests.TestGenenerators;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Models;
using Hy.Modeller.Outputs;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Hy.Modeller.CoreTests
{
    public class CodeGeneratorFacts
    {
        [Fact]
        public void CodeGenerator_Allows_Create()
        {
            var op = System.IO.Path.GetTempPath();
            var module = new Module() { Company = "My", Project = new Name("Project") };
            var model = new Model("MyModel");
            model.Fields.Add(new Field("MyField") { MaxLength = 30 });
            module.Models.Add(model);

            var wModule = Hy.Modeller.Base.WorkingModels.Module.Create(module);

            var metadata = new Mock<IMetadata>();
            metadata.SetupGet(c => c.EntryPoint).Returns(typeof(SimpleTestGenerator));

            var generatorItem = new Mock<IGeneratorItem>();
            generatorItem.SetupGet(c => c.Metadata).Returns(metadata.Object);

            var settings = new Mock<ISettings>();
            var context = new Mock<IContext>();
            context.Setup(c => c.ProcessConfiguration()).Returns(new FluentValidation.Results.ValidationResult());
            context.SetupGet(c => c.Module).Returns(module);
            context.SetupGet(c => c.Settings).Returns(settings.Object);
            context.SetupGet(c => c.Generator).Returns(generatorItem.Object);
            var fileWriter = new Mock<IFileWriter>();
            var fileCreater = new Mock<IFileCreator>();

            var logger = new Mock<ILogger<ICodeGenerator>>();

            var codeGenerator = new CodeGenerator(logger.Object);

            var output = codeGenerator.Create(context.Object);

            output.Should().BeOfType<Snippet>();
            ((ISnippet)output).Content.Should().Be("Snippet Content for Project");
        }
    }
}
