using Hy.Modeller.Interfaces;
using System;

namespace Hy.Modeller.Outputs
{
    public class Creator
    {
        private readonly IFileWriter _fileWriter;
        private readonly IFileCreator _creator;
        private readonly Action<string> _output;

        public Creator(IContext context, IFileWriter fileWriter, IFileCreator creator, Action<string> output = null)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _fileWriter = fileWriter ?? throw new ArgumentNullException(nameof(fileWriter));
            _creator = creator ?? throw new ArgumentNullException(nameof(creator));
            _output = output;
        }

        public IContext Context { get; }

        public void Create(IOutput output)
        {
            var outputPath = Context.GeneratorConfiguration.OutputPath;
            if (string.IsNullOrWhiteSpace(outputPath))
                outputPath = Defaults.OutputFolder;

            _output?.Invoke($"Creating: {outputPath}");

            var file = _creator.Create(output);
            _fileWriter.Write(file);
            _output?.Invoke("Generation complete.");
        }
    }
}