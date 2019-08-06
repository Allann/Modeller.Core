using FluentAssertions;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Models;
using Moq;
using System.Collections.Generic;
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
            config.Setup(x => x.GeneratorName).Returns("TestGenerator");
            config.Setup(x => x.SourceModel).Returns("test.json");

            var metaData = new Mock<IMetadata>();
            metaData.Setup(m => m.Name).Returns("TestGenerator");
            metaData.Setup(m => m.Version).Returns(new Base.Models.GeneratorVersion("1.0"));

            var item = new Mock<IGeneratorItem>();
            item.Setup(g => g.Metadata).Returns(metaData.Object);

            IEnumerable<IGeneratorItem> generators = new[] { item.Object };

            var settingsLoader = new Mock<ISettingsLoader>();
            var moduleLoader = new Mock<IModuleLoader>();
            moduleLoader.Setup(l => l.Load(It.IsAny<string>())).Returns(new Module());

            var generatorLoader = new Mock<IGeneratorLoader>();
            generatorLoader.Setup(l => l.TryLoad(It.IsAny<string>(), out generators)).Returns(true);
            var packageService = new Mock<IPackageService>();

            var sut = new Context(config.Object, settingsLoader.Object, moduleLoader.Object, generatorLoader.Object, packageService.Object);
            var result = sut.ProcessConfiguration();

            packageService.Verify(s => s.Refresh(It.Is<Context>(c=>ReferenceEquals(c, sut))), Times.Once);

            sut.TargetFolder.Should().NotBeNull();
            sut.Generator.Should().NotBeNull();
            sut.Module.Should().NotBeNull();
            sut.Version.Should().NotBeNull();

            //result.Errors.Should().HaveCount(0);
        }
    }
}
