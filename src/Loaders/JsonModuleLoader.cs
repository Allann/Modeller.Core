using Hy.Modeller.Domain;
using Hy.Modeller.Domain.Extensions;
using Hy.Modeller.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace Hy.Modeller.Loaders
{
    public class JsonModuleLoader : ILoader<IEnumerable<INamedElement>>
    {
        private Module Process(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Module file '{filePath}' does not exist.");

            string model;
            using (var reader = File.OpenText(filePath))
            {
                model = reader.ReadToEnd();
            }
            return model.FromJson<Module>();
        }

        IEnumerable<INamedElement> ILoader<IEnumerable<INamedElement>>.Load(string filePath)
        {
            return new[] { Process(filePath) };
        }

        bool ILoader<IEnumerable<INamedElement>>.TryLoad(string filePath, out IEnumerable<INamedElement> modules)
        {
            try
            {
                modules = new[] { Process(filePath) };
                return true;
            }
            catch
            {
                modules = new List<INamedElement>();
                return false;
            }
        }
    }
}
