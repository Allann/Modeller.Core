using FluentAssertions;
using Hy.Modeller.Base.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hy.Modeller.CoreTests
{
    public class FileHelperFacts
    {
        [Theory]
        [InlineData("Hy.Helper.Tester.dll", "Hy.Helper.Tester", null)]
        [InlineData("Hy.Helper.Tester.2.0.dll", "Hy.Helper.Tester", "2.0")]
        [InlineData("Api.v2.0.dll", "Api", "2.0")]
        [InlineData("Api.v2.0.deps.json", "Api.deps", "2.0")]
        [InlineData("Api.v2.0.pdb", "Api", "2.0")]
        public void FileHelper_GetAbbreviatedFileName_ReturnsExpected(string filePath, string expectedFilename, string version)
        {
            var f = FileHelper.GetAbbreviatedFilename(filePath);

            GeneratorVersion expectedVersion;
            if (version == null)
                expectedVersion = null;
            else if (version == string.Empty)
                expectedVersion = new GeneratorVersion();
            else
                expectedVersion = new GeneratorVersion(version);

            f.filename.Should().Be(expectedFilename);
            f.version.Should().BeEquivalentTo(expectedVersion);
        }
    }
}
