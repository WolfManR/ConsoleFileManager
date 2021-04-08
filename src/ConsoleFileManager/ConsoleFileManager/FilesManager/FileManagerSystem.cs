using System;

using ConsoleFileManager.FilesManager.Configurations;
using ConsoleFileManager.FilesManager.Services;

namespace ConsoleFileManager.FilesManager
{
    public class FilesManagerSystem
    {
        private readonly Messenger _messenger;
        private readonly FileManager _filesManager;
        private readonly Configuration _config;
        private Action _onClose;

        public FilesManagerSystem()
        {
            _config = new Configuration();
            _config.CommandLineConfiguration = new()
            {
                X = 4,
                Y = _config.WindowHeight - 4,
                Width = _config.WindowWidth - 8
            };

            _messenger = new Messenger();
            _filesManager = new FileManager(_messenger);
        }

        public FilesManagerSystem Configure(Action<Configuration> configuration)
        {
            configuration?.Invoke(_config);
            _onClose = _config.OnClose;
            return this;
        }


        public void Start()
        {
            while (true)
            {
                
            }
        }


        private void SwitchMode()
        {
            _config.InputMode = !_config.InputMode;
        }
    }
}