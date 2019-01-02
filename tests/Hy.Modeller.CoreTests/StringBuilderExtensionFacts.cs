using FluentAssertions;
using Hy.Modeller.Outputs;
using System.Text;
using Xunit;

namespace Hy.Modeller.CoreTests
{
    public class StringBuilderExtensionFacts
    {
        [Fact]
        public void StringBuild_Indent_DefaultsTo4Spaces()
        {
            var sut = new StringBuilder();

            sut.Indent(1);
            
            sut.ToString().Should().Be("    ");
        }

        [Theory]
        [InlineData(-4, "    ")]
        [InlineData(0, "    ")]
        [InlineData(3, "   ")]
        [InlineData(5, "     ")]
        [InlineData(8, "        ")]
        [InlineData(9, "    ")]
        public void StringBuild_Indent_DefaultsTo6Spaces(int spaces, string expected)
        {
            var sut = new StringBuilder();

            sut.Indent(1, spaces);

            sut.ToString().Should().Be(expected);
        }
    }
}
