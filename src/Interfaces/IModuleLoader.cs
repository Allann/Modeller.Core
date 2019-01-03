using Hy.Modeller.Models;
using System.Collections.Generic;

namespace Hy.Modeller.Interfaces
{
    public interface IModuleLoader
    {
        Module Load(string filePath);
        bool TryLoad(string filePath, out Module module);
    }

    public interface IGeneratorLoader
    {
        IEnumerable<GeneratorItem> Load(string filePath);
        bool TryLoad(string filePath, out IEnumerable<GeneratorItem> generators);
    }
}
