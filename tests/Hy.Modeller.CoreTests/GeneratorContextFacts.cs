using FluentAssertions;
using Hy.Modeller.Core.Outputs;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Outputs;
using Moq;
using System;
using System.Text;
using Xunit;

namespace Hy.Modeller.CoreTests
{
    public class GeneratorContextFacts
    {
        [Fact]
        public void GeneratorContext_Defaults_AreSetOnConstructor()
        {
            var sut = new GeneratorContext();
            sut.Packages.Should().NotBeNull();
        }
    }

    internal class TestFile : IFile
    {
        private readonly string _name;
        public TestFile()
        {
            _name = System.IO.Path.GetTempFileName();
        }

        string IFile.Content { get; set; }
        string IFile.Path { get; set; }
        string IFile.FullName { get; }
        bool IFile.CanOverwrite { get; set; }
        string IOutput.Name => _name;
    }

    internal class TestFileWriter : IFileWriter
    {
        public string Output { get; private set; }

        void IFileWriter.Write(IFile file) {
            Output = file.Content;
        }
    }

    public class OutputCreatorFacts
    {
        [Fact]
        public void Creator_Creates_ExpectedSnippetOutput()
        {
            var sb = new StringBuilder();
            var context = new Mock<IContext>();
            context.Setup(c => c.OutputPath).Returns("myPath");
            var output = new Mock<ISnippet>();

            var writer = new Mock<IFileWriter>();
            var creator = new Mock<IFileCreator>();

            //var writer = new TestFileWriter();
            //var fileFactory = new FileFactory(new Lazy<IFile>(() =>
            //{
            //    IFile file = new TestFile();
            //    file.Content = "My test content";
            //    return file;
            //}));
            //var fileWriterFactory = new FileWriterFactory(new Lazy<IFileWriter>(() => writer));

            var sut = new Creator(context.Object,writer.Object,creator.Object , s=>sb.AppendLine(s));
                       
            sut.Create(output.Object);


            sb.ToString().Should().Match("Creating: myPath\r\nFile: myPath\\myPath\\Snippets\\tmp*.txt\r\nGeneration complete.\r\n");
            output.Verify(s => s.Content, Times.AtLeastOnce);
        }
    }
}
