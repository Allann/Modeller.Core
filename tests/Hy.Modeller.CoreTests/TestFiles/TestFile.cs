﻿using Hy.Modeller.Interfaces;

namespace Hy.Modeller.Tests.TestFiles
{
    internal class TestFile : IFile
    {
        private readonly string _name;
        public TestFile()
        {
            _name = System.IO.Path.GetTempFileName();
        }

        string IFile.Content { get; set; }
        string IFile.Path { get; set; }
        string IFile.FullName { get; }
        bool IFile.CanOverwrite { get; set; }
        string IOutput.Name => _name;
    }
}
