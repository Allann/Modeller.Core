using Hy.Modeller.Interfaces;
using System;

namespace Hy.Modeller.Outputs
{
    public class CreateFileGroup : IFileCreator
    {
        private readonly IFileWriter _fileWriter;

        public CreateFileGroup(IFileWriter fileWriter)
        {
            _fileWriter = fileWriter ?? throw new ArgumentNullException(nameof(fileWriter));
        }

        public Type SupportedType => typeof(IFileGroup);

        public void Create(IOutput output, IGeneratorConfiguration generatorConfiguration) 
        {
            if (!(output is IFileGroup fileGroup))
                throw new NotSupportedException($"{nameof(CreateFileGroup)} only supports {SupportedType.FullName} output types.");

            if (string.IsNullOrWhiteSpace(output.Name))
                fileGroup.SetPath(generatorConfiguration.OutputPath);
            else if (!System.IO.Path.IsPathRooted(output.Name))
                fileGroup.SetPath(System.IO.Path.Combine(generatorConfiguration.OutputPath, output.Name));

            foreach (var file in fileGroup.Files)
            {
                var fileOutput = new CreateFile(_fileWriter);
                fileOutput.Create(file, generatorConfiguration);
            }
        }
    }
}