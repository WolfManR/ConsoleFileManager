namespace Thundire.FileManager.Core.ConsoleUI.Commands.ViewModeCommands
{
    public class MoveCursorToNextLineCommand : ConsoleKeyCommand
    {
        private readonly ConsoleHandler _handler;

        public MoveCursorToNextLineCommand(ConsoleHandler handler)
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