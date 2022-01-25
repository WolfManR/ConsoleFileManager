﻿namespace Thundire.FileManager.Core.ConsoleUI.Commands.CommandModeCommands
{
    public class ExecuteFileManagerCommand : ConsoleKeyCommand
    {
        private readonly CommandHolder _holder;

        public ExecuteFileManagerCommand(CommandHolder holder) => _holder = holder;


        public override bool CanHandle(ConsoleKeyInfo keyInfo) => keyInfo.Key == ConsoleKey.Enter && _holder.HasCommandLine;

        public override void Handle(ConsoleKeyInfo keyInfo) => _holder.ExecuteCommand();
    }
}