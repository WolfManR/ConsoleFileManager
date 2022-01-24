using Thundire.FileManager.Core.Extensions;
using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core.Commands.FileManagerCommands
{
    public class ShowDetailsCommand : FileManagerCommand
    {
        private readonly Services.FileManager _fileManager;
        private readonly ConsoleHandler _handler;

        public ShowDetailsCommand(Services.FileManager fileManager, ConsoleHandler handler)
        {
            _fileManager = fileManager;
            _handler = handler;
        }

        public override string Name { get; } = "Details";
        public override string Description { get; } = "Details";
        public override string[] Abbreviations { get; } = new[] { "sh", "info" };
        public override bool CanHandle(string[] args) => args.Length == 1;

        public override void Handle(string[] args)
        {
            var path = args[0];
            path = _fileManager.RebasePath(path);
            var info = _fileManager.StringPathIsDirectory(path)
                ? new DirectoryInfo(path).ToInfo()
                : new FileInfo(path).ToInfo();
            _handler.ShowDetails(info);
        }
    }
}