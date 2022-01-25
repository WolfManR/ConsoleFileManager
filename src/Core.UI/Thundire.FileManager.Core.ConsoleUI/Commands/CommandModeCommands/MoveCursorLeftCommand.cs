namespace Thundire.FileManager.Core.ConsoleUI.Commands.CommandModeCommands
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