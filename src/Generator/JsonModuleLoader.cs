using Hy.Modeller.Extensions;
using Hy.Modeller.Interfaces;
using System.IO;

namespace Hy.Modeller.Generator
{
    public class JsonModuleLoader : IModuleLoader
    {
        private Models.Module Process(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Module file '{filePath}' does not exist.");
            }

            string model;
            using (var reader = File.OpenText(filePath))
            {
                model = reader.ReadToEnd();
            }
            return model.FromJson<Models.Module>();
        }

        Models.Module IModuleLoader.Load(string filePath)
        {
            return Process(filePath);
        }

        bool IModuleLoader.TryLoad(string filePath, out Models.Module module)
        {
            try
            {
                module = Process(filePath);
                return true;
            }
            catch
            {
                module = null;
                return false;
            }
        }
    }
}
