using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core.Commands.ViewModeCommands
{
    public class ShowSelectedLineInfoCommand : ConsoleKeyCommand
    {
        private readonly Services.FileManager _fileManager;
        private readonly ConsoleHandler _handler;

        public ShowSelectedLineInfoCommand(Services.FileManager fileManager,ConsoleHandler handler)
        {
            _fileManager = fileManager;
            _handler = handler;
        }
        public override bool CanHandle(ConsoleKeyInfo keyInfo) => keyInfo.Key == ConsoleKey.RightArrow;

        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            var info = _handler.GetSelectedInfo();
            _handler.ShowDetails(info);
        }
    }
}