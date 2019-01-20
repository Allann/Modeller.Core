using FluentAssertions;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;
using Microsoft.Extensions.Logging;
using Modeller;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Hy.Modeller.CoreTests
{
    public class PackageServiceFacts
    {
        private IEnumerable<Package> GetDefaultTargetTestPackages() => new List<Package> { new Package("Package1", "1.2.3"), new Package("Package2", "2.3") };

        [Fact]
        public void PackageService_LoadsFiles()
        {
            var packages = GetDefaultTargetTestPackages();

            var logger = new Mock<ILogger<IPackageService>>();
            var context = new Mock<IContext>();
            context.Setup(c => c.GeneratorConfiguration.Target).Returns(Defaults.Target);
            context.Setup(c => c.TargetFolder).Returns(System.IO.Path.GetTempPath());
            var loader = new Mock<IPackageFileLoader>();
            loader.Setup(l => l.TryLoad(It.IsAny<string>(), out packages)).Returns(true);

            var sut = new PackageService(loader.Object, logger.Object);
            sut.Refresh(context.Object);

            sut.Items.Should().BeEquivalentTo(packages);
        }
            
        [Fact]
        public void PackageService_ThrowsMissingTargetException_WhenFolderNotValid()
        {
            var packages = GetDefaultTargetTestPackages();

            var logger = new Mock<ILogger<IPackageService>>();
            var context = new Mock<IContext>();
            context.Setup(c => c.GeneratorConfiguration.Target).Returns(Defaults.Target);
            context.Setup(c => c.TargetFolder).Returns(System.IO.Path.GetTempPath());
            var loader = new Mock<IPackageFileLoader>();
            loader.Setup(l => l.TryLoad(It.IsAny<string>(), out packages)).Returns(false);

            var sut = new PackageService(loader.Object, logger.Object);
            sut.Refresh(context.Object);

            Func<IEnumerable<Package>> act = () => sut.Items;
            act.Should().Throw<MissingTargetException>();
        }
    }
}
