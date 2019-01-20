using System;
using Hy.Modeller.Interfaces;
using Microsoft.Extensions.Logging;

namespace Hy.Modeller.Outputs
{
    public class FileCopier : IFileCreator
    {
        private readonly ILogger<IFileCreator> _logger;

        public FileCopier(ILogger<IFileCreator> logger)
        {
            _logger = logger;
        }

        public Type SupportedType => typeof(IFileCopy);

        public void Create(IOutput output, IGeneratorConfiguration generatorConfiguration) 
        {
            if (!(output is IFileCopy fileCopy))
                throw new NotSupportedException($"{nameof(CreateFile)} only supports {SupportedType.FullName} output types.");

            var basePath = generatorConfiguration.OutputPath;
            var overwrite = generatorConfiguration.Overwrite;

            if (!System.IO.Path.IsPathRooted(fileCopy.Destination))
            {
                fileCopy.Destination = !string.IsNullOrWhiteSpace(fileCopy.Destination) ? System.IO.Path.Combine(basePath, fileCopy.Destination) : basePath;
            }

            try
            {
                if (!overwrite && System.IO.File.Exists(fileCopy.Destination))
                    _logger.LogInformation($"Copy: {fileCopy.Source} skipped.");
                else
                {
                    System.IO.File.Copy(fileCopy.Source, fileCopy.Destination, overwrite);
                    _logger.LogInformation($"Copy: {fileCopy.Source} -> {fileCopy.Destination} - success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Copy: {fileCopy.Source} -> {fileCopy.Destination} - failed.");
            }
        }
    }
}