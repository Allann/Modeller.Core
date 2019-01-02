using Hy.Modeller.Core.Outputs;
using Hy.Modeller.Interfaces;
using System;

namespace Hy.Modeller.Outputs
{
    internal class CreateSnippet
    {
        private readonly IFileWriter _writer;
        private readonly IFileCreator _fileCreator;
        private readonly ISnippet _snippet;
        private readonly Action<string> _output;

        public CreateSnippet(IFileWriter writer, IFileCreator filecreator, ISnippet snippet, Action<string> output = null)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _fileCreator = filecreator ?? throw new ArgumentNullException(nameof(filecreator));
            _snippet = snippet ?? throw new ArgumentNullException(nameof(snippet));
            _output = output;
        }

        public void Create(string basePath)
        {
            var outputPath = basePath;

            string filename;
            if (string.IsNullOrWhiteSpace(_snippet.Name))
            {
                filename = System.IO.Path.GetFileNameWithoutExtension(System.IO.Path.GetTempFileName());
            }
            else
                filename = _snippet.Name;
            var ext = System.IO.Path.GetExtension(filename);
            if (string.IsNullOrEmpty(ext) || ext != ".txt")
                filename += ".txt";

            var f = _fileCreator.Create(_snippet);
            f.Path = System.IO.Path.Combine(outputPath, "Snippets");

            var fileOutput = new CreateFile(_writer, f, _output);
            fileOutput.Create(basePath);
        }
    }
}
