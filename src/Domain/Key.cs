using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hy.Modeller.Domain
{
    public class Key
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public List<Field> Fields { get; } = new List<Field>();
    }
}