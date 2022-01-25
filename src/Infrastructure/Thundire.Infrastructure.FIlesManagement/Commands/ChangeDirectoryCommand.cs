using Thundire.FileManager.Core;
using Thundire.FileManager.Core.Configurations;
using Thundire.FileManager.Core.Models;

namespace Thundire.Infrastructure.FIlesManagement.Commands
{
    public class ChangeDirectoryCommand : FileManagerCommand
    {
        private readonly IFilesManager _fileManager;


        public ChangeDirectoryCommand(IFilesManager fileManager) => _fileManager = fileManager;


        public override string Name { get; } = "Change directory";
        public override string Description { get; } = "Change directory";
        public override string[] Abbreviations { get; } = {"cd"};


        public override bool CanHandle(string[] args) => args.Length == 1;

        public override void Handle(string[] args)
        {
            var move = args[0];
            switch (move)
            {
                case PathAbbreviations.Back:
                    _fileManager.ChangeDirectory(DirectoryMove.Back);
                    break;
                case PathAbbreviations.ToRoot:
                    _fileManager.ChangeDirectory(DirectoryMove.ToRoot);
                    break;
                case var path when Path.IsPathRooted(path):
                    _fileManager.ChangeDirectory(path);
                    break;
                case var path when !Path.IsPathRooted(path):
                    _fileManager.ChangeDirectory(DirectoryMove.Inner, path);
                    break;
            }
        }
    }
}