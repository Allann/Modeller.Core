using FluentAssertions;
using Hy.Modeller.Generator;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Hy.Modeller.CoreTests
{
    public class GeneratorContextFacts
    {
        [Fact]
        public void GeneratorContext_Defaults_AreSetOnConstructor()
        {
            var sut = new GeneratorContext();
            sut.Packages.Should().NotBeNull();
        }
    }

    public class ContextFacts
    {
        [Fact]
        public void Context_Constructor()
        {
            var sb = new StringBuilder();
            Action<string> a = (s) => sb.AppendLine(s);

            var sut = new Context("model.json", "c:\\local", "class", "target", "1.0", "settings.json", "model", "c:\\output", a);

            //sut.IsValid.Should().BeTrue();
            var list = new List<string>()
            {
                "Module file 'model.json' does not exist.",
                "Local folder not found 'c:\\local'",
                "Settings file 'settings.json' does not exist.",
                "Context Module not defined.",
                "Context Generator not defined.",
                "Context Settings not defined."
            };
            sut.ValidationMessages.Should().BeEquivalentTo(list);
        }
    }
}
