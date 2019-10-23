using Hy.Modeller.Domain;
using Hy.Modeller.Domain.Extensions;
using Xunit;
using FluentAssertions;
using System.ComponentModel;

namespace Hy.Modeller.DomainTests
{
    public static class IndexFacts
    {
        [Fact]
        public static void Index_SetsDefaults_WhenCreated()
        {
            var sut = new Domain.Index("Test");
            sut.Name.ToString().Should().Be("Test");
            sut.IsClustered.Should().BeFalse();
            sut.IsUnique.Should().BeTrue();
        }

        [Fact]
        public static void Index_Serialization()
        {
            var sut = new Domain.Index("Test");
            sut.Fields.Add(new IndexField("Field1") { Sort = ListSortDirection.Descending });
            var json = sut.ToJson();

            //json.Should().Be("{\"fields\":[{\"sort\":\"Descending\",\"name\":\"Field1\"}],\"name\":\"Test\"}");

            var actual = json.FromJson<Domain.Index>();
            actual.Should().BeEquivalentTo(sut);
        }
    }

}
