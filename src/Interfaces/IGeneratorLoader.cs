using System.Collections.Generic;

namespace Hy.Modeller.Interfaces
{
    public interface IGeneratorLoader
    {
        IEnumerable<GeneratorItem> Load(string filePath);
        bool TryLoad(string filePath, out IEnumerable<GeneratorItem> generators);
    }
}
