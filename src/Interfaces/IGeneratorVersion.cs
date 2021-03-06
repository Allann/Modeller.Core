﻿using System;

namespace Hy.Modeller.Interfaces
{
    public interface IGeneratorVersion
    {
        bool IsAlphaRelease { get; set; }
        bool IsBetaRelease { get; set; }
        bool IsRelease { get; set; }
        Version Version { get; set; }
    }
}
