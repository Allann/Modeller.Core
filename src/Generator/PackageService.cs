using Hy.Modeller.Interfaces;
using Modeller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Hy.Modeller.Generator
{
    public class PackageService
    {
        private readonly IPackageFileLoader _loader;
        private string _target;
        private readonly IDictionary<string, IEnumerable<Package>> _items = new Dictionary<string, IEnumerable<Package>>();
        private string _folder;
        private string _defaultFolder;

        public PackageService(IPackageFileLoader loader)
        {
            _loader = loader ?? throw new ArgumentNullException(nameof(loader));
            _defaultFolder = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath);
        }

        public string Target
        {
            get => string.IsNullOrWhiteSpace(_target) ? Defaults.Target : _target;
            set => _target = value;
        }

        public string Folder
        {
            get => string.IsNullOrWhiteSpace(_folder)  ? _defaultFolder : _folder;
            set => _folder = value;
        }

        public void Refresh()
        {
            var d = new DirectoryInfo(Folder);
            if (!d.Exists) return;

            var path = Path.Combine(d.FullName, Target + ".json");
            if (_loader.TryLoad(path, out var packages))
            {
                if (!_items.ContainsKey(Target))
                    _items.Add(Target, packages);
            }
        }

        public IEnumerable<Package> Items
        {
            get
            {
                if (!_items.ContainsKey(Target))
                    Refresh();
                if (!_items.ContainsKey(Target))
                {
                    var path = Path.Combine(Folder, Target + ".json");
                    throw new MissingTargetException(Target, $"Missing target file {path}");
                }
                return _items[Target];
            }
        }
    }
}