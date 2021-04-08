using System;

namespace ConsoleFileManager.FileManager.Configuration
{
    public class Configuration
    {
        public string OpenedPath { get; set; } = "D:/demo";
        public int WindowWidth { get; set; } = 160;
        public int WindowHeight { get; set; } = 40;
        public int ViewPageSize { get; set; } = 20;
        public bool InputMode { get; set; } = true;

        public Action OnClose { get; set; }

        public CommandLineConfiguration CommandLineConfiguration { get; set; }
    }
}