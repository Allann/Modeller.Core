﻿using Newtonsoft.Json;
using System.ComponentModel;
using System.Diagnostics;

namespace Hy.Modeller.Domain
{
    [JsonObject(MemberSerialization.OptIn)]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Field : NamedElementBase
    {
        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        private string DebuggerDisplay => $"{Name}";

        [JsonConstructor]
        public Field(string name, DataTypes dataType = DataTypes.String, bool nullable = false)
            : base(name)
        {
            DataType = dataType;
            Nullable = nullable;
        }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Default { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxLength { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? MinLength { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Precision { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? Scale { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(DataTypes.String)]
        public DataTypes DataType { get; set; } = DataTypes.String;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string DataTypeTypeName { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(false)]
        public bool Nullable { get; set; } = false;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        [DefaultValue(false)]
        public bool BusinessKey { get; set; }
    }
}