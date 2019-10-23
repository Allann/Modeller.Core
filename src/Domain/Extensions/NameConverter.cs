using Hy.Modeller.Domain;
using Newtonsoft.Json;
using System;

namespace Hy.Modeller.Domain.Extensions
{
    public class NameConverter : JsonConverter<Name>
    {
        public override Name ReadJson(JsonReader reader, Type objectType, Name existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var s = (string)reader.Value;
            if (s.EndsWith("]"))
            {
                var match = s.LastIndexOf("[");
                if (match > -1)
                {
                    var ovr = s.Substring(match + 1, s.Length - match - 2);
                    s = s.Substring(0, match - 1);
                    var temp = new Name(s);
                    temp.SetOverride(ovr);
                    return temp;
                }
            }
            return new Name(s);
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
