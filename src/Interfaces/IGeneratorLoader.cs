using System.Collections.Generic;

namespace Hy.Modeller.Interfaces
{
    public interface IGeneratorLoader
    {
        IEnumerable<IGeneratorItem> Load(string filePath);
        bool TryLoad(string filePath, out IEnumerable<IGeneratorItem> generators);
    }
}
