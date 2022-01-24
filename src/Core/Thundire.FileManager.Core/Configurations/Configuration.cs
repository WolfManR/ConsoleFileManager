using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core.Configurations
{
    public class Configuration
    {
        public string OpenedPath { get; set; } = "D:/demo";
        public int WindowWidth { get; set; } = 160;
        public int WindowHeight { get; set; } = 40;
        public int ViewPageSize { get; set; } = 20;
        public InputHandleMode InputMode { get; set; } = InputHandleMode.CommandLine;

        public Action OnClose { get; set; }

        public CommandLineConfiguration CommandLineConfiguration { get; set; }
    }
}