using Microsoft.Extensions.Logging;
using Hy.Modeller.Generator.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Hy.Modeller.Interfaces;

namespace Hy.Modeller.Generator
{
    public class PackageService : IPackageService
    {
        private readonly ILoader<IEnumerable<IPackage>> _loader;
        private readonly ILogger<IPackageService> _logger;
        private readonly List<IPackage> _items = new List<IPackage>();

        public PackageService(ILoader<IEnumerable<IPackage>> loader, ILogger<IPackageService> logger)
        {
            _loader = loader;
            _logger = logger;
        }

        public void Refresh(string targetFile)
        {
            _logger.LogInformation($"Using Package file: {targetFile}");

            var d = new FileInfo(targetFile);
            if (!d.Exists) return;
            
            _items.Clear();
            if (_loader.TryLoad(targetFile, out var packages))
            {
                _items.AddRange(packages);
                _logger.LogInformation($"Loaded {packages.Count()} packages");
            }
        }

        IEnumerable<IPackage> IPackageService.Items
        {
            get
            {
                return new ReadOnlyCollection<IPackage>(_items);
            }
        }
    }
}
