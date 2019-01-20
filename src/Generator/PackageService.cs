using Hy.Modeller.Interfaces;
using Microsoft.Extensions.Logging;
using Modeller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Hy.Modeller.Generator
{
    public class PackageService : IPackageService
    {
        private readonly IPackageFileLoader _loader;
        private readonly ILogger<IPackageService> _logger;
        private readonly List<Package> _items = new List<Package>();

        public PackageService(IPackageFileLoader loader, ILogger<IPackageService> logger)
        {
            _loader = loader ?? throw new ArgumentNullException(nameof(loader));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private string Target { get; set; }
        private string TargetFile { get; set; }

        public void Refresh(IContext context)
        {
            _items.Clear();
            Target = context?.GeneratorConfiguration?.Target;
            if (Target == null)
                return;
            var d = new DirectoryInfo(context.TargetFolder);
            if (!d.Exists) return;

            TargetFile = Path.Combine(d.FullName, Target + ".json");
            _logger.LogInformation($"Using Package file: {TargetFile}");

            if (_loader.TryLoad(TargetFile, out var packages))
            {
                _items.AddRange(packages);
            }
        }

        public IEnumerable<Package> Items
        {
            get
            {
                if (!_items.Any())
                    throw new MissingTargetException(Target, $"Missing target file {TargetFile}");
                return new ReadOnlyCollection<Package>(_items);
            }
        }
    }
}