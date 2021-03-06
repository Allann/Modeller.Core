﻿using Hy.Modeller.Domain.Extensions;
using Xunit;
using FluentAssertions;
using Moq;
using System;
using System.Text;

namespace Hy.Modeller.DomainTests
{
    public class StringBuilderExtensionFacts
    {
        [Theory]
        [InlineData("Id", "ValueId", "Value")]
        [InlineData("Id", "Value Id", "Value ")]
        [InlineData("Id", "ValueIds", "ValueIds")]
        [InlineData(null, "ValueIds", "ValueIds")]
        [InlineData("x", null, "")]
        public void StringBuilderExtension_TrimEnd_RemovesValuesCorrectly(string trim, string value, string expected)
        {
            var sb = new StringBuilder(value);

            sb.TrimEnd(trim);

            sb.ToString().Should().Be(expected);
        }
    }
}
