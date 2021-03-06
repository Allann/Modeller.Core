﻿using Hy.Modeller.Domain;
using Hy.Modeller.Domain.Extensions;
using System;
using System.ComponentModel;

namespace Hy.Modeller.Fluent
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class EnumerationBuilder : FluentBase
    {
        public EnumerationBuilder(ModuleBuilder module, Domain.Enumeration enumeration)
        {
            Build = module ?? throw new ArgumentNullException(nameof(module));
            Instance = enumeration ?? throw new ArgumentNullException(nameof(enumeration));
        }

        public ModuleBuilder Build { get; }

        public Domain.Enumeration Instance { get; }

        public EnumerationBuilder Name(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            Instance.Name.SetName(name);
            return this;
        }

        public EnumerationBuilder AsFlag()
        {
            Instance.Flag = true;
            return this;
        }

        public EnumerationBuilder AddItem(string name)
        {
            Instance.AddItem(name);
            return this;
        }
    }
}
