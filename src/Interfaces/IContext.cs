using System;
using System.Collections.Generic;
using Hy.Modeller.Interfaces;
using Hy.Modeller.Models;

namespace Hy.Modeller.Interfaces
{
    public interface IContext
    {
        IGeneratorConfiguration GeneratorConfiguration { get; }
        GeneratorItem Generator { get; set; }
        Model Model { get; set; }
        Module Module { get; set; }
        ISettings Settings { get; set; }
        Version Version { get; set; }
        string TargetFolder { get; }
    }
}