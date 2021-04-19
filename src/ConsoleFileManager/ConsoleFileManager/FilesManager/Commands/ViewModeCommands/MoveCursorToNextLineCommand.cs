using System;
using ConsoleFileManager.FilesManager.Models;
using ConsoleFileManager.Render;

namespace ConsoleFileManager.FilesManager.Commands.ViewModeCommands
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