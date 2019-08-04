using FluentAssertions;
using Hy.Modeller.Core.Outputs;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Outputs;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Hy.Modeller.CoreTests
{
    public class BuilderFacts
    {
        [Fact]
        public void Builder_Allows_Create()
        {
            var op = System.IO.Path.GetTempPath();

            var snippet = new Mock<ISnippet>();

            var context = new Mock<IContext>();
            context.Setup(c => c.GeneratorConfiguration.OutputPath).Returns(op);
            var codeGenerator = new Mock<ICodeGenerator>();
            codeGenerator.Setup(c => c.Create(It.IsAny<IContext>())).Returns(snippet.Object);
            var outputStrategy = new Mock<IOutputStrategy>();
            var logger = new Mock<ILogger<IBuilder>>();

            var builder = new Builder(context.Object, codeGenerator.Object, outputStrategy.Object, logger.Object);
            builder.Create();

            codeGenerator.Verify(c => c.Create(It.IsAny<IContext>()), Times.Once);
            outputStrategy.Verify(c => c.Create(It.IsAny<IOutput>(), It.IsAny<IGeneratorConfiguration>()), Times.Once);
        }

        [Fact]
        public void Serialize()
        {
            var module = Hy.Modeller.Fluent.Module
                .Create("Member")
                .CompanyName("FourMi")
                .AddModel("Member")
                    .WithKey()
                        .AddField("Id").DataType(DataTypes.Int32).Nullable(false).Build
                        .Build
                    .AddField("TenantId").DataType(DataTypes.Int32).Nullable(false).Build
                    .AddField("FirstName").MaxLength(50).Nullable(false).Build
                    .AddField("LastName").MaxLength(50).Nullable(false).Build
                    .AddField("Email").MaxLength(256).BusinessKey(true).Nullable(false).Build
                    .AddField("Phone").MaxLength(15).Nullable(true).Build
                    .Build
                .AddModel("Membership")
                    .WithKey()
                        .AddField("Id").DataType(DataTypes.Int32).Nullable(false).Build
                        .Build
                    .AddField("TenantId").DataType(DataTypes.Int32).Nullable(false).Build
                    .AddField("StartDate").DataType(DataTypes.DateTimeOffset).Nullable(true).Build
                    .AddField("EndDate").DataType(DataTypes.DateTimeOffset).Nullable(true).Build
                    .AddField("Status").DataTypeTypeName("MembershipStates").Nullable(false).Build
                    .Build
                .AddModel("MembershipApplication")
                    .WithKey()
                        .AddField("Id").DataType(DataTypes.Int32).Nullable(false).Build
                        .Build
                    .AddField("TenantId").DataType(DataTypes.Int32).Nullable(false).Build
                    .AddField("ApprovalRequired").DataType(DataTypes.Bool).Nullable(false).Build
                    .AddField("FullPayment").DataType(DataTypes.Bool).Nullable(false).Build
                    .AddField("PaymentEmailTemplateId").DataType(DataTypes.Int32).Nullable(true).Build
                    .AddField("ActivationEmailTemplateId").DataType(DataTypes.Int32).Nullable(true).Build
                    .Build
                .AddModel("Group")
                    .WithKey()
                        .AddField("Id").DataType(DataTypes.Int32).Nullable(false).Build
                        .Build
                    .AddField("TenantId").DataType(DataTypes.Int32).Nullable(false).Build
                    .AddField("Name").MaxLength(50).Nullable(false).Build
                    .AddField("Description").MaxLength(1000).Nullable(true).Build
                    .Build
                .AddModel("Level")
                    .WithKey()
                        .AddField("Id").DataType(DataTypes.Int32).Nullable(false).Build
                        .Build
                    .AddField("TenantId").DataType(DataTypes.Int32).Nullable(false).Build
                    .AddField("Name").MaxLength(50).Nullable(false).Build
                    .AddField("Type").DataTypeTypeName("MembershipTypes").Nullable(false).Build
                    .AddField("BundleLimit").DataType(DataTypes.Int32).Nullable(false).Build
                    .AddField("Description").MaxLength(1000).Build
                    .AddField("Fee").DataTypeTypeName("Money").Nullable(false).Build
                    .AddField("IsTaxed").DataType(DataTypes.Bool).Nullable(false).Build
                    .AddField("IsPublic").DataType(DataTypes.Bool).Nullable(false).Build
                    .AddField("IsUpgradable").DataType(DataTypes.Bool).Nullable(false).Build
                    .Build
                .AddModel("PaymentMethod")
                    .WithKey()
                        .AddField("Id").DataType(DataTypes.Int32).Nullable(false).Build
                        .Build
                    .AddField("Name").MaxLength(50).Nullable(false).Build
                    .Build
                .AddModel("RenewalReminder")
                    .WithKey()
                        .AddField("Id").DataType(DataTypes.Int32).Nullable(false).Build
                        .Build
                    .AddField("DaysBefore").DataType(DataTypes.Int32).Nullable(false).Build
                    .AddField("GenerateInvoice").DataTypeTypeName("RenewalFlags").Nullable(false).Build
                    .AddField("EmailTemplateId").DataType(DataTypes.Int32).Nullable(false).Build
                    .Build
                .AddModel("Policy")
                    .WithKey()
                        .AddField("Id").DataType(DataTypes.Int32).Nullable(false).Build
                        .Build
                    .AddField("Period").DataTypeTypeName("MembershipPeriods").Nullable(false).Build
                    .AddField("Custom").DataType(DataTypes.Time).Nullable(true).Build
                    .Build
                .Build;

            var level = module.Models.Single(m => m.Name.Value == "Level");
            var member = module.Models.Single(m => m.Name.Value == "Member");
            var paymentMethod = module.Models.Single(m => m.Name.Value == "PaymentMethod");
            var policy = module.Models.Single(m => m.Name.Value == "Policy");
            module.AddForeignKey(level, member);
            module.AddForeignKey(paymentMethod, level);
            module.AddForeignKey(policy, level);

            Console.WriteLine( JsonExtensions.ToJson(module));

            //output.Should().Be("{\"company\":\"FourMi\",\"project\":\"Member\",\"models\":[]}");
        }

        //[Fact]
        //public void Builder_Real_Test()
        //{
        //    var config = new GeneratorConfiguration
        //    {
        //        GeneratorName = "DomainProject",
        //        LocalFolder = "F:\\Repos\\Modeller.SampleGenerators\\src\\Generators",
        //        Target="netstandard2.0",
        //        OutputPath = "F:\\dev\\test\\members",
        //        SourceModel = "F:\\dev\\members_model.json"
        //    };

        //    var logger = new Mock<ILogger<IPackageService>>();
        //    var settingLoader = new JsonSettingsLoader();
        //    var moduleLoader = new JsonModuleLoader();
        //    var generatorLoader = new GeneratorLoader();
        //    var packageLoader = new PackageFileLoader();
        //    var packageService = new PackageService(packageLoader, logger.Object);

        //    var context = new Context(config, settingLoader, moduleLoader, generatorLoader, packageService);

        //    var loggerCG = new Mock<ILogger<ICodeGenerator>>();
        //    var loggerFW = new Mock<ILogger<FileWriter>>();
        //    var loggerB = new Mock<ILogger<IBuilder>>();
        //    var codeGenerator = new CodeGenerator(loggerCG.Object);

        //    var fileWriter = new FileWriter(loggerFW.Object);
        //    var fc1 = new CreateFile(fileWriter);
        //    var fc2 = new CreateFileGroup(fileWriter);
        //    var fc3 = new CreateProject(fileWriter);
        //    var fc4 = new CreateSnippet(fileWriter);
        //    var fc5 = new CreateSolution(fileWriter);
        //    var list = new List<IFileCreator> { fc1, fc2, fc3, fc4, fc5 };
        //    var outputStrategy = new OutputStrategy(list);

        //    var builder = new Builder(context, codeGenerator, outputStrategy, loggerB.Object);
        //    builder.Create();
        //}
    }
}
