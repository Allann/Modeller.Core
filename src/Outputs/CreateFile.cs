using Hy.Modeller.Core.Outputs;
using Hy.Modeller.Interfaces;
using System;

namespace Hy.Modeller.Outputs
{
    internal class CreateFile
    {
        private readonly IFileWriter _fileWriter;
        private readonly IFile _file;
        private readonly Action<string> _output;

        public CreateFile(IFileWriter fileWriter, IFile file, Action<string> output = null)
        {
            _fileWriter = fileWriter ?? throw new ArgumentNullException(nameof(fileWriter));
            _file = file ?? throw new ArgumentNullException(nameof(file));
            _output = output;
        }

        internal void Create(string basePath)
        {
            if (string.IsNullOrWhiteSpace(_file.Path))
                _file.Path = basePath;
            else if (!System.IO.Path.IsPathRooted(_file.Path))
                _file.Path = System.IO.Path.Combine(basePath, _file.Path);

            _fileWriter.Write(_file);

            _output?.Invoke($"File: {_file.FullName}");
        }
    }
}