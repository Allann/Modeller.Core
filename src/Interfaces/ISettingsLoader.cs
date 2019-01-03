﻿namespace Hy.Modeller.Interfaces
{
    public interface ISettingsLoader
    {
        ISettings Load<T>(string filePath)
            where T : ISettings;
        bool TryLoad<T>(string filePath, out ISettings module)
            where T : ISettings;
    }
}
