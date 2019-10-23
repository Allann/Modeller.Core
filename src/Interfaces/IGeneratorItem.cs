using System;

namespace Hy.Modeller.Interfaces
{
    public interface IGeneratorItem
    {
        string AbbreviatedFileName { get; }
        string FilePath { get; }
        IMetadata Metadata { get; }
        Type Type { get; }

        IGenerator Instance(params object[] args);
    }
}
