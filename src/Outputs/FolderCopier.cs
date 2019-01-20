﻿using System;
using Hy.Modeller.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hy.Modeller.Outputs
{
    public class FolderCopier : IFileCreator
    {
        private readonly ILogger<IFileCreator> _logger;

        public FolderCopier(ILogger<IFileCreator> logger)
        {
            _logger = logger;
        }

        public Type SupportedType => typeof(IFolderCopy);

        public void Create(IOutput output, IGeneratorConfiguration generatorConfiguration) 
        {
            if (!(output is IFolderCopy folderCopy))
                throw new NotSupportedException($"{nameof(CreateFile)} only supports {SupportedType.FullName} output types.");

            var basePath = generatorConfiguration.OutputPath;
            var overwrite = generatorConfiguration.Overwrite;

            if (!System.IO.Path.IsPathRooted(folderCopy.Destination))
            {
                folderCopy.SetPath(!string.IsNullOrWhiteSpace(folderCopy.Destination) ? System.IO.Path.Combine(basePath, folderCopy.Destination) : basePath);
            }

            try
            {
                var s = new System.IO.DirectoryInfo(folderCopy.Source);
                if (!s.Exists)
                    return;

                var d = new System.IO.DirectoryInfo(folderCopy.Destination);
                if (!overwrite && d.Exists)
                {
                    _logger.LogInformation($"Copy: {folderCopy.Source} skipped.");
                }
                else
                {
                    if (!d.Exists)
                        d.Create();
                    foreach (var file in s.GetFiles())
                    {
                        file.CopyTo(System.IO.Path.Combine(d.FullName, file.Name), overwrite);
                    }
                    _logger.LogInformation($"Copy: {folderCopy.Source} -> {folderCopy.Destination} - success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Copy: {folderCopy.Source} -> {folderCopy.Destination} - failed.");
            }
        }
    }
}