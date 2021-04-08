using ConsoleFileManager.FileManager.Models;
using System.IO;

namespace ConsoleFileManager.FileManager.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IOExtensions
    {
        public static Info ToInfo(this DirectoryInfo directory) =>
            new() { IsFile = false, Name = directory.Name, Path = directory.ToString() };
        public static Info ToInfo(this FileInfo file) =>
            new() { IsFile = true, Name = file.Name, Path = file.ToString(), FileExtension = file.Extension };

        public static long GetLength(this FileSystemInfo self) =>
            self switch
            {
                FileInfo file           => file.Length,
                DirectoryInfo directory => directory.GetLength(),
                _                       => -1
            };

        public static long GetLength(this DirectoryInfo directory)
        {
            long actualLength = 0;
            var innerDirectories = directory.GetDirectories();
            for (var i = 0; i < innerDirectories.Length; i++)
            {
                actualLength = actualLength + innerDirectories[i].GetLength();
            }

            var innerFiles = directory.GetFiles();
            for (var i = 0; i < innerFiles.Length; i++)
            {
                actualLength = actualLength + innerFiles[i].Length;
            }

            return actualLength;
        }
    }
}