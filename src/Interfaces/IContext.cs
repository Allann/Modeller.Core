using FluentValidation.Results;
using Hy.Modeller.Base.Models;
using Hy.Modeller.Models;

namespace Hy.Modeller.Interfaces
{
    public interface IContext
    {
        IGeneratorConfiguration GeneratorConfiguration { get; }
        IGeneratorItem Generator { get; }
        Model Model { get; }
        Module Module { get;  }
        ISettings Settings { get; }
        GeneratorVersion Version { get; }
        string TargetFolder { get; }

        ValidationResult ProcessConfiguration();
    }
}