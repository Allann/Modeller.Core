using Hy.Modeller.Domain;
using Hy.Modeller.Domain.Extensions;
using Xunit;
using FluentAssertions;
using System;

namespace Hy.Modeller.DomainTests
{
    public class RequestFacts
    {
        [Theory]
        [InlineData("ChangeBillingAddress", false, "ChangeBillingAddressCommand")]
        [InlineData("ChangeBillingAddressQuery", false, "ChangeBillingAddressCommand")]
        [InlineData("ChangeBillingAddressCommand", false, "ChangeBillingAddressCommand")]
        [InlineData("Detail", true, "DetailQuery")]
        [InlineData("DetailQuery", true, "DetailQuery")]
        [InlineData("DetailCommand", true, "DetailQuery")]
        public static void Request_WithoutReponse_CreatesCommand(string request, bool includeResponse, string expected)
        {
            var sut = !includeResponse ? new Request(request) : new Request(request, new Response());
            sut.Name.ToString().Should().Be(expected);
            sut.Fields.Should().BeEmpty();
        }

        [Theory]
        [InlineData("Command")]
        [InlineData("Query")]
        [InlineData("")]
        [InlineData(null)]
        public static void Request_SetInvalidName_ThrowsArgumentException(string actual)
        {
            Action action = () => new Request(actual);
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Request_CanSerializeCommand_Success()
        {
            var module = new Module("Company", "Test");
            module.AddRequest("ChangeBillingAddress")
                .AddField("TenantId", DataTypes.Int32, false)
                .AddField("Line1");

            var expected = module.ToJson();
            expected.Should().Be("{\"company\":\"Company\",\"project\":\"Test\",\"models\":[],\"enumerations\":[],\"requests\":[{\"fields\":[{\"dataType\":\"Int32\",\"name\":\"TenantId\"},{\"name\":\"Line1\"}],\"name\":\"ChangeBillingAddressCommand\"}]}");

            var actual = expected.FromJson<Module>();
            actual.Should().BeEquivalentTo(module);
        }

        [Fact]
        public void Request_CanSerializeQuery_Success()
        {
            var module = new Module("Company", "Test");
            module.AddRequest("Detail")
                .AddField("tenantId", DataTypes.Int32)
                .AddResponse()
                .AddField("Tenant")
                .AddField("Name");

            var expected = module.ToJson();
            expected.Should().Be("{\"company\":\"Company\",\"project\":\"Test\",\"models\":[],\"enumerations\":[],\"requests\":[{\"response\":{\"fields\":[{\"name\":\"Tenant\"},{\"name\":\"Name\"}]},\"fields\":[{\"dataType\":\"Int32\",\"name\":\"TenantId\"}],\"name\":\"DetailQuery\"}]}");

            var actual = expected.FromJson<Module>();
            actual.Should().BeEquivalentTo(module);
        }
    }
}
