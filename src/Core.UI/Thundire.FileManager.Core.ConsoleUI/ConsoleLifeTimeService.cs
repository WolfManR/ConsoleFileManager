using Thundire.FileManager.Core.Configurations;
using Thundire.FileManager.Core.ConsoleUI.Commands;
using Thundire.FileManager.Core.ConsoleUI.Commands.CommandModeCommands;
using Thundire.FileManager.Core.ConsoleUI.Commands.ViewModeCommands;
using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core.ConsoleUI
{
    public class ConsoleLifeTimeService : ILifeTimeService
    {
        private readonly ThundireFileManager _thundireFileManager;
        private IFilesManager _filesManager => _thundireFileManager.FilesManager;


        private readonly INotifyService _notifyService;
        private readonly IConsoleCommandsRepository _commandsRepository;
        private readonly IConsoleRenderer _consoleRenderer;

        private readonly Configuration _config;
        private readonly CommandLineConfiguration _commandLineConfiguration;

        private Action _onClose;
        private bool _isExit = false;

        public ConsoleLifeTimeService(ThundireFileManager thundireFileManager)
        {
            _thundireFileManager = thundireFileManager;

            _config = thundireFileManager.Configuration;
            _commandLineConfiguration = new()
            {
                X = 4,
                Y = _config.WindowHeight - 4
            };

            _consoleRenderer = new ConsoleRenderer(_config, _commandLineConfiguration);
            _notifyService = new Messenger(_consoleRenderer);
            _commandsRepository = new CommandsRepository(_config.InputMode, _notifyService);
            thundireFileManager.NotifyService = _notifyService;
            thundireFileManager.CommandsRepository = _commandsRepository;
            thundireFileManager.Renderer = _consoleRenderer;

            _filesManager.OnDirectoryChanged += HandleDirectoryChanged;
            _commandsRepository.OnCommandChanged += HandleCommandChanged;
            _commandsRepository.OnCommandExecuted += HandleCommandExecuted;
            _commandsRepository.OnInputHandleModeChanged += HandleInputHandleModeChanged;
        }

        private void HandleDirectoryChanged()
        {
            _consoleRenderer.UpdateDirectoryView(_filesManager.Infos, _filesManager.Current);
            _consoleRenderer.ReturnCursor();
        }

        private void HandleInputHandleModeChanged(InputHandleMode mode)
        {
            _config.InputMode = mode;
            if(mode == InputHandleMode.CommandLine)
                _consoleRenderer.ReturnCursor();
        }

        private void HandleCommandExecuted()
        {
            _consoleRenderer.ClearCommandLine();
        }

        private void HandleCommandChanged(string obj)
        {
            _consoleRenderer.ReturnCursorToCommandLineStartPosition();
            _notifyService.PrintCommand(obj);
        }

        public void Start()
        {
            _thundireFileManager.Initialize();

            _filesManager.ChangeDirectory(_config.OpenedPath);
            _consoleRenderer.ShowView();
            _consoleRenderer.ReturnCursorToCommandLineStartPosition();
            while (!_isExit)
            {
                _commandsRepository.HandleInput(Console.ReadKey(true));
            }
        }

        public void Exit()
        {
            _isExit = true;
            _onClose?.Invoke();
        }
    }
}