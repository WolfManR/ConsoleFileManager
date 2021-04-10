﻿using System;

using ConsoleFileManager.FilesManager.Configurations;
using ConsoleFileManager.FilesManager.Services;
using ConsoleFileManager.Render;

namespace ConsoleFileManager.FilesManager
{
    public class FilesManagerSystem
    {
        private readonly Messenger _messenger;
        private readonly FileManager _filesManager;
        private readonly Configuration _config;
        private readonly CommandHolder _commandHolder;
        private readonly ConsoleHandler _consoleHandler;
        private Action _onClose;
        private bool _isExit = false;

        public FilesManagerSystem()
        {
            _config = new();
            _config.CommandLineConfiguration = new()
            {
                X = 4,
                Y = _config.WindowHeight - 4,
                Width = _config.WindowWidth - 8
            };

            _messenger = new();
            _filesManager = new(_messenger);
            _commandHolder = new();
            _commandHolder.OnCommandChanged += _commandHolder_OnCommandChanged;
            _commandHolder.OnCommandExecuted += _commandHolder_OnCommandExecuted;
        }

        private void _commandHolder_OnCommandExecuted()
        {
            _consoleHandler.ClearCommandLine();
        }

        private void _commandHolder_OnCommandChanged(string obj)
        {
            _consoleHandler.ReturnCursorToCommandLineStartPosition();
            _messenger.PrintCommand(obj);
        }

        public FilesManagerSystem Configure(Action<Configuration> configuration)
        {
            configuration?.Invoke(_config);
            _onClose = _config.OnClose;
            return this;
        }


        public void Start()
        {
            while (!_isExit)
            {
                
            }
        }


        private void SwitchMode()
        {
            _config.InputMode = !_config.InputMode;
        }

        public void Exit()
        {
            _isExit = true;
            _onClose?.Invoke();
        }
    }
}