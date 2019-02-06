using Hy.Modeller.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hy.Modeller.Outputs
{
    public class OutputStrategy : IOutputStrategy
    {
        private readonly IEnumerable<IFileCreator> _creators;

        public OutputStrategy(IEnumerable<IFileCreator> creators)
        {
            _creators = creators ?? throw new ArgumentNullException(nameof(creators));
        }

        public void Create(IOutput output, IGeneratorConfiguration generatorConfiguration)
        {
            var creator = _creators.FirstOrDefault(c => c.SupportedType.IsAssignableFrom(output.GetType()));
            if (creator == null)
                throw new InvalidOperationException($"No IFileCreator implementation registered for {output.GetType().Name}");

            if (generatorConfiguration?.OutputPath != null)
            {
                var s = new System.IO.DirectoryInfo(generatorConfiguration.OutputPath);
                if (!s.Exists)
                    s.Create();
            }

            creator.Create(output, generatorConfiguration);
        }
    }
}
