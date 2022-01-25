namespace Thundire.FileManager.Core.ConsoleUI.Commands.CommandModeCommands
{
    public class MoveCursorRightCommand : ConsoleKeyCommand
    {
        private readonly CommandHolder _holder;
        private readonly ConsoleHandler _handler;

        public MoveCursorRightCommand(CommandHolder holder, ConsoleHandler handler)
        {
            _holder = holder;
            _handler = handler;
        }

        public override bool CanHandle(ConsoleKeyInfo keyInfo) => 
            keyInfo.Key == ConsoleKey.RightArrow && _handler.CanMoveCursorRight(_holder.CommandLineLength);

        public override void Handle(ConsoleKeyInfo keyInfo) => _handler.MoveCursorRight();
    }
}