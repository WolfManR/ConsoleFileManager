using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleFileManager.Infrastructure;

using ConsoleFileManager.Render;
using ConsoleFileManager.Render.Controls;

namespace ConsoleFileManager
{
    public class InputHandler
    {
        private readonly Configuration _config;
        private readonly InputCommandsParser _parser;
        private static bool _isExit;

        private bool InputMode { get; set; } = true;
        private int CommandLine { get; set; }
        private int CommandLineWidth { get; set; }
        private int CommandLineStartPosition { get; set; } = 4;
        public event Action OnClose;

        public InputHandler(Configuration config, InputCommandsParser parser)
        {
            this._config = config;
            this._parser = parser;
            Initialize();
        }

        private void Initialize()
        {
            ConfigureConsole(_config);
            MainView();
            ReturnCursorToCommandLine();
        }

        public void Start()
        {
            while (!_isExit)
            {
                var inputKey = Console.ReadKey(true);
                _isExit = HandleKey(inputKey);
            }
            OnClose?.Invoke();
        }


        private void ConfigureConsole(Configuration config)
        {
            Console.BufferHeight = Console.WindowHeight = config.WindowHeight;
            Console.BufferWidth = Console.WindowWidth = config.WindowWidth;
            CommandLine = config.WindowHeight - 4;
            CommandLineWidth = config.WindowWidth - 8;
        }

        private void MainView()
        {
            var (windowWidth, windowHeight) = (_config.WindowWidth - 1, _config.WindowHeight);

            var windowBorder = new Border(0, 0, windowWidth, windowHeight);
            var view = ViewHandler.View = new ListView(20, Enumerable.Range(0, 45).Select(i => (object)$"Message Line {i} cutted").ToList());
            var fileManager = new Border(1, 1, (int)(windowWidth * 0.6), (int)(windowHeight * 0.8))
            {
                Padding = (1, 0),
                ContentControl = view
            };
            windowBorder.Print();
            fileManager.Print();
        }

        

        private bool HandleKey(ConsoleKeyInfo inputKey)
        {
            switch (inputKey.Key)
            {
                case ConsoleKey.Escape:
                    return true;
                case ConsoleKey.Tab:
                    InputMode = !InputMode;
                    Console.CursorVisible = !Console.CursorVisible;
                    break;
            }

            if (InputMode)
            {
                HandleCommandMode(inputKey);
            }
            else
            {
                HandleViewMode(inputKey);
            }

            return false;
        }



        private void ReturnCursorToCommandLine()
        {
            Console.SetCursorPosition(CommandLineStartPosition, CommandLine);
        }

        private void ClearCommandLine()
        {
            ReturnCursorToCommandLine();
            Console.Write(new string(' ', CommandLineWidth));
            ReturnCursorToCommandLine();
        }

        private void PrintKey(ConsoleKeyInfo input)
        {
            var msg = input.Key switch
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

        

        private void HandleViewMode(ConsoleKeyInfo input)
        {
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    ViewHandler.Prev(); break;
                case ConsoleKey.DownArrow:
                    ViewHandler.Next(); break;
            };
        }

        private StringBuilder buffer = new StringBuilder();
        private List<string> history = new List<string>();
        private int CurrentHistoryLine;
        private int CommandIndex;
        private void HandleCommandMode(ConsoleKeyInfo input)
        {
            if (input.Key == ConsoleKey.Enter && buffer.Length > 0)
            {
                var command = buffer.ToString();
                buffer.Clear();
                CommandIndex = 0;
                HandleCommandLine(command);
                history.Add(command);
                CurrentHistoryLine = -1;
                return;
            }

            if (input.Key == ConsoleKey.UpArrow || input.Key == ConsoleKey.DownArrow)
            {
                if (history.Count <= 0) return;

                if (CurrentHistoryLine >= history.Count)
                    CurrentHistoryLine = -1;

                if (input.Key == ConsoleKey.UpArrow)
                {
                    CurrentHistoryLine = CurrentHistoryLine switch
                                         {
                                             0                                    => 1,
                                             var index when index < history.Count => CurrentHistoryLine + 1,
                                             _                                    => history.Count - 1
                                         };
                }

                if (input.Key == ConsoleKey.DownArrow)
                {
                    CurrentHistoryLine = CurrentHistoryLine switch
                                         {
                                             0   => 0,
                                             < 0 => history.Count - 1,
                                             _   => CurrentHistoryLine - 1
                                         };
                }

                ReturnCursorToCommandLine();
                var command = history[CurrentHistoryLine];
                buffer.Clear().Append(command);
                Console.Write(command);
                return;
            }
            

            if (input.Key == ConsoleKey.LeftArrow)
            {
                if (CommandIndex <= 0) return;
                Console.CursorLeft--;
                CommandIndex--;
                return;
            }

            if (input.Key == ConsoleKey.RightArrow)
            {
                if(CommandIndex > buffer.Length - 1) return;
                Console.CursorLeft++;
                CommandIndex++;
                return;
            }

            if (input.Key == ConsoleKey.Backspace)
            {
                if (CommandIndex <= 0) return;
                buffer.Remove(--CommandIndex, 1);
                Console.CursorLeft--;
                Console.Write(' ');
                Console.CursorLeft--;
                return;
            }

            if (char.IsLetterOrDigit(input.KeyChar) || char.IsSeparator(input.KeyChar) || char.IsPunctuation(input.KeyChar))
            {
                CommandIndex++;
                buffer.Append(input.KeyChar);
                Console.Write(input.KeyChar);
            }
        }

        private void HandleCommandLine(string input)
        {
            var toHandle = input.Split(' ').AsSpan();
            var commandName = toHandle[0];
            var args = toHandle.Length > 1 ? toHandle[1..] : Span<string>.Empty;

            var command = _parser.Find(commandName);
            if (command is null)
                Console.WriteLine($"Not registered command for {commandName} to handle input:\n{string.Join(" ", args.ToArray())}");
            try
            {
                _parser.ExecuteCommand(command, args.ToArray());
            }
            catch (Exception e)
            {
                ViewHandler.PrintException(e);
            }
        }
    }
}