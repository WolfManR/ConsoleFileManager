using Thundire.FileManager.Core.Configurations;
using Thundire.FileManager.Core.ConsoleUI.Commands;
using Thundire.FileManager.Core.ConsoleUI.Commands.CommandModeCommands;
using Thundire.FileManager.Core.ConsoleUI.Commands.ViewModeCommands;
using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core.ConsoleUI
{
    public class FilesManagerSystem
    {
        private readonly Messenger _messenger;
        private readonly IFilesManager _filesManager;
        private readonly Configuration _config;
        private readonly CommandLineConfiguration _commandLineConfiguration;
        private readonly CommandHolder _commandHolder;
        private readonly ConsoleHandler _consoleHandler;
        private Action _onClose;
        private bool _isExit = false;

        public FilesManagerSystem()
        {
            _config = new();
            _commandLineConfiguration = new()
            {
                X = 4,
                Y = _config.WindowHeight - 4
            };

            _consoleHandler = new(_config, _commandLineConfiguration);
            _messenger = new(_consoleHandler);
            _filesManager = new(_messenger);
            _filesManager.OnDirectoryChanged += FilesManagerOnDirectoryChanged;
            _commandHolder = new (_config.InputMode, _messenger);
            _commandHolder.OnCommandChanged += _commandHolder_OnCommandChanged;
            _commandHolder.OnCommandExecuted += _commandHolder_OnCommandExecuted;
            _commandHolder.OnInputHandleModeChanged += CommandHolderOnOnInputHandleModeChanged;
            FileManagerCommandsRegister(_commandHolder);
            CommandModeCommandsRegister(_commandHolder);
            ViewModeCommandsRegister(_commandHolder);
            SharedCommandsRegister(_commandHolder);
        }

        private void FilesManagerOnDirectoryChanged()
        {
            _consoleHandler.UpdateDirectoryView(_filesManager.Infos, _filesManager.Current);
            _consoleHandler.ReturnCursor();
        }

        private void CommandHolderOnOnInputHandleModeChanged(InputHandleMode mode)
        {
            _config.InputMode = mode;
            if(mode == InputHandleMode.CommandLine)
                _consoleHandler.ReturnCursor();
        }

        

        private void CommandModeCommandsRegister(CommandHolder holder) => holder
            .Register(new AppendCharToCommandLineCommand(_commandHolder,_consoleHandler))
            .Register(new ExecuteFileManagerCommand(_commandHolder))
            .Register(new MoveCursorLeftCommand(_consoleHandler))
            .Register(new MoveCursorRightCommand(_commandHolder,_consoleHandler))
            .Register(new NextCommandCommand(_commandHolder))
            .Register(new PreviousCommandCommand(_commandHolder))
            .Register(new RemovePreviousCharFromCommandLineCommand(_commandHolder,_consoleHandler))
        ;

        private void ViewModeCommandsRegister(CommandHolder holder) => holder
            .Register(new MoveCursorToNextLineCommand(_consoleHandler),InputHandleMode.List)
            .Register(new MoveCursorToPreviousLineCommand(_consoleHandler), InputHandleMode.List)
            .Register(new ShowSelectedLineInfoCommand(_filesManager,_consoleHandler),InputHandleMode.List)
            ;

        private void SharedCommandsRegister(CommandHolder holder) => holder
            .Register(new SwitchInputHandleModeCommand(holder),InputHandleMode.Shared)
        ;

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
            _filesManager.ChangeDirectory(_config.OpenedPath);
            _consoleHandler.ShowView();
            _consoleHandler.ReturnCursorToCommandLineStartPosition();
            while (!_isExit)
            {
                _commandHolder.HandleInput(Console.ReadKey(true));
            }
        }
        

        public void Exit()
        {
            _isExit = true;
            _onClose?.Invoke();
        }
    }
}