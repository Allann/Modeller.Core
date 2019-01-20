using Hy.Modeller.Models;

namespace Hy.Modeller.Interfaces
{
    public interface IModuleLoader
    {
        Module Load(string filePath);
        bool TryLoad(string filePath, out Module module);
    }
}
