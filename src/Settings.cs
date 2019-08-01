using Hy.Modeller.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hy.Modeller
{
    public class Settings : ISettings
    {
        public Settings(IGeneratorConfiguration context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        string ISettings.GetPackageVersion(string name)
        {
            if (Context.Packages == null || !Context.Packages.Any())
                return "";
            var found = Context.Packages.FirstOrDefault(p => string.Equals(p.Name, name));
            return found == null ? "" : found.Version;
        }

        void ISettings.RegisterPackage(Package package)
        {
            Register(package);
        }

        void ISettings.RegisterPackage(string name, string version) => Register(new Package(name, version));

        void ISettings.RegisterPackages(IEnumerable<Package> packages)
        {
            foreach (var item in packages)
            {
                Register(item);
            }
        }

        private void Register(Package package)
        {
            if (package == null || string.IsNullOrWhiteSpace(package.Name) || string.IsNullOrWhiteSpace(package.Version))
                return;

            var packages = Context.Packages.Where(p => p.Name == package.Name);
            if (!packages.Any())
            {
                Context.Packages.Add(package);
                return;
            }
            if (packages.Count() == 1)
            {
                var p = packages.First();
                if (Version.TryParse(p.Version, out var p1))
                {
                    if (Version.TryParse(package.Version, out var p2))
                    {
                        if (p1 < p2)
                            p.Version = p2.ToString();
                    }
                }
                return;
            }
        }

        bool ISettings.PackagesInitialised() => Context.Packages!=null && Context.Packages.Any();

        public IGeneratorConfiguration Context { get; }

        bool ISettings.SupportRegen { get; set; } = true;

        string ISettings.LocalFolder { get; set; }

        string ISettings.OutputPath { get; set; }

        string ISettings.ServerFolder { get; set; }

        string ISettings.SourceModel { get; set; }

        string ISettings.Target { get; set; }

        string ISettings.Version { get; set; }
    }
}