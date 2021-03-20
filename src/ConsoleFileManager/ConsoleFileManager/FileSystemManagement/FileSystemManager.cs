using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleFileManager.FileSystemManagement
{
    public class FileSystemManager
    {
        public IEnumerable<FileSystemInfo> GetDirectoryStruct(string directory)
        {
            var info = new DirectoryInfo(directory);
            foreach (var dir in info.GetDirectories(directory))
                yield return dir;
            foreach (var file in info.GetFiles(directory))
                yield return file;
        }

        #region Get Info

        public Info GetInfoDirectory(string directory) => new DirectoryInfo(directory).ToInfo();

        public Info GetInfoFile(string filePath) => new FileInfo(filePath).ToInfo();

        #endregion

        #region Copy

        public void CopyDirectory(string directory)
        {

        }

        public void CopyDirectories(IEnumerable<string> directories)
        {

        }

        public void CopyFile(string filePath)
        {

        }

        public void CopyFiles(IEnumerable<string> files)
        {

        }

        #endregion

        #region Delete

        public void DeleteDirectory(string directory)
        {

        }

        public void DeleteDirectories(IEnumerable<string> directories)
        {

        }

        public void DeleteFile(string filePath)
        {

        }

        public void DeleteFiles(IEnumerable<string> files)
        {

        }

        #endregion


        public void Print()
        {

        }
    }

    public class Info
    {
        public FileSystemInfo SystemInfo { get; init; }
        public bool IsFile { get; init; }
        public FileAttributes Attributes => SystemInfo.Attributes;

        public long Length =>
            IsFile
                ? ((FileInfo) SystemInfo).Length
                : ((DirectoryInfo) SystemInfo).GetLength();

        public DateTime CreationTime => SystemInfo.CreationTime;
        public DateTime LastAccessTime => SystemInfo.LastAccessTime;
        public DateTime LastWriteTime => SystemInfo.LastWriteTime;
    }

    public static class Extensions
    {
        public static Info ToInfo(this DirectoryInfo directory) => new() {IsFile = false, SystemInfo = directory};
        public static Info ToInfo(this FileInfo file) => new() {IsFile = true, SystemInfo = file};

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