using Hy.Modeller.Base.Models;
using Hy.Modeller.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hy.Modeller.Generator
{
    public class GeneratorLoader : IGeneratorLoader
    {
        private class TempGeneratorDetail : IMetadata
        {
            public TempGeneratorDetail(string name, string description, Type entryPoint, IEnumerable<Type> subGenerators, GeneratorVersion version)
            {
                Name = name;
                Description = description;
                EntryPoint = entryPoint;
                SubGenerators = subGenerators;
                Version = version;
            }
            public string Name { get; }
            public string Description { get; }
            public Type EntryPoint { get; }
            public IEnumerable<Type> SubGenerators { get; }
            public bool IsAlphaRelease { get; }
            public bool IsBetaRelease { get; }
            public GeneratorVersion Version { get; }
        }

        private IEnumerable<GeneratorItem> Process(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                filePath = Defaults.LocalFolder;

            var local = new DirectoryInfo(filePath);
            var list = new List<GeneratorItem>();
            if (!local.Exists)
                return list;

            AddFiles(list, local);
            return list;
        }

        private static void AddFiles(List<GeneratorItem> list, DirectoryInfo folder)
        {
            foreach (var subFolder in folder.GetDirectories())
            {
                AddFiles(list, subFolder);
            }

            var asmLoader = new AssemblyLoader(folder.FullName);
            foreach (var file in folder.GetFiles("*.dll"))
            {
                var deps = file.FullName.Substring(0, file.FullName.Length - 3) + "deps.json";
                if (!File.Exists(deps))
                    continue;

                var ass = asmLoader.Load(file.FullName);
                var dt = ass.DefinedTypes;
                var metaDataTypes = dt.Where(t => t.ImplementedInterfaces.Any(it => it.FullName == "Hy.Modeller.Interfaces.IMetadata"));
                foreach (var type in metaDataTypes)
                {
                    if (type.IsAbstract || type.IsInterface || !type.IsPublic)
                        continue;

                    var obj = Activator.CreateInstance(type);
                    if (obj == null)
                        continue;

                    if (obj is IMetadata instance)
                    {
                        var entryPoint = instance.EntryPoint;
                        if (entryPoint == null)
                        {
                            continue;
                        }
                        list.Add(new GeneratorItem(instance, file.FullName, entryPoint));
                    }
                    else
                    {
                        var name = type.GetProperty("Name")?.GetValue(obj).ToString();
                        var description = type.GetProperty("Description")?.GetValue(obj).ToString();
                        var entryPoint = type.GetProperty("EntryPoint")?.GetValue(obj) as Type;
                        var subGenerators = type.GetProperty("SubGenerators")?.GetValue(obj) as IEnumerable<Type>;
                        var version = type.GetProperty("Version")?.GetValue(obj) as GeneratorVersion;

                        if (string.IsNullOrEmpty(name) || entryPoint == null)
                            continue;

                        var md = new TempGeneratorDetail(name, description, entryPoint, subGenerators, version);
                        list.Add(new GeneratorItem(md, file.FullName, entryPoint));
                    }
                }
            }
        }

        IEnumerable<GeneratorItem> IGeneratorLoader.Load(string filePath)
        {
            return Process(filePath);
        }

        bool IGeneratorLoader.TryLoad(string filePath, out IEnumerable<GeneratorItem> generators)
        {
            try
            {
                generators = Process(filePath);
                return true;
            }
            catch
            {
                generators = null;
                return false;
            }
        }
    }
}
