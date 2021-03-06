﻿using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Tests.TestFiles;
using FluentAssertions;
using Moq;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace Hy.Modeller.Tests
{

    public class GeneratorItemFacts
    {
        private string GetThisFilePath()
        {
            return Path.GetFileName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath);
        }

        [Fact]
        public void GeneratorItem_ReturnsValidInstance_WhenInstanceIsCalled()
        {
            var thisTestFilePath = GetThisFilePath();

            var meta = new Mock<IMetadata>();
            var settings = new Mock<ISettings>();
            var module = new Domain.Module("Company", "Test");

            var sut = new GeneratorItem(meta.Object, thisTestFilePath, typeof(SimpleTestGenerator));

            sut.Instance(settings.Object, module).Should().NotBeNull();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void GeneratorItem_ThrowsArgumentException_WhenNoFilePathIsPassed(string filepath)
        {
            var meta = new Mock<IMetadata>();
            Action create = () => new GeneratorItem(meta.Object, filepath, typeof(SimpleTestGenerator));

            create.Should().Throw<ArgumentException>().And.Message.Should().Be("File path must be provided (Parameter 'filePath')");
        }

        [Fact]
        public void GeneratorItem_ThrowsArgumentNullException_WhenNoMetadataIsPassed()
        {
            Action create = () => new GeneratorItem(null, GetThisFilePath(), typeof(SimpleTestGenerator));

            create.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("metadata");
        }

        [Fact]
        public void GeneratorItem_ThrowsArgumentNullException_WhenNoGeneratorTypeIsPassed()
        {
            var meta = new Mock<IMetadata>();
            Action create = () => new GeneratorItem(meta.Object, GetThisFilePath(), null);

            create.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("generatorType");
        }

        [Fact]
        public void GeneratorItem_FileNotFoundException_WhenInvalidFilePathIsPassed()
        {
            var meta = new Mock<IMetadata>();
            Action create = () => new GeneratorItem(meta.Object, "c:\\xx\\invalidFilename.yyy", typeof(SimpleTestGenerator));

            create.Should().Throw<FileNotFoundException>().And.Message.Should().Be("Generator not found.");
        }

        [Fact]
        public void GeneratorItem_Properties_CorrectlySet()
        {
            var thisTestFilePath = GetThisFilePath();

            var meta = new Mock<IMetadata>();
            var sut = new GeneratorItem(meta.Object, thisTestFilePath, typeof(SimpleTestGenerator));

            sut.AbbreviatedFileName.Should().NotBeEmpty();
        }
    }
}
