using FluentAssertions;
using System.Linq;
using Xunit;

namespace Hy.Modeller.CoreTests
{
    public class TargetFacts
    {
        [Fact]
        public void Targets_Default_ReturnsExpectedValue()
        {
            var sut = new Targets();
            sut.Default.Should().Be(sut.Supported.First());
        }

        [Fact]
        public void Targets_Supported_ReturnsExpectedCount()
        {
            var sut = new Targets();
            sut.Supported.Should().HaveCount(1);
        }

        [Fact]
        public void Targets_Shared_ReturnsSameInstance()
        {
            var sut = Targets.Shared;
            var another = Targets.Shared;

            sut.Should().BeSameAs(another);

            sut.RegisterTarget("new One");
            another.Supported.Should().Contain("new one");
        }

        [Fact]
        public void Targets_Reset_ReturnsSupportedToDefault()
        {
            var sut = new Targets();
            sut.RegisterTarget("new");
            sut.Supported.Should().HaveCount(2);

            sut.Reset();

            sut.Supported.Should().HaveCount(1);
        }
    }
}
