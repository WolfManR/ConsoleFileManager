﻿namespace Thundire.FileManager.Core.ConsoleUI.Commands.ViewModeCommands
{
    public class ShowSelectedLineInfoCommand : ConsoleKeyCommand
    {
        private readonly ConsoleHandler _handler;

        public ShowSelectedLineInfoCommand(ConsoleHandler handler)
        {
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