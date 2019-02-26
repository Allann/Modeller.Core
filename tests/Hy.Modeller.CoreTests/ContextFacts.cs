using FluentAssertions;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;
using Moq;
using Xunit;

namespace Hy.Modeller.CoreTests
{

    public class ContextFacts
    {
        [Fact]
        public void Context_Constructor()
        {
            var config = new Mock<IGeneratorConfiguration>();
            config.Setup(x => x.LocalFolder).Returns("c:\\Local");
            config.Setup(x => x.Target).Returns("1");

            var settingsLoader = new Mock<ISettingsLoader>();
            var moduleLoader = new Mock<IModuleLoader>();
            var generatorLoader = new Mock<IGeneratorLoader>();
            var packageService = new Mock<IPackageService>();

            var sut = new Context(config.Object, settingsLoader.Object, moduleLoader.Object, generatorLoader.Object, packageService.Object);

            sut.TargetFolder.Should().NotBeNull();
            sut.Generator.Should().NotBeNull();
            sut.Module.Should().NotBeNull();
            sut.Version.Should().NotBeNull();
        }
    }
}
