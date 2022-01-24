using Thundire.FileManager.Core.Exceptions;
using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core.Commands.FileManagerCommands
{
    public class DeletePathCommand : FileManagerCommand
    {
        private readonly Services.FileManager _fileManager;


        public DeletePathCommand(Services.FileManager fileManager)
        {
            _fileManager = fileManager;
        }


        public override string Name { get; } = "Delete path";
        public override string Description { get; } = "Delete path";
        public override string[] Abbreviations { get; } = { "del", "delete", "r", "rm" };


        public override bool CanHandle(string[] args)
        {
            if (args.Length != 1)
                throw ExceptionsFactory.IncorrectArgument("Path to delete", nameof(args));
            return Path.IsPathFullyQualified(args[0]);
        }

        public override void Handle(string[] args)
        {
            var toDelete = args[0];
            if (_fileManager.StringPathIsDirectory(toDelete))
            {
                _fileManager.DeleteDirectory(toDelete);
                return;
            }

            _fileManager.DeleteFile(toDelete);
        }
    }
}