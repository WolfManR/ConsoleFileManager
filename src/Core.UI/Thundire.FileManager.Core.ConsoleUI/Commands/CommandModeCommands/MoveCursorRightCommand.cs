namespace Thundire.FileManager.Core.ConsoleUI.Commands.CommandModeCommands
{
    public class MoveCursorRightCommand : ConsoleKeyCommand
    {
        private readonly IConsoleCommandsRepository _holder;
        private readonly IConsoleRenderer _handler;

        public MoveCursorRightCommand(IConsoleCommandsRepository holder, IConsoleRenderer handler)
        {
            _holder = holder;
            _handler = handler;
        }

        public override bool CanHandle(ConsoleKeyInfo keyInfo) => 
            keyInfo.Key == ConsoleKey.RightArrow && _handler.CanMoveCursorRight(_holder.CommandLineLength);

        public override void Handle(ConsoleKeyInfo keyInfo) => _handler.MoveCursorRight();
    }
}