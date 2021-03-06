﻿using Hy.Modeller.Domain;
using Newtonsoft.Json;
using System;

namespace Hy.Modeller.Domain.Extensions
{
    public class NameConverter : JsonConverter<Name>
    {
        public override Name ReadJson(JsonReader reader, Type objectType, Name existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader is null)
                throw new ArgumentNullException(nameof(reader));

            var s = (string)reader.Value;

            if (hasExistingValue && existingValue != null)
            {
                existingValue.SetName(s);
                return existingValue;
            }
            else
            {
                return new Name(s);
            }
        }

        public override void WriteJson(JsonWriter writer, Name value, JsonSerializer serializer)
        {
            if (string.IsNullOrWhiteSpace(value.Overridden))
                writer.WriteValue(value.Value + string.Empty);
            else
                writer.WriteValue($"{value.Singular.Value}[{value.Value}]");
        }
    }
}
