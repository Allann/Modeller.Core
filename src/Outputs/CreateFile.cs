using Hy.Modeller.Interfaces;
using System;

namespace Hy.Modeller.Outputs
{
    public class CreateFile : IFileCreator
    {
        private readonly IFileWriter _fileWriter;

        public CreateFile(IFileWriter fileWriter)
        {
            _fileWriter = fileWriter ?? throw new ArgumentNullException(nameof(fileWriter));
        }

        public Type SupportedType => typeof(IFile);

        public void Create(IOutput output, IGeneratorConfiguration generatorConfiguration)
        {
            if (!(output is IFile file))
                throw new NotSupportedException($"{nameof(CreateFile)} only supports {SupportedType.FullName} output types.");

            if (string.IsNullOrWhiteSpace(file.Path))
                file.Path = generatorConfiguration.OutputPath;
            else if (!System.IO.Path.IsPathRooted(file.Path))
                file.Path = System.IO.Path.Combine(generatorConfiguration.OutputPath, file.Path);

            //if (!string.IsNullOrWhiteSpace(relativePath))
            //    file.Path = System.IO.Path.Combine(file.Path, relativePath);

            _fileWriter.Write(file);
        }
    }
}