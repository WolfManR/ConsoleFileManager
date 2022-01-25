namespace Thundire.FileManager.Core.ConsoleUI.Commands.ViewModeCommands
{
    public class MoveCursorToPreviousLineCommand : ConsoleKeyCommand
    {
        private readonly IConsoleRenderer _handler;

        public MoveCursorToPreviousLineCommand(IConsoleRenderer handler)
        {
            _handler = handler;
        }

        public override bool CanHandle(ConsoleKeyInfo keyInfo) => keyInfo.Key == ConsoleKey.UpArrow;

        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            _handler.PrevLine();
        }
    }
}