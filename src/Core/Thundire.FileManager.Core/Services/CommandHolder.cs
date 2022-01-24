using System.Text;
using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core.Services
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
        private int _currentHistoryLine = -1;
        
        public bool HasHistory => _history.Count > 0;
        public bool HasCommandLine => _buffer.Length > 0;
        public int CommandLineLength => _buffer.Length;


        public event Action<string> OnCommandChanged;
        public event Action OnCommandExecuted;
        public event Action<InputHandleMode> OnInputHandleModeChanged;

        public CommandHolder(InputHandleMode inputHandleMode, Messenger messenger)
        {
            this._inputHandleMode = inputHandleMode;
            _messenger = messenger;
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
                _messenger.Report($"Not registered command to handle input:\n{abbreviation}\n{string.Join(" ", args)}");
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
            if(!HasHistory) return;
            var line = _currentHistoryLine;

            if (line <= 0)
            {
                _currentHistoryLine = _history.Count - 1;
                SetCommand();
                return;
            }

            _currentHistoryLine -= 1;
            SetCommand();
        }
        
        public void SetNextCommand()
        {
            if (!HasHistory) return;
            var line = _currentHistoryLine;

            if (line < 0)
            {
                _currentHistoryLine = _history.Count - 1;
                SetCommand();
                return;
            }

            if (line + 1 < _history.Count)
            {
                _currentHistoryLine += 1;
                SetCommand();
                return;
            }

            _currentHistoryLine = 0;
            SetCommand();
        }
        private void SetCommand()
        {
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

        public CommandHolder Register(ConsoleKeyCommand command, InputHandleMode mode = InputHandleMode.CommandLine)
        {
            switch (mode)
            {
                case InputHandleMode.CommandLine:
                    _commandModeCommands.Add(command);
                    break;
                case InputHandleMode.List:
                    _viewModeCommands.Add(command);
                    break;
                default:
                    _commandModeCommands.Add(command);
                    _viewModeCommands.Add(command);
                    break;
            }

            return this;
        }

        public string RemoveChar(int index)
        {
            var length = _buffer.Length - index;
            _buffer.Remove(index, 1);
            if (index == _buffer.Length) return " ";
            var toReplace = _buffer.ToString(index, _buffer.Length - index);
            if (toReplace.Length < length)
                toReplace += new string(' ', length - toReplace.Length);
            return toReplace;
        }

        public void AppendCharToCommandLine(char toAppend)
        {
            _buffer.Append(toAppend);
        }
    }
}