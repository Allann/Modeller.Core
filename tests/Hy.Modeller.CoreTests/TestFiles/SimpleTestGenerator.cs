using Hy.Modeller.Domain;
using Hy.Modeller.Generator;
using Hy.Modeller.Interfaces;

namespace Hy.Modeller.Tests.TestFiles
{
    public class SimpleTestGenerator : IGenerator
    {
        private readonly Module _module;

        public SimpleTestGenerator(ISettings settings, Module module)
        {
            Settings = settings;
            _module = module;
        }

        public ISettings Settings { get; }

        IOutput IGenerator.Create() => new Snippet($"Snippet Content for {_module.Project}");
    }
}
