using Thundire.FileManager.Core;
using Thundire.FileManager.Core.Extensions;
using Thundire.FileManager.Core.Models;

namespace Thundire.Infrastructure.FIlesManagement.Commands
{
    public class ShowDetailsCommand : FileManagerCommand
    {
        private readonly IFilesManager _fileManager;
        private readonly IRenderer _handler;

        public ShowDetailsCommand(IFilesManager fileManager, IRenderer handler)
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