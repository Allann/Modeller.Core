using System;

namespace Hy.Modeller.Interfaces
{
    public interface IFileWriter
    {
        void Write(IFile file);
    }

    public interface IFileCreator
    {
        IFile Create(IOutput output);
    }
}
