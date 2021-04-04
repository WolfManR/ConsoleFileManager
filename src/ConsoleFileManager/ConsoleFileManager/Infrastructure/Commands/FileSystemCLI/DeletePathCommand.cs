using System.IO;
using ConsoleFileManager.Infrastructure.Exceptions;
using ConsoleFileManager.Render;

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
            if (args.Length != 1)
                throw ExceptionsFactory.IncorrectArgument("Path to delete", nameof(args));
            return Path.IsPathFullyQualified(args[0]);
        }

        /// <inheritdoc />
        public override void Handle(string[] args)
        {
            var toDelete = args[0];
            if (FilesManager.StringPathIsDirectory(toDelete))
            {
                FilesManager.DeleteDirectory(toDelete);
                return;
            }

            FilesManager.DeleteFile(toDelete);
        }

        #endregion
    }
}