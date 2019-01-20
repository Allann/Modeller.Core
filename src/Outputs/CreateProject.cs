using Hy.Modeller.Interfaces;
using System;

namespace Hy.Modeller.Outputs
{
    public class CreateProject : IFileCreator
    {
        private readonly IFileWriter _fileWriter;

        public CreateProject(IFileWriter fileWriter)
        {
            _fileWriter = fileWriter ?? throw new ArgumentNullException(nameof(fileWriter));
        }

        public Type SupportedType => typeof(IProject);

        public void Create(IOutput output, IGeneratorConfiguration generatorConfiguration)
        {
            if (!(output is IProject project))
                throw new NotSupportedException($"{nameof(CreateProject)} only supports {SupportedType.FullName} output types.");

            var path = System.IO.Path.IsPathRooted(project.Path) ? project.Path : System.IO.Path.Combine(generatorConfiguration.OutputPath, project.Path);

            foreach (var fg in project.FileGroups)
            {
                var groupPath = string.IsNullOrWhiteSpace(fg.Name) ? path : System.IO.Path.Combine(path, fg.Name);
                foreach (var file in fg.Files)
                {
                    var filePath = (string.IsNullOrWhiteSpace(file.Path) || groupPath.Contains(file.Path)) ? groupPath : System.IO.Path.Combine(groupPath, file.Path);
                    file.Path = filePath;

                    var fileOutput = new CreateFile(_fileWriter);
                    fileOutput.Create(file, generatorConfiguration);
                }
            }
        }
    }
}