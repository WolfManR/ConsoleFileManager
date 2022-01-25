namespace Thundire.FileManager.Core.ConsoleUI.Commands.CommandModeCommands
{
    public class NextCommandCommand : ConsoleKeyCommand
    {
        private readonly IConsoleCommandsRepository _holder;

        public NextCommandCommand(IConsoleCommandsRepository holder) => _holder = holder;

        public override bool CanHandle(ConsoleKeyInfo keyInfo) => keyInfo.Key == ConsoleKey.DownArrow && _holder.HasHistory;

        public override void Handle(ConsoleKeyInfo keyInfo) => _holder.SetNextCommand();
    }
}