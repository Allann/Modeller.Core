using FluentAssertions;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;
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
        private IEnumerable<Package> GetOtherTargetTestPackages() => new List<Package> { new Package("Package1", "2.3.4"), new Package("Package2", "3.3") };

        [Fact]
        public void PackageService_Target_HasDefault()
        {
            IEnumerable<Package> packages = new List<Package>();

            var loader = new Mock<IPackageFileLoader>();
            loader.Setup(l => l.TryLoad(It.IsAny<string>(), out packages)).Returns(true);
            var sut = new PackageService(loader.Object);

            sut.Target.Should().Be(Defaults.Target);
        }

        [Fact]
        public void PackageService_LoadsFiles()
        {
            var packages = GetDefaultTargetTestPackages();

            var loader = new Mock<IPackageFileLoader>();
            loader.Setup(l => l.TryLoad(It.IsAny<string>(), out packages)).Returns(true);
            var sut = new PackageService(loader.Object);

            sut.Items.Should().BeEquivalentTo(packages);
        }

        [Fact]
        public void PackageService_LoadsMultipleTargetFiles()
        {
            var packages1 = GetDefaultTargetTestPackages();
            var packages2 = GetOtherTargetTestPackages();

            var loader = new Mock<IPackageFileLoader>();
            var sut = new PackageService(loader.Object);
            var folder = sut.Folder;

            loader.Setup(l => l.TryLoad(folder + "\\1.json", out packages1)).Returns(true);
            loader.Setup(l => l.TryLoad(folder + "\\2.json", out packages2)).Returns(true);

            sut.Target = "1";
            sut.Items.Should().BeEquivalentTo(packages1);
            sut.Target = "2";
            sut.Items.Should().BeEquivalentTo(packages2);
            sut.Target = "1";
            sut.Items.Should().BeEquivalentTo(packages1);

            loader.Verify(m => m.TryLoad(It.IsAny<string>(), out packages1), Times.Exactly(2));
        }

        [Fact]
        public void PackageService_ThrowsMissingTargetException_WhenFolderNotValid()
        {
            var packages = GetDefaultTargetTestPackages();

            var loader = new Mock<IPackageFileLoader>();
            loader.Setup(l => l.TryLoad(It.IsAny<string>(), out packages)).Returns(false);
            var sut = new PackageService(loader.Object);

            Func<IEnumerable<Package>> act = () => sut.Items;
            act.Should().Throw<MissingTargetException>();
        }
    }
}
