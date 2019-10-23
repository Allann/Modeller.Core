using Hy.Modeller.Domain;
using Hy.Modeller.Domain.Extensions;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Hy.Modeller.Tests
{
    public class FluentFacts
    {
        [Fact]
        public void Fluent_Module_CreateSucceeds()
        {
            var module = Fluent.Module
                .Create("Company","Project")
                .DefaultSchema("dbo")
                .FeatureName("Feature")
                .AddModel("Model")
                    .IsAuditable(true)
                    .Schema("dbo")
                    .WithDefaultKey()
                    .AddField("Field1")
                        .BusinessKey(true)
                        .DataType(DataTypes.String)
                        .Default("Default")
                        .MaxLength(10)
                        .MinLength(5)
                        .Build
                    .AddField("Field2")
                        .DataType(DataTypes.Decimal)
                        .Scale(12)
                        .Precision(2)
                        .Nullable(true)
                        .MinLength(200)
                        .MaxLength(3000)
                        .Build
                    .Build
                .AddEnumeration("Enumeration")
                    .AsFlag()
                    .AddItem("None")
                    .AddItem("One")
                    .AddItem("Two")
                    .AddItem("Three")
                    .Build
                .AddRequest("ChangeModel")
                    .AddField("ModelId")
                        .Build
                        .WithResponse()
                            .AddField("ModelName")
                            .Build
                        .Build
                    .Build
                .Build;

            var output = module.ToJson();

            output.Should().Be("{\"company\":\"Company\",\"project\":\"Project\",\"feature\":\"Feature\",\"defaultSchema\":\"dbo\",\"models\":[{\"schema\":\"dbo\",\"key\":{\"fields\":[{\"dataType\":\"UniqueIdentifier\",\"name\":\"Id\"}]},\"fields\":[{\"default\":\"Default\",\"maxLength\":10,\"minLength\":5,\"businessKey\":true,\"name\":\"Field1\"},{\"maxLength\":3000,\"minLength\":200,\"precision\":2,\"scale\":12,\"dataType\":\"Decimal\",\"nullable\":true,\"name\":\"Field2\"}],\"indexes\":[],\"relationships\":[],\"name\":\"Model\"}],\"enumerations\":[{\"flag\":true,\"items\":[{\"name\":\"None\",\"display\":\"None\",\"value\":0},{\"name\":\"One\",\"display\":\"One\",\"value\":1},{\"name\":\"Two\",\"display\":\"Two\",\"value\":2},{\"name\":\"Three\",\"display\":\"Three\",\"value\":4}],\"name\":\"Enumeration\"}],\"requests\":[{\"response\":{\"fields\":[{\"name\":\"ModelName\"}]},\"fields\":[{\"name\":\"ModelId\"}],\"name\":\"ChangeModelQuery\"}]}");
        }
    }
}
