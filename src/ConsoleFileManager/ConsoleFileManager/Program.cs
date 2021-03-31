using System;
using System.Reflection.Metadata;
using System.Text;

using ConsoleFileManager.Infrastructure;
using ConsoleFileManager.Infrastructure.Extensions;
using ConsoleFileManager.Render;
using ConsoleFileManager.Render.Controls;
using ConsoleFileManager.Render.Primitives;

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
            new Printer(new Configuration()).HandleInput();

            //while (!_isExit)
            //{
            //    Console.WriteLine("Input command");
            //    var userInput = Console.ReadLine();
            //    if(string.IsNullOrWhiteSpace(userInput)) continue;

            //    handler.Handle(userInput);
            //}
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
        public int WindowHeight { get; set; } = 40;
    }

    public class Printer
    {
        private readonly Configuration config;
        private static bool _isExit;

        public Printer(Configuration config)
        {
            this.config = config;
        }

        public void HandleInput()
        {
            Console.BufferHeight = Console.WindowHeight = config.WindowHeight;
            Console.BufferWidth = Console.WindowWidth = config.WindowWidth;
            //while (!_isExit)
            //{
            //}
            PrintBorder(2, 2, 20, 10);
            Console.SetCursorPosition(0, 30);
            Console.WriteLine();
        }



        private void PrintBorder(int x, int y, int width, int height)
        {
            new Border(x, y, width, height)
            {
                ContentControl = new StackPanel(
                    "There some text",
                    "There some text",
                    "There some text",
                    "There some text"
                    )
                {
                    Size = new Size(16, 8)
                },
                Padding = 1
            }.Draw();
        }
    }

    public static class Alphabet
    {
        public const char BorderVertical = '║';
        public const char BorderVerticalLeft = '╣';
        public const char BorderVerticalRight = '╠';

        public const char BorderCenter = '╬';

        public const char BorderHorizontal = '═';
        public const char BorderHorizontalUp = '╩';
        public const char BorderHorizontalDown = '╦';


        public const char BorderCornerUpLeft = '╔';
        public const char BorderCornerUpRight = '╗';
        public const char BorderCornerDownRight = '╝';
        public const char BorderCornerDownLeft = '╚';
    }
}
