using Hy.Modeller.Domain;
using Hy.Modeller.Domain.Extensions;
using Xunit;
using FluentAssertions;

namespace Hy.Modeller.DomainTests
{
    public class EnumerationFacts
    {
        [Fact]
        public void Enumeration_CanSerialize_Success()
        {
            var module = new Module("Company", "Test");
            var sut = new Enumeration("PaymentTypes")
                .AddItem("Cash")
                .AddItem("CreditCard")
                .AddItem("Cheque")
                .AddItem("DirectDebit");
            module.Enumerations.Add(sut);

            var expected = module.ToJson();

            expected.Should().Be("{\"company\":\"Company\",\"project\":\"Test\",\"models\":[],\"enumerations\":[{\"items\":[{\"name\":\"Cash\",\"display\":\"Cash\",\"value\":0},{\"name\":\"CreditCard\",\"display\":\"Credit Card\",\"value\":1},{\"name\":\"Cheque\",\"display\":\"Cheque\",\"value\":2},{\"name\":\"DirectDebit\",\"display\":\"Direct Debit\",\"value\":3}],\"name\":\"PaymentType\"}],\"requests\":[]}");
            var actual = expected.FromJson<Module>();

            actual.Should().BeEquivalentTo(module);
        }

        [Fact]
        public void Enumeration_FlagValues_Set()
        {
            var module = new Module("Company", "Test");
            var sut = new Enumeration("PaymentTypes", true)
                .AddItem("None")
                .AddItem("Cash")
                .AddItem("CreditCard")
                .AddItem("Cheque")
                .AddItem("DirectDebit");
            module.Enumerations.Add(sut);

            var expected = module.ToJson();

            expected.Should().Be("{\"company\":\"Company\",\"project\":\"Test\",\"models\":[],\"enumerations\":[{\"flag\":true,\"items\":[{\"name\":\"None\",\"display\":\"None\",\"value\":0},{\"name\":\"Cash\",\"display\":\"Cash\",\"value\":1},{\"name\":\"CreditCard\",\"display\":\"Credit Card\",\"value\":2},{\"name\":\"Cheque\",\"display\":\"Cheque\",\"value\":4},{\"name\":\"DirectDebit\",\"display\":\"Direct Debit\",\"value\":8}],\"name\":\"PaymentType\"}],\"requests\":[]}");
            var actual = expected.FromJson<Module>();

            actual.Should().BeEquivalentTo(module);
        }
    }
}
