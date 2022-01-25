namespace Thundire.FileManager.Core.ConsoleUI.Commands.ViewModeCommands
{
    public class MoveCursorToNextLineCommand : ConsoleKeyCommand
    {
        private readonly IConsoleRenderer _handler;

        public MoveCursorToNextLineCommand(IConsoleRenderer handler)
        {
            _handler = handler;
        }

        public override bool CanHandle(ConsoleKeyInfo keyInfo) => keyInfo.Key == ConsoleKey.DownArrow;

        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            _handler.NextLine();
        }
    }
}