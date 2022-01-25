namespace Thundire.FileManager.Core.ConsoleUI.Commands.CommandModeCommands
{
    public class PreviousCommandCommand : ConsoleKeyCommand
    {
        private readonly CommandHolder _holder;

        public PreviousCommandCommand(CommandHolder holder) => _holder = holder;

        public override bool CanHandle(ConsoleKeyInfo keyInfo) => keyInfo.Key == ConsoleKey.UpArrow && _holder.HasHistory;

        public override void Handle(ConsoleKeyInfo keyInfo) => _holder.SetPreviousCommand();
    }
}