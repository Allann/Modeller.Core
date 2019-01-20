using System.Collections.Generic;

namespace Hy.Modeller.Interfaces
{
    public interface IPackageService
    {
        IEnumerable<Package> Items { get; }

        void Refresh(IContext context);
    }
}