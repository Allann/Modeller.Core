using System.Collections.Generic;

namespace Hy.Modeller.Interfaces
{
    public interface IPackageService
    {
        IEnumerable<IPackage> Items { get; }

        void Refresh(string targetFile);
    }
}
