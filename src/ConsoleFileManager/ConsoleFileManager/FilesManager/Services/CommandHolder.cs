﻿using System;
using System.Collections.Generic;
using System.Text;

using ConsoleFileManager.FilesManager.Models;

namespace ConsoleFileManager.FilesManager.Services
{
    public class CommandHolder
    {
        private readonly Messenger _messenger;

        private readonly List<FileManagerCommand> _fileManagerCommands = new();
        private readonly List<ConsoleKeyCommand> _commandModeCommands = new();
        private readonly List<ConsoleKeyCommand> _viewModeCommands = new();

        private InputHandleMode _inputHandleMode;

        private readonly StringBuilder _buffer = new StringBuilder();
        private readonly List<string> _history = new List<string>();
        private int _currentHistoryLine;
        
        public bool HasHistory => _history.Count > 0;
        public bool HasCommandLine => _buffer.Length > 0;
        public int CommandLineLength => _buffer.Length;


        public event Action<string> OnCommandChanged;
        public event Action OnCommandExecuted;
        public event Action<InputHandleMode> OnInputHandleModeChanged;

        public CommandHolder(InputHandleMode inputHandleMode)
        {
            this._inputHandleMode = inputHandleMode;
        }


        public void HandleInput(ConsoleKeyInfo key)
        {
            var commands = _inputHandleMode switch
                           {
                               InputHandleMode.CommandLine => _commandModeCommands,
                               InputHandleMode.List => _viewModeCommands,
                               _ => throw new ArgumentOutOfRangeException()
                           };

            for (var i = 0; i < commands.Count; i++)
            {
                if (!commands[i].CanHandle(key)) continue;
                commands[i].Handle(key);
                return;
            }
        }

        public void SwitchMode()
        {
            _inputHandleMode = _inputHandleMode switch
                              {
                                  InputHandleMode.CommandLine => InputHandleMode.List,
                                  InputHandleMode.List        => InputHandleMode.CommandLine,
                                  _                           => throw new ArgumentOutOfRangeException(nameof(_inputHandleMode))
                              };
            OnInputHandleModeChanged?.Invoke(_inputHandleMode);
        }


        private (string abbreviation, string[] args) ParseCommandLine(string commandLine)
        {
            var toHandle = commandLine.Split(' ').AsSpan();
            var abbreviation = toHandle[0];
            var args = toHandle.Length > 1 ? toHandle[1..] : Span<string>.Empty;
            return (abbreviation, args.ToArray());
        }

        private FileManagerCommand Find(string abbreviation)
        {
            static bool IsCorrectCommand(FileManagerCommand command, string commandName)
            {
                if (command.Name == commandName) return true;
                for (var i = 0; i < command.Abbreviations.Length; i++)
                {
                    if (command.Abbreviations[i] == commandName)
                        return true;
                }
                return false;
            }

            for (var i = 0; i < _fileManagerCommands.Count; i++)
            {
                if (IsCorrectCommand(_fileManagerCommands[i], abbreviation))
                    return _fileManagerCommands[i];
            }

            return null;
        }

        public void ExecuteCommand()
        {
            var command = _buffer.ToString();
            _buffer.Clear();
            _history.Add(command);
            _currentHistoryLine = -1;
            OnCommandExecuted?.Invoke();

            ExecuteCommand(command);
        }

        private void ExecuteCommand(string input)
        {
            var (abbreviation, args) = ParseCommandLine(input);
            var command = Find(abbreviation);
            if (command is null)
            {
                _messenger.Report($"Not registered command for {abbreviation} to handle input:\n{string.Join(" ", args)}");
                return;
            }
            try
            {
                if (command.CanHandle(args))
                    command.Handle(args);
            }
            catch (Exception e)
            {
                _messenger.Report(e);
            }
        }

        public void SetPreviousCommand()
        {
            if (_currentHistoryLine >= _history.Count)
                _currentHistoryLine = -1;

            _currentHistoryLine = _currentHistoryLine switch
                                  {
                                      0                                     => 1,
                                      var index when index < _history.Count => _currentHistoryLine + 1,
                                      _                                     => _history.Count - 1
                                  };

            var command = _history[_currentHistoryLine];
            _buffer.Clear().Append(command);
            OnCommandChanged?.Invoke(command);
        }

        public void SetNextCommand()
        {
            if (_currentHistoryLine >= _history.Count)
                _currentHistoryLine = -1;

            _currentHistoryLine = _currentHistoryLine switch
                                  {
                                      0   => 0,
                                      < 0 => _history.Count - 1,
                                      _   => _currentHistoryLine - 1
                                  };

            var command = _history[_currentHistoryLine];
            _buffer.Clear().Append(command);
            OnCommandChanged?.Invoke(command);
        }


        public CommandHolder Register(FileManagerCommand command)
        {
            var abbreviations = string.Join(' ', command.Abbreviations);
            if (_fileManagerCommands.Find(c => string.Join(' ', c.Abbreviations) == abbreviations) is null)
                _fileManagerCommands.Add(command);
            return this;
        }

        public CommandHolder Register(ConsoleKeyCommand command)
        {
            _commandModeCommands.Add(command);
            return this;
        }

        public string RemoveChar(int index)
        {
            _buffer.Remove(index, 1);
            return _buffer.ToString(index, _buffer.Length - index - 1);
        }

        public void AppendCharToCommandLine(char toAppend)
        {
            _buffer.Append(toAppend);
        }
    }
}