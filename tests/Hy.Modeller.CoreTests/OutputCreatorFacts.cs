using FluentAssertions;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Outputs;
using Moq;
using System.Text;
using Xunit;

namespace Hy.Modeller.CoreTests
{
    public class OutputCreatorFacts
    {
        [Fact]
        public void Creator_Creates_ExpectedSnippetOutput()
        {
            var context = new Mock<IContext>();
            context.Setup(c => c.OutputPath).Returns("myPath");

            var snippet = new Mock<ISnippet>();
            var writer = new Mock<IFileWriter>();
            var creator = new Mock<IFileCreator>();

            var sb = new StringBuilder();
            var sut = new Creator(context.Object, writer.Object, creator.Object, s => sb.AppendLine(s));
            sut.Create(snippet.Object);

            sb.ToString().Should().Match("Creating: myPath\r\nFile: myPath\\myPath\\Snippets\\tmp*.txt\r\nGeneration complete.\r\n");
            snippet.Verify(s => s.Content, Times.AtLeastOnce);
        }
    }
}
