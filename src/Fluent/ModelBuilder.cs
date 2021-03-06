﻿using Hy.Modeller.Domain;
using System;
using System.ComponentModel;

namespace Hy.Modeller.Fluent
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ModelBuilder : FluentBase
    {
        public ModelBuilder(ModuleBuilder module, Domain.Model model)
        {
            Build = module ?? throw new ArgumentNullException(nameof(module));
            Instance = model ?? throw new ArgumentNullException(nameof(model));
        }

        public ModuleBuilder Build { get; }

        public Domain.Model Instance { get; }

        public ModelBuilder Name(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Instance.Name.SetName(name);
            return this;
        }

        public ModelBuilder IsAuditable(bool value)
        {
            Instance.HasAudit = value;
            return this;
        }

        public ModelBuilder WithDefaultKey()
        {
            Instance.Key.Fields.Add(new Domain.Field("Id") { DataType = DataTypes.UniqueIdentifier, Nullable = false });
            return this;
        }

        public ModelBuilder Schema(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Instance.Schema = name;
            return this;
        }

        public KeyBuilder WithKey()
        {
            var key = Fluent.Key.Create(this, Instance);
            return key;
        }

        public FieldBuilder<ModelBuilder> AddField(string name)
        {
            var field = Field<ModelBuilder>.Create(this, name);
            Instance.Fields.Add(field.Instance);
            return field;
        }

        public RelationshipBuilder AddRelationship()
        {
            var relation = Fluent.Relationship.Create(Build, this);
            Instance.Relationships.Add(relation.Instance);
            return relation;
        }

        public IndexBuilder AddIndex(string name)
        {
            var index = Index.Create(this, name);
            Instance.Indexes.Add(index.Instance);
            return index;
        }
    }
}
