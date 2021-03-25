using System;
using System.Runtime.CompilerServices;

namespace ConsoleFileManager.Render
{
    public static class ViewHandler
    {
        private static Configuration config;
        private static FileSystemPageView pageView;

        [ModuleInitializer]
        public static void Initialize()
        {
            LoadConfiguration();
            UpdateView();
            InitializeViews();
        }
        
        public static void InitializeViews()
        {
            pageView = new(config.OpenedPath);
        }

        public static void LoadConfiguration()
        {
            config = new Configuration();
        }

        public static void UpdateView()
        {
            Console.BufferHeight = Console.WindowHeight = config.WindowHeight;
            Console.BufferWidth = Console.WindowWidth = config.WindowWidth;
        }

        

        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public static void PrintException(Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }
}