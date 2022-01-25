namespace Thundire.FileManager.Core.ConsoleUI.Commands.CommandModeCommands
{
    public class RemovePreviousCharFromCommandLineCommand : ConsoleKeyCommand
    {
        private readonly IConsoleCommandsRepository _holder;
        private readonly IConsoleRenderer _handler;

        public RemovePreviousCharFromCommandLineCommand(IConsoleCommandsRepository holder, IConsoleRenderer handler)
        {
            _holder = holder;
            _handler = handler;
        }

        public override bool CanHandle(ConsoleKeyInfo keyInfo) => keyInfo.Key == ConsoleKey.Backspace && _handler.CanMoveCursorLeft();

        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            var index = _handler.MoveCursorLeft();
            var toReplace = _holder.RemoveChar(index);
            _handler.ReplaceCommandLineText(index, toReplace);
        }
    }
}