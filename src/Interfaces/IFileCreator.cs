using System;

namespace Hy.Modeller.Interfaces
{
    public interface IFileCreator
    {
        Type SupportedType { get; }

        void Create(IOutput output, string path = null, bool overwrite = false);
    }

}
