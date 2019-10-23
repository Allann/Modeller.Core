using Hy.Modeller.Generator;
using Hy.Modeller.Generator.Exceptions;
using Hy.Modeller.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Hy.Modeller.Tests
{
    public class PackageServiceFacts
    {
        //private IEnumerable<IPackage> GetDefaultTargetTestPackages() => new List<IPackage> { new Package("Package1", "1.2.3"), new Package("Package2", "2.3") };

        //[Fact]
        //public void PackageService_LoadsFiles()
        //{
        //    var packages = GetDefaultTargetTestPackages();

        //    var logger = new Mock<ILogger<IPackageService>>();
        //    var context = new Mock<IContext>();
        //    context.Setup(c => c.GeneratorConfiguration.Target).Returns(Defaults.Target);
        //    context.Setup(c => c.TargetFolder).Returns(System.IO.Path.GetTempPath());
        //    var loader = new Mock<ILoader<IPackage>>();
        //    loader.Setup(l => l.TryLoad(It.IsAny<string>(), out packages)).Returns(true);

        //    IPackageService sut = new PackageService(loader.Object, logger.Object);
        //    sut.Refresh(context.Object);

        //    sut.Items.Should().BeEquivalentTo(packages);
        //}

        //[Fact]
        //public void PackageService_ThrowsMissingTargetException_WhenFolderNotValid()
        //{
        //    var packages = GetDefaultTargetTestPackages();

        //    var logger = new Mock<ILogger<IPackageService>>();
        //    var context = new Mock<IContext>();
        //    context.Setup(c => c.GeneratorConfiguration.Target).Returns(Defaults.Target);
        //    context.Setup(c => c.TargetFolder).Returns(System.IO.Path.GetTempPath());
        //    var loader = new Mock<ILoader<IPackage>>();
        //    loader.Setup(l => l.TryLoad(It.IsAny<string>(), out packages)).Returns(false);

        //    IPackageService sut = new PackageService(loader.Object, logger.Object);
        //    sut.Refresh(context.Object);

        //    Func<IEnumerable<IPackage>> act = () => sut.Items;
        //    act.Should().Throw<MissingTargetException>();
        //}
    }
}
