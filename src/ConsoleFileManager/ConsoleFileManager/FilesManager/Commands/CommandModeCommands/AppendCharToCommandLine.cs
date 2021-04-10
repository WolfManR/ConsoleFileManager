using System;
using ConsoleFileManager.FilesManager.Models;
using ConsoleFileManager.FilesManager.Services;
using ConsoleFileManager.Render;

namespace ConsoleFileManager.FilesManager.Commands.CommandModeCommands
{
    public class AppendCharToCommandLineCommand : ConsoleKeyCommand
    {
        private readonly CommandHolder _holder;
        private readonly ConsoleHandler _handler;

        public AppendCharToCommandLineCommand(CommandHolder holder, ConsoleHandler handler)
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