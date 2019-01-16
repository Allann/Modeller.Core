using FluentValidation.Results;
using Hy.Modeller.Base.Models;
using Hy.Modeller.Core.Validators;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Models;
using System;
using System.Linq;

namespace Hy.Modeller.Generator
{
    public class Context : IContext
    {
        private readonly ISettingsLoader _settingsLoader;
        private readonly IModuleLoader _moduleLoader;
        private readonly IGeneratorLoader _generatorLoader;

        public Context(IGeneratorConfiguration generatorConfiguration, ISettingsLoader settingsLoader, IModuleLoader moduleLoader, IGeneratorLoader generatorLoader)
        {
            GeneratorConfiguration = generatorConfiguration ?? throw new ArgumentNullException(nameof(generatorConfiguration));
            _settingsLoader = settingsLoader ?? throw new ArgumentNullException(nameof(settingsLoader));
            _moduleLoader = moduleLoader ?? throw new ArgumentNullException(nameof(moduleLoader));
            _generatorLoader = generatorLoader ?? throw new ArgumentNullException(nameof(generatorLoader));

            ProcessConfiguration();
        }

        public GeneratorItem Generator { get; set; }

        public Module Module { get; set; }

        public ISettings Settings { get; set; }

        public GeneratorVersion Version { get; set; }

        public Model Model { get; set; }

        public string TargetFolder => !string.IsNullOrWhiteSpace(GeneratorConfiguration.LocalFolder) && !string.IsNullOrWhiteSpace(GeneratorConfiguration.Target) ? System.IO.Path.Combine(GeneratorConfiguration.LocalFolder, GeneratorConfiguration.Target) : null;

        public IGeneratorConfiguration GeneratorConfiguration { get; }

        public ValidationResult ProcessConfiguration()
        {
            var name = GeneratorConfiguration.GeneratorName?.ToLowerInvariant();
            if (!string.IsNullOrEmpty(name))
            {
                if (_generatorLoader.TryLoad(GeneratorConfiguration.LocalFolder, out var generators))
                {
                    var matches = generators.Where(g => g.Metadata.Name.ToLowerInvariant() == name || g.AbbreviatedFileName.ToLowerInvariant() == name);
                    var exact = matches.SingleOrDefault(m => m.Metadata.Version == Version);
                    Generator = exact ?? matches.OrderByDescending(k => k.Metadata.Version).First();
                }
            }

            if (!string.IsNullOrEmpty(GeneratorConfiguration.SettingsFile))
                Settings = _settingsLoader.Load<ISettings>(GeneratorConfiguration.SettingsFile);

            if (!string.IsNullOrEmpty(GeneratorConfiguration.SourceModel))
                Module = _moduleLoader.Load(GeneratorConfiguration.SourceModel);

            var configValidator = new ContextValidator();
            return configValidator.Validate(this);
        }

        internal void SetSettings(ISettings settings)
        {
            Settings = settings;
            if (Settings != null && !Settings.PackagesInitialised())
            {
                var ps = new PackageService(new PackageFileLoader());
                Settings.RegisterPackages(ps.Items);
            }
        }
    }
}