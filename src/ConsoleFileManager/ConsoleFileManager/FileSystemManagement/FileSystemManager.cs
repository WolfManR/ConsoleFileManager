using System.Collections.Generic;
using System.Collections.Immutable;
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

        public DirectoryInfo GetInfoDirectory(string directoryPath) => new(directoryPath);

        public FileInfo GetInfoFile(string filePath) => new(filePath);

        #endregion

        #region Copy

        public void CopyDirectory(string from, string to)
        {
            if (!Directory.Exists(from))
                throw ExceptionsFactory.PathNotExist(from, nameof(from));
            if (!Directory.Exists(to)) 
                Directory.CreateDirectory(to);
            Directory.Move(from,to);
        }

        public void CopyDirectories(ImmutableArray<string> directories, string to)
        {
            for (var i = 0; i < directories.Length; i++)
            {
                CopyDirectory(directories[i], to);
            }
        }

        public void CopyFile(string from, string to, bool rewrite = false)
        {
            if(!File.Exists(from))
                throw ExceptionsFactory.PathNotExist(from, nameof(from));
            if(!rewrite && File.Exists(to))
                throw ExceptionsFactory.SamePathAlreadyExist(to, nameof(to));
            File.Copy(from,to,true);
        }

        public void CopyFiles(ImmutableArray<string> files, string to, bool rewrite = false)
        {
            for (var i = 0; i < files.Length; i++)
            {
                CopyFile(files[i], to, rewrite);
            }
        }

        #endregion

        #region Delete

        public void DeleteDirectory(string directory, bool withChild = false)
        {
            if(!Directory.Exists(directory)) return;
            Directory.Delete(directory, withChild);
        }

        public void DeleteDirectories(ImmutableArray<string> directories, bool withChild = false)
        {
            for (var i = 0; i < directories.Length; i++)
            {
                DeleteDirectory(directories[i],withChild);
            }
        }

        public void DeleteFile(string filePath)
        {
            if (!File.Exists(filePath)) return;
            File.Delete(filePath);
        }

        public void DeleteFiles(ImmutableArray<string> files)
        {
            for (var i = 0; i < files.Length; i++)
            {
                DeleteFile(files[i]);
            }
        }

        #endregion

        
    }
}