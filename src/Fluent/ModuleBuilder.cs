﻿using Hy.Modeller.Domain;
using Humanizer;
using System;
using System.ComponentModel;

namespace Hy.Modeller.Fluent
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ModuleBuilder : FluentBase
    {
        public ModuleBuilder(Domain.Module module)
        {
            Build = module ?? throw new ArgumentNullException(nameof(module));
        }

        public Domain.Module Build { get; }

        public ModuleBuilder DefaultSchema(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Build.DefaultSchema = name;
            return this;
        }

        public ModuleBuilder FeatureName(string name)
        {
            Build.Feature = string.IsNullOrWhiteSpace(name) ? null : new Name(name);
            return this;
        }

        public ModelBuilder AddModel(string name)
        {
            var model = Fluent.Model.Create(this, name);
            Build.Models.Add(model.Instance);
            return model;
        }

        public EnumerationBuilder AddEnumeration(string name)
        {
            var enumeration = Fluent.Enumeration.Create(this, name);
            Build.Enumerations.Add(enumeration.Instance);
            return enumeration;
        }

        public RequestBuilder AddRequest(string name)
        {
            var request = Fluent.Request.Create(this, name);
            Build.Requests.Add(request.Instance);
            return request;
        }

    }
}
