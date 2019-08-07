using FluentAssertions;
using Xunit;

namespace Hy.Modeller.CoreTests
{
    public class GeneratorConfigurationFacts
    {
        [Fact]
        public void GeneratorConfiguration_Packages_DontReturnNull()
        {
            var sut = new GeneratorConfiguration();
            sut.Packages.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void GeneratorConfiguration_Target_ReturnsExpectedValues(string value)
        {
            var sut = new GeneratorConfiguration
            {
                Target = value
            };

            sut.Target.Should().Be(Defaults.Target);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void GeneratorConfiguration_LocalFolder_ReturnsExpectedValues(string value)
        {
            var sut = new GeneratorConfiguration
            {
                LocalFolder = value
            };

            sut.LocalFolder.Should().Be(Defaults.LocalFolder);
        }
    }
}
