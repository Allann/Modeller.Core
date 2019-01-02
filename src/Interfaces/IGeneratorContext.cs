using System.Collections.Generic;

namespace Hy.Modeller.Interfaces
{
    public interface IGeneratorContext
    {
        string Generator { get; set; }
        string LocalFolder { get; set; }
        string ModelName { get; set; }
        string OutputPath { get; set; }
        List<Package> Packages { get; set; }
        string ServerFolder { get; set; }
        string Settings { get; set; }
        string SourceModel { get; set; }
        string Target { get; set; }
        bool Verbose { get; set; }
        string Version { get; set; }
    }
}