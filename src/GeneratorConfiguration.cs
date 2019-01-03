using Hy.Modeller.Interfaces;
using System.Collections.Generic;

namespace Hy.Modeller
{
    public class GeneratorConfiguration : IGeneratorConfiguration
    {
        private string _localFolder;
        private string _target;

        public string SourceModel { get; set; }

        public string OutputPath { get; set; }

        public string Target
        {
            get => string.IsNullOrWhiteSpace(_target) ? Defaults.Target : _target;
            set => _target = value;
        }

        public string GeneratorName { get; set; }

        public string Version { get; set; }

        public string LocalFolder
        {
            get => string.IsNullOrWhiteSpace(_localFolder) ? Defaults.LocalFolder : _localFolder;
            set => _localFolder = value;
        }

        public string ServerFolder { get; set; }

        public bool Verbose { get; set; }

        public string ModelName { get; set; }

        public string SettingsFile { get; set; }

        public List<Package> Packages { get; set; } = new List<Package>();
    }
}