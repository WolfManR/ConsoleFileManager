using Thundire.FileManager.Core.Models;
using Thundire.FileManager.Core.Services;

namespace Thundire.FileManager.Core.Commands.CommandModeCommands
{
    public class NextCommandCommand : ConsoleKeyCommand
    {
        private readonly CommandHolder _holder;

        public NextCommandCommand(CommandHolder holder) => _holder = holder;

        public override bool CanHandle(ConsoleKeyInfo keyInfo) => keyInfo.Key == ConsoleKey.DownArrow && _holder.HasHistory;

        public override void Handle(ConsoleKeyInfo keyInfo) => _holder.SetNextCommand();
    }
}