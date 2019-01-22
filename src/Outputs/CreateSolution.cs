﻿using Hy.Modeller.Interfaces;
using System;

namespace Hy.Modeller.Outputs
{
    public class CreateSolution : IFileCreator
    {
        private readonly IFileWriter _fileWriter;

        public CreateSolution(IFileWriter fileWriter)
        {
            _fileWriter = fileWriter ?? throw new ArgumentNullException(nameof(fileWriter));
        }

        public Type SupportedType => typeof(ISolution);

        public void Create(IOutput output, IGeneratorConfiguration generatorConfiguration)
        {
            if (!(output is ISolution solution))
                throw new NotSupportedException($"{nameof(CreateSolution)} only supports {SupportedType.FullName} output types.");

            if (!string.IsNullOrWhiteSpace(solution.Directory))
            {
                if (!System.IO.Path.IsPathRooted(solution.Directory))
                    solution.Directory = System.IO.Path.Combine(generatorConfiguration.OutputPath, solution.Directory);
            }

            foreach (var file in solution.Files)
            {
                var fileOutput = new CreateFile(_fileWriter);
                fileOutput.Create(file, generatorConfiguration);
            }

            foreach (var project in solution.Projects)
            {
                var projectOutput = new CreateProject(_fileWriter);
                projectOutput.Create(project, generatorConfiguration);
            }
        }
    }
}
