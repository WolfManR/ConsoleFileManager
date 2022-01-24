﻿using Thundire.FileManager.Core.Models;
using Thundire.FileManager.Core.Services;

namespace Thundire.FileManager.Core.Commands
{
    public class SwitchInputHandleModeCommand: ConsoleKeyCommand
    {
        private readonly CommandHolder _holder;

        public SwitchInputHandleModeCommand(CommandHolder holder)
        {
            _holder = holder;
        }

        public override bool CanHandle(ConsoleKeyInfo keyInfo) => keyInfo.Key == ConsoleKey.Tab;

        public override void Handle(ConsoleKeyInfo keyInfo)
        {
            _holder.SwitchMode();
        }
    }
}