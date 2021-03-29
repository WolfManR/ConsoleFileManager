using System;
using System.Collections.Generic;
using System.IO;
using ConsoleFileManager.Infrastructure.Commands.FileSystemCLI;
using ConsoleFileManager.Infrastructure.Exceptions;
using ConsoleFileManager.Infrastructure.Extensions;
using ConsoleFileManager.Models;

namespace ConsoleFileManager.Render
{
    public static class FilesManager
    {
        public static string CurrentDirectoryPath { get; set; }
        public static DirectoryInfo CurrentDirectory { get; set; }
        public static List<Info> Infos { get; set; } = new List<Info>();
        public static Info Current { get; set; }


        public static void ChangeDirectory(string path)
        {
            DirectoryInfo newDirectory;
            if (StringPathIsDirectory(path))
            {
                newDirectory = new DirectoryInfo(path);
            }
            else
            {
                Current = new FileInfo(path).ToInfo();
                var directory = Path.GetDirectoryName(path);
                newDirectory = new DirectoryInfo(directory);
            }
            ChangeDirectory(newDirectory);
        }

        public static void ChangeDirectory(Move move, string path = null)
        {
            switch (move)
            {
                case Move.Back:
                    var backDir = CurrentDirectory.Parent;
                    ChangeDirectory(backDir);
                    break;
                case Move.ToRoot:
                    var root = Path.GetPathRoot(CurrentDirectoryPath);
                    ChangeDirectory(new DirectoryInfo(root));
                    break;
                case Move.Inner when path is not null:
                    var next = Path.Combine(CurrentDirectoryPath, path);
                    if (!Path.IsPathFullyQualified(next)) throw ExceptionsFactory.PathNotExist(next);
                    ChangeDirectory(next);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(move), move, null);
            }
        }

        private static void ChangeDirectory(DirectoryInfo directory)
        {
            CurrentDirectory = directory;
            CurrentDirectoryPath = directory.ToString();
            Infos.Clear();
            foreach (var dir in CurrentDirectory.EnumerateDirectories())
                Infos.Add(dir.ToInfo());
            foreach (var file in CurrentDirectory.EnumerateFiles())
                Infos.Add(file.ToInfo());
        }


        public static void CopyFile(string from, string to)
        {
            from = RebasePath(from);
            to = RebasePath(to);

            if (!File.Exists(from))
                throw ExceptionsFactory.PathNotExist(from);
            if (File.Exists(to))
            {
                while (true)
                {
                    var response = ViewHandler.Ask("Output file already exist, rewrite it? (y,n): ", true);
                    if (response == "n")
                        throw ExceptionsFactory.SamePathAlreadyExist(to, nameof(to));
                    if (response == "y")
                        break;
                }
                File.Copy(from, to, true);
            }

            var copyDirectory = Path.GetDirectoryName(to);
            if (!Directory.Exists(copyDirectory))
                Directory.CreateDirectory(copyDirectory);
            File.Copy(from, to);
        }

        public static void CopyDirectory(string from, string to)
        {
            from = RebasePath(from);
            to = RebasePath(to);

            if (!Directory.Exists(from))
                throw ExceptionsFactory.PathNotExist(from);
            if (!Directory.Exists(to))
                Directory.CreateDirectory(to);
            Directory.Move(from, to);
        }

        public static void DeleteDirectory(string directory, bool withChild = false)
        {
            directory = RebasePath(directory);

            if (!Directory.Exists(directory)) return;
            Directory.Delete(directory, withChild);
        }

        public static void DeleteFile(string filePath)
        {
            filePath = RebasePath(filePath);

            if (!File.Exists(filePath)) return;
            File.Delete(filePath);
        }

        public static IEnumerable<Info> GetDirectoryStruct()
        {
            foreach (var dir in CurrentDirectory.GetDirectories())
                yield return dir.ToInfo();
            foreach (var file in CurrentDirectory.GetFiles())
                yield return file.ToInfo();
        }

        public static bool StringPathIsDirectory(string path)
        {
            if (path is null)
                throw ExceptionsFactory.IncorrectArgument("Path to file or directory", nameof(path));
            var pathToCheck = Path.IsPathRooted(path) ? path : Path.Combine(CurrentDirectoryPath, path);
            var attributes = File.GetAttributes(pathToCheck);
            return attributes.HasFlag(FileAttributes.Directory);
        }

        private static string RebasePath(string path)
        {
            if (path is null) return null;
            if (Path.IsPathRooted(path)) return path;
            return Path.Combine(CurrentDirectoryPath, path);
        }
    }
}