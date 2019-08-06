using Hy.Modeller.GeneratorBase;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Hy.Modeller.CoreTests.TestGenenerators
{
    public class SimpleTestGeneratorDetails : MetadataBase
    {
        public override string Name => "SimpleTestGenerator";
        public override string Description => "A simple test generator";
        public override Type EntryPoint => typeof(SimpleTestGenerator);
        public override IEnumerable<Type> SubGenerators => new Collection<Type>();
    }

    public class SimpleTestGenerator : IGenerator
    {
        private readonly Module _module;

        public SimpleTestGenerator(ISettings settings, Module module )
        {
            Settings = settings;
            _module = module;
        }

        public ISettings Settings { get; }

        IOutput IGenerator.Create() => new Outputs.Snippet($"Snippet Content for {_module.Project}");
    }
}
