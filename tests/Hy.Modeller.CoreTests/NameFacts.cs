﻿using Hy.Modeller.Domain;
using Hy.Modeller.Domain.Extensions;
using Xunit;
using FluentAssertions;
using System;

namespace Hy.Modeller.DomainTests
{
    public static class NameFacts
    {
        [Theory]
        [InlineData("Tests", null, "\"Test\"", "Test")]
        [InlineData("Test[s]", null, "\"Test\"", "Test")]
        [InlineData("Test", null, "\"Test\"", "Test")]
        [InlineData("Test", "Tests", "\"Test[Tests]\"", "Tests")]
        [InlineData("Test", "Testing", "\"Test[Testing]\"", "Testing")]
        [InlineData("PriceOffer", "Promotion", "\"PriceOffer[Promotion]\"", "Promotion")]
        public static void Name_Serialization_MaintainsValues(string actual, string overridden, string serialised, string expected)
        {
            var name = new Name(actual);
            if (!string.IsNullOrWhiteSpace(overridden))
                name.SetOverride(overridden);

            var json = name.ToJson();
            json.Should().Be(serialised);
            var name2 = json.FromJson<Name>();

            name2.Should().BeEquivalentTo(name);
            name2.Value.Should().Be(expected);
        }

        [Fact]
        public static void Name_Equals_WhenSameInstance()
        {
            var name = new Name("Test");
            name.Equals(name).Should().BeTrue();
        }

        [Fact]
        public static void Name_Equals_WhenSameObjectInstance()
        {
            var name = new Name("Test");
            var name2 = (object)name;

            name.Equals(name2).Should().BeTrue();
        }

        [Fact]
        public static void Name_Equals_ByObjectName()
        {
            var name = new Name("Test");
            var name2 = new Name("Test");

            name.Equals(name2).Should().BeTrue();
        }

        [Fact]
        public static void Name_Equals_SinglularNameAndPluralName()
        {
            var names = new Name("Tests");
            var name = new Name("Test");

            name.Equals(names).Should().BeTrue();
        }

        [Fact]
        public static void Name_Values_SetWhenCreated()
        {
            var name = new Name("Freight_Rate");

            name.Singular.Display.Should().Be("Freight Rate");
            name.Singular.LocalVariable.Should().Be("freightRate");
            name.Singular.ModuleVariable.Should().Be("_freightRate");
            name.Singular.StaticVariable.Should().Be("FreightRate");
            name.Singular.Value.Should().Be("FreightRate");

            name.Plural.Display.Should().Be("Freight Rates");
            name.Plural.LocalVariable.Should().Be("freightRates");
            name.Plural.ModuleVariable.Should().Be("_freightRates");
            name.Plural.StaticVariable.Should().Be("FreightRates");
            name.Plural.Value.Should().Be("FreightRates");
        }

        [Fact]
        public static void Name_ToString_ReturnsSingularValue()
        {
            var name = new Name("Tests");
            string.Equals(name.ToString(), name.Singular.Value, StringComparison.InvariantCulture).Should().BeTrue();
        }

        [Fact]
        public static void Name_GetHashCode_SameForSingleAndPluralNames()
        {
            var names = new Name("Tests");
            var name = new Name("Test");

            name.GetHashCode().Equals(names.GetHashCode()).Should().BeTrue();
        }
    }
}