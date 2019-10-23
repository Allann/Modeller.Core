using System.Collections.Generic;

namespace Hy.Modeller.Interfaces
{
    public interface ISolution : IOutput
    {
        string Directory { get; set; }

        IEnumerable<IFile> Files { get; }

        IEnumerable<IProject> Projects { get; }

        void AddFile(IFile file);

        void AddProject(IProject project);
    }

}
