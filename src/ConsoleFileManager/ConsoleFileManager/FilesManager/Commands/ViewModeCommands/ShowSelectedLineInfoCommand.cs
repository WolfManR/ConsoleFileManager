using System;
using ConsoleFileManager.FilesManager.Models;
using ConsoleFileManager.FilesManager.Services;
using ConsoleFileManager.Render;

namespace ConsoleFileManager.FilesManager.Commands.ViewModeCommands
{
    public class ShowSelectedLineInfoCommand : ConsoleKeyCommand
    {
        private readonly FileManager _fileManager;
        private readonly ConsoleHandler _handler;

        public ShowSelectedLineInfoCommand(FileManager fileManager,ConsoleHandler handler)
        {
            _fileManager = fileManager;
            _handler = handler;
        }
        public override bool CanHandle(ConsoleKeyInfo keyInfo) => keyInfo.Key == ConsoleKey.RightArrow;

        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            var info = _handler.GetSelectedInfo();
            _handler.ShowDetails(info);
        }
    }
}