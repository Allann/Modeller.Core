using Hy.Modeller.GeneratorBase;
using Hy.Modeller.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
        ISettings IGenerator.Settings { get; }

        IOutput IGenerator.Create() => new Outputs.Snippet("Snippet Conent");
    }
}
