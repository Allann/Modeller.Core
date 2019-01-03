using Hy.Modeller.GeneratorBase;
using Hy.Modeller.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Hy.Modeller
{
    public static class FileHelper
    {
        public static string GetAbbreviatedFilename(string filePath)
        {
            if (filePath == null)
                return null;

            var filename = Path.GetFileNameWithoutExtension(filePath);
            var idx = filename.IndexOf('.');
            if (idx > -1)
            {
                filename = filename.Substring(0, idx);
            }
            return filename;
        }

        public static void Write(this IFile file, bool overwrite = false)
        {
            var dir = new DirectoryInfo(file.Path);
            var filename = Path.Combine(dir.FullName, file.Name);
            if (!dir.Exists)
            {
                Directory.CreateDirectory(dir.FullName);
                File.WriteAllText(filename, file.Content);
            }
            else
            {
                var existing = new FileInfo(filename);
                if (existing.Exists)
                {
                    if (overwrite)
                    {
                        File.WriteAllText(filename, file.Content);
                    }
                }
                else
                {
                    File.WriteAllText(filename, file.Content);
                }
            }
        }

        public static bool UpdateLocalGenerators(string serverFolder = null, string localFolder = null, bool overwrite = false, Action<string> output = null)
        {
            if (string.IsNullOrWhiteSpace(localFolder))
            {
                localFolder = Defaults.LocalFolder;
            }
            //if (string.IsNullOrWhiteSpace(serverFolder))
            //{
            //    serverFolder = Defaults.ServerFolder;
            //}

            var server = new DirectoryInfo(serverFolder);
            var local = new DirectoryInfo(localFolder);

            if (!server.Exists)
            {
                return false;
            }

            DirectoryCopy(server, local, true, overwrite, output);
            return true;
        }

        private static void DirectoryCopy(DirectoryInfo sourceDirName, DirectoryInfo destDirName, bool copySubDirs, bool overwrite, Action<string> output)
        {
            if (!sourceDirName.Exists)
            {
                throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
            }

            var dirs = sourceDirName.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!destDirName.Exists)
            {
                output?.Invoke($"creating {destDirName.FullName}");
                destDirName.Create();
            }

            // Get the files in the directory and copy them to the new location.
            var files = sourceDirName.GetFiles();
            foreach (var file in files)
            {
                var temppath = Path.Combine(destDirName.FullName, file.Name);
                if (File.Exists(temppath) && !overwrite)
                {
                    continue;
                }

                output?.Invoke($"copying {file.Name} to {destDirName.Name}");
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (var subdir in dirs)
                {
                    var temppath = new DirectoryInfo(Path.Combine(destDirName.FullName, subdir.Name));
                    DirectoryCopy(subdir, temppath, copySubDirs, overwrite, output);
                }
            }
        }

    }
}