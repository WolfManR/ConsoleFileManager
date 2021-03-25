using System;
using System.Collections.Generic;
using System.IO;
using ConsoleFileManager.Infrastructure.Exceptions;
using ConsoleFileManager.Render;

namespace ConsoleFileManager.Infrastructure.Commands
{
    public class ListPathCommand : Command
    {
        #region Implementation of ICommand

        /// <inheritdoc />
        public override string Name { get; } = "list";

        /// <inheritdoc />
        public override string[] Abbreviations { get; } = {"ls"};
        

        /// <inheritdoc />
        public override bool CanHandle(string[] args)
        {
            if (args.Length <= 0 || args[0] is not { Length: > 0 } path)
                throw ExceptionsFactory.IncorrectArgument("Path to directory", nameof(args));

            if (Path.IsPathFullyQualified(path) && !Directory.Exists(path))
                throw ExceptionsFactory.PathNotExist(args.ToString(), nameof(args));

            return true;
        }

        public override void Handle(string[] args)
        {
            var path = args[0];

            IEnumerable<FileSystemInfo> GetDirectoryStruct(string directory)
            {
                var info = new DirectoryInfo(directory);
                foreach (var dir in info.GetDirectories())
                    yield return dir;
                foreach (var file in info.GetFiles())
                    yield return file;
            }


            foreach (var systemInfo in GetDirectoryStruct(path))
                ViewHandler.WriteLine(systemInfo.Name);
        }

        #endregion
    }
}