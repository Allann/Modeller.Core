﻿using Hy.Modeller.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Hy.Modeller.Generator
{
    public class Updater : IUpdater
    {
        private readonly IGeneratorConfiguration _generatorConfiguration;
        private readonly ILogger<IUpdater> _logger;
        private int _affected;

        IGeneratorConfiguration IUpdater.GeneratorConfiguration => _generatorConfiguration;

        public Updater(IGeneratorConfiguration generatorConfiguration, ILogger<IUpdater> logger)
        {
            _generatorConfiguration = generatorConfiguration ?? throw new ArgumentNullException(nameof(generatorConfiguration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        void IUpdater.Refresh() 
        {
            _affected = 0;
            _logger.LogInformation($"Updating generator files for target: {_generatorConfiguration.Target}");
            _logger.LogInformation($"Server Folder: {_generatorConfiguration.ServerFolder}");
            _logger.LogInformation($"Local Folder: {_generatorConfiguration.LocalFolder}");
            _logger.LogInformation($"Overwrite: {_generatorConfiguration.Overwrite}");

            if (UpdateLocalGenerators())
            {
                _logger.LogInformation($"Update completed successfully. Files affected: {_affected}");
            }
            else
            {
                _logger.LogInformation($"Update failed. Files affected: {_affected}");
            }
        }

        private bool UpdateLocalGenerators()
        {
            var server = new DirectoryInfo(_generatorConfiguration.ServerFolder);
            var local = new DirectoryInfo(_generatorConfiguration.LocalFolder);

            if (!server.Exists)
            {
                _logger.LogWarning($"Server Folder '{server.FullName}' not found.");
                return false;
            }

            DirectoryCopy(server, local);
            return true;
        }

        private void DirectoryCopy(DirectoryInfo sourceDirName, DirectoryInfo destDirName)
        {
            if (!sourceDirName.Exists)
            {
                return;
            }

            var dirs = sourceDirName.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!destDirName.Exists)
            {
                if (_generatorConfiguration.Verbose)
                    _logger.LogInformation($"creating {destDirName.FullName}");
                destDirName.Create();
            }

            // Get the files in the directory and copy them to the new location.
            var files = sourceDirName.GetFiles();
            foreach (var file in files)
            {
                var temppath = Path.Combine(destDirName.FullName, file.Name);
                if (File.Exists(temppath) && !_generatorConfiguration.Overwrite)
                {
                    if (_generatorConfiguration.Verbose)
                        _logger.LogInformation($"skipping {file.Name}");
                    continue;
                }

                if (_generatorConfiguration.Verbose)
                    _logger.LogInformation($"copying {file.Name} to {destDirName.Name}");
                file.CopyTo(temppath, _generatorConfiguration.Overwrite);
                _affected++;
            }

            // copy Sub-directories and their contents to new location.
            foreach (var subdir in dirs)
            {
                var temppath = new DirectoryInfo(Path.Combine(destDirName.FullName, subdir.Name));
                DirectoryCopy(subdir, temppath);
            }
        }
    }
}