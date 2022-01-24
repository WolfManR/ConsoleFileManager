using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core.Commands.CommandModeCommands
{
    public class MoveCursorLeftCommand : ConsoleKeyCommand
    {
        private readonly ConsoleHandler _handler;

        public MoveCursorLeftCommand(ConsoleHandler handler) => _handler = handler;


        public override bool CanHandle(ConsoleKeyInfo keyInfo) => 
            keyInfo.Key==ConsoleKey.LeftArrow && _handler.CanMoveCursorLeft();

        public override void Handle(ConsoleKeyInfo keyInfo) => _handler.MoveCursorLeft();
    }
}