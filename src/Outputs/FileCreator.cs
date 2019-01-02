using Hy.Modeller.Interfaces;
using System;

namespace Hy.Modeller.Core.Outputs
{
    public class FileCreator : IFileCreator
    {
        private readonly Action<string> _output;

        public FileCreator(Action<string> output=null)
        {
            _output = output;
        }

        public IFile Create(IOutput output)
        {
            //if (T)
            //{
            //    //var snippet = new CreateSnippet(_fileWriter, _fileFactory, x, _output);
            //    //snippet.Create(outputPath);
            //}
            //else if (T is IFile f)
            //{
            //    //var file = new CreateFile(_fileWriter, f, _output);
            //    //file.Create(outputPath);
            //}
            //else if (output is IFileGroup fg)
            //{
            //    //var fileGroup = new CreateFileGroup(_fileWriter, fg, _output);
            //    //fileGroup.Create(outputPath);
            //}
            //else if (output is IProject p)
            //{
            //    //var project = new CreateProject(_fileWriter, p, _output);
            //    //project.Create(outputPath);
            //}
            //else if (output is ISolution s)
            //{
            //    //var solution = new CreateSolution(_fileWriter, s, _output);
            //    //solution.Create(outputPath);
            //}
            //else if (output is IFileCopy fc)
            //{
            //    //var copier = new FileCopier(fc, false, _output);
            //    //copier.Copy(outputPath);
            //}
            //else if (output is IFolderCopy fo)
            //{
            //    //var copier = new FolderCopier(fo, false, _output);
            //    //copier.Copy(outputPath);
            //}
            //else
            //    _output?.Invoke($"Output type '{output?.GetType().FullName}' is not supported.");

            return null;
        }
    }

    public class FileWriter : IFileWriter
    {


        public void Write(IFile file) {

        }
    }
}
