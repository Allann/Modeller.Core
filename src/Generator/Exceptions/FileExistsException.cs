﻿using System;

namespace Hy.Modeller.Generator.Exceptions
{

    public class FileExistsException : ApplicationException
    {
        public string Filename { get; set; }

        public FileExistsException()
        {
            Filename = "unknown";
        }

        public FileExistsException(string filename)
        {
            Filename = filename;
        }

        public FileExistsException(string filename, string message) : base(message)
        {
            Filename = filename;
        }

        public FileExistsException(string filename, string message, Exception innerException) : base(message, innerException)
        {
            Filename = filename;
        }

        public FileExistsException(string message, Exception innerException) : base(message, innerException)
        {
            Filename = "unknown";
        }
    }
}
