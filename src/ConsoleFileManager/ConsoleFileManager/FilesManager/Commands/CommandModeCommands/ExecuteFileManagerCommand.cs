using System;
using ConsoleFileManager.FilesManager.Models;
using ConsoleFileManager.FilesManager.Services;

namespace ConsoleFileManager.FilesManager.Commands.CommandModeCommands
{
    public class ExecuteFileManagerCommand : ConsoleKeyCommand
    {
        private readonly CommandHolder _holder;

        public ExecuteFileManagerCommand(CommandHolder holder) => _holder = holder;


        public override bool CanHandle(ConsoleKeyInfo keyInfo) => keyInfo.Key == ConsoleKey.Enter && _holder.HasCommandLine;

        public override void Handle(ConsoleKeyInfo keyInfo) => _holder.ExecuteCommand();
    }
}