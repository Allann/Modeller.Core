using Hy.Modeller.Base.Models;
using Hy.Modeller.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hy.Modeller.Generator
{
    public class Presenter : IPresenter
    {
        private readonly IGeneratorLoader _generatorLoader;
        private readonly ILogger<IPresenter> _logger;

        public Presenter(IGeneratorConfiguration generatorConfiguration, IGeneratorLoader generatorLoader, ILogger<IPresenter> logger)
        {
            GeneratorConfiguration = generatorConfiguration ?? throw new ArgumentNullException(nameof(generatorConfiguration));
            _generatorLoader = generatorLoader ?? throw new ArgumentNullException(nameof(generatorLoader));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IGeneratorConfiguration GeneratorConfiguration { get; }

        public void Display()
        {
            var folder = System.IO.Path.Combine(GeneratorConfiguration.LocalFolder, GeneratorConfiguration.Target);
            _logger.LogInformation("Available generators");
            _logger.LogInformation($"  location: {folder}");

            var table = Process(folder);
            var sb = new StringBuilder();
            sb.AppendLine();
            foreach (var row in table)
            {
                foreach (var cell in row)
                    sb.Append(cell);
                sb.AppendLine();
            }
            _logger.LogInformation(sb.ToString());
        }

        private IEnumerable<IEnumerable<string>> Process(string folder)
        {
            var rows = new List<List<string>>();

            if (!_generatorLoader.TryLoad(folder, out var generators))
                return rows;

            foreach (var generator in generators)
            {
                var abbr = FileHelper.GetAbbreviatedFilename(generator.FilePath);
                var m = generator.Metadata;
                var vers = m.Version == null ? new GeneratorVersion().ToString() : m.Version.ToString();
                if (GeneratorConfiguration.Verbose)
                {
                    rows.Add(new List<string> { abbr.filename, m.Name, vers, m.Description });
                    foreach (var item in generator.Metadata.SubGenerators)
                        rows.Add(new List<string> { "", item.Name });
                }
                else
                    rows.Add(new List<string> { abbr.filename, m.Name, vers });
            }

            if (rows.Any())
            {
                var cols = GeneratorConfiguration.Verbose ? 3 : 2;
                var widths = new List<int>(cols);
                for (var col = 0; col < cols; col++)
                    widths.Add(rows.Max(l => l[col].Length));

                foreach (var row in rows)
                {
                    for (var col = 0; col < cols; col++)
                        row[col] = row[col].PadRight(widths[col]) + " | ";
                }
            }

            return rows;
        }
    }
}
