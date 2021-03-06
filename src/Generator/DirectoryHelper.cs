﻿using System;
using System.IO;

namespace Hy.Modeller.Generator
{
    public static class DirectoryHelper
    {
        public static string MergePaths(string current, string proposed)
        {
            if (string.IsNullOrWhiteSpace(proposed))
                return current;

            if (string.IsNullOrWhiteSpace(current))
                return proposed;

            current = current.TrimEnd('\\');
            proposed = proposed.TrimStart('\\');

            for (var i = proposed.Length; i > 0; i--)
            {
                if (current.EndsWith(proposed.Substring(0, i), StringComparison.InvariantCultureIgnoreCase))
                    return Path.Combine(current, proposed.Substring(i).TrimStart('\\'));
            }
            return Path.Combine(current, proposed);
        }
    }
}
