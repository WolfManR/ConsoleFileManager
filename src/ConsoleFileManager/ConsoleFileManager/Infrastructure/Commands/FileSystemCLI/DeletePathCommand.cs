using System.IO;
using ConsoleFileManager.Infrastructure.Exceptions;
using ConsoleFileManager.Infrastructure.Extensions;

namespace ConsoleFileManager.Infrastructure.Commands.FileSystemCLI
{
    public class DeletePathCommand : Command
    {
        #region Overrides of Command

        /// <inheritdoc />
        public override string Name { get; } = "Delete path";

        /// <inheritdoc />
        public override string[] Abbreviations { get; } = {"del", "delete","r","rm" };

        /// <inheritdoc />
        public override bool CanHandle(string[] args)
        {
            if (args.Length < 1)
                throw ExceptionsFactory.IncorrectArgument("Path to delete", nameof(args));
            return Path.IsPathFullyQualified(args[0]);
        }

        /// <inheritdoc />
        public override void Handle(string[] args)
        {
            var toDelete = args[0];
            if (toDelete.StringPathIsDirectory())
            {
                DeleteDirectory(toDelete);
                return;
            }

            DeleteFile(toDelete);
        }

        #endregion

        public void DeleteDirectory(string directory, bool withChild = false)
        {
            if (!Directory.Exists(directory)) return;
            Directory.Delete(directory, withChild);
        }

        public void DeleteFile(string filePath)
        {
            if (!File.Exists(filePath)) return;
            File.Delete(filePath);
        }
    }
}