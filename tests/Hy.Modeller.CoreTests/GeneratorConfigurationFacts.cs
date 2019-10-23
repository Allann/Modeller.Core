using Hy.Modeller.Generator;
using FluentAssertions;
using Xunit;

namespace Hy.Modeller.Tests
{
    public class GeneratorConfigurationFacts
    {
        [Fact]
        public void GeneratorConfiguration_Packages_DontReturnNull()
        {
            var sut = new GeneratorConfiguration();
            sut.Packages.Should().NotBeNull();
        }
    }
}
