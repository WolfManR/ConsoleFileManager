using Thundire.FileManager.Core.Models;
using Thundire.FileManager.Core.Services;

namespace Thundire.FileManager.Core.Commands.CommandModeCommands
{
    public class RemovePreviousCharFromCommandLineCommand : ConsoleKeyCommand
    {
        private readonly CommandHolder _holder;
        private readonly ConsoleHandler _handler;

        public RemovePreviousCharFromCommandLineCommand(CommandHolder holder, ConsoleHandler handler)
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