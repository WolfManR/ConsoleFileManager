using System;
using System.Linq;
using ConsoleFileManager.Render.Controls;

namespace ConsoleFileManager
{
    public class Printer
    {
        private readonly Configuration config;
        private static bool _isExit;

        private ListView view;

        public Printer(Configuration config)
        {
            this.config = config;
        }

        public int CommandLine { get; set; }
        public int CommandLineWidth { get; set; }
        public void HandleInput()
        {
            Console.BufferHeight = Console.WindowHeight = config.WindowHeight;
            Console.BufferWidth = Console.WindowWidth = config.WindowWidth;
            
            MainView();
            ReturnCursorToCommandLine();
            while (!_isExit)
            {
                var inputKey = Console.ReadKey();

                _isExit = HandleKey(inputKey);
            }
        }

        private void ReturnCursorToCommandLine()
        {
            Console.SetCursorPosition(4, CommandLine);
        }

        private void ClearCommandLine()
        {
            ReturnCursorToCommandLine();
            Console.Write(new string(' ', CommandLineWidth));
            ReturnCursorToCommandLine();
        }

        private bool HandleKey(ConsoleKeyInfo inputKey)
        {
            if (inputKey.Key == ConsoleKey.Escape)
            {
                return true;
            }

            
            void PrintKey()
            {
                var msg = inputKey.Key switch
                          {
                              ConsoleKey.UpArrow    => "up",
                              ConsoleKey.LeftArrow  => "left",
                              ConsoleKey.RightArrow => "right",
                              ConsoleKey.DownArrow  => "down",
                              _                     => null
                          };
                if (msg is null) return;

                ClearCommandLine();

                Console.Write(msg);
                ReturnCursorToCommandLine();
            }

            if (inputKey.Key == ConsoleKey.Tab)
            {
                InputMode = !InputMode;
                Console.CursorVisible = !Console.CursorVisible;
            }

            if (InputMode)
            {
                PrintKey();
            }
            else if (inputKey.Key == ConsoleKey.UpArrow || inputKey.Key == ConsoleKey.DownArrow)
            {
                switch (inputKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        view.Prev(); break;
                    case ConsoleKey.DownArrow:
                        view.Next(); break;
                };

            }


            ReturnCursorToCommandLine();
            PrintKey();

            return false;
        }


        public bool InputMode { get; set; } = true;

        private void PrintListBorder(int x, int y, int width, int height)
        {
            var content = new ListView(8, Enumerable.Range(0, 45).Select(i => (object)$"Message Line {i} cutted").ToList());
            var border = new Border(x, y, width, height)
            {
                Padding = (1, 0),
                ContentControl = content
            };
            view = content;
            border.Print();

        }

        private void PrintBorder(int x, int y, int width, int height)
        {
            var border = new Border(x, y, width, height)
            {
                Padding = (1, 0),
                ContentControl = new StackPanel(
                    "There some text",
                    "There some text",
                    "There some text",
                    "There some text"
                )
            };
            border.Print();
        }

        private void MainView()
        {
            var (windowWidth,windowHeight) = (config.WindowWidth - 1, config.WindowHeight);

            var windowBorder = new Border(0, 0, windowWidth, windowHeight);
            view = new ListView(20, Enumerable.Range(0, 45).Select(i => (object)$"Message Line {i} cutted").ToList());
            var fileManager = new Border(1, 1, (int) (windowWidth * 0.6), (int) (windowHeight * 0.8))
            {
                Padding = (1,0),
                ContentControl = view
            };
            windowBorder.Print();
            fileManager.Print();

            CommandLine = windowHeight - 4;
            CommandLineWidth = windowWidth - 8;
        }
    }
}