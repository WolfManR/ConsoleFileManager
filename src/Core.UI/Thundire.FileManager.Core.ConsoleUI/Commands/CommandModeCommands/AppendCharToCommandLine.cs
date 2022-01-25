namespace Thundire.FileManager.Core.ConsoleUI.Commands.CommandModeCommands
{
    public class AppendCharToCommandLineCommand : ConsoleKeyCommand
    {
        private readonly IConsoleCommandsRepository _holder;
        private readonly IConsoleRenderer _handler;

        public AppendCharToCommandLineCommand(IConsoleCommandsRepository holder, IConsoleRenderer handler)
        {
            _holder = holder;
            _handler = handler;
        }

        public override bool CanHandle(ConsoleKeyInfo keyInfo) => 
            char.IsLetterOrDigit(keyInfo.KeyChar) || char.IsSeparator(keyInfo.KeyChar) || char.IsPunctuation(keyInfo.KeyChar);

        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            _handler.AppendCharToCommandLine(keyInfo.KeyChar);
            _holder.AppendCharToCommandLine(keyInfo.KeyChar);
        }
    }
}