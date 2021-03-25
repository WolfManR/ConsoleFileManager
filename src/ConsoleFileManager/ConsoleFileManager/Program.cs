using System;
using ConsoleFileManager.Infrastructure;
using ConsoleFileManager.Infrastructure.Extensions;

namespace ConsoleFileManager
{
    class Program
    {
        private static bool _isExit;

        private static readonly InputHandler handler = new(
            new InputCommandsParser()
            .CommandsRegistration());



        static void Main()
        {
            while (!_isExit)
            {
                Console.WriteLine("Input command");
                var userInput = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(userInput)) continue;

                handler.Handle(userInput);
            }
        }



        public static void Close()
        {
            _isExit = true;
        }
    }


    public class Configuration
    {
        public string OpenedPath { get; set; } = "D:/demo";
        public int WindowWidth { get; set; } = 120;
        public int WindowHeight { get; set; } = 30;
    }
}
