using System;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using ConsoleFileManager.FilesManager.Configurations;
using ConsoleFileManager.Render.Controls;
using ConsoleFileManager.Render.Primitives;
using static System.Console;
namespace ConsoleFileManager.Render
{
    public class ConsoleHandler
    {
        private readonly Configuration _configuration;
        private CommandLineConfiguration CommandLineConfiguration => _configuration.CommandLineConfiguration;
        private int _commandIndex;
        private Size _messageBoxContentSize;
        private (int x, int y) _messageBoxContentStartPoint;
        public ConsoleHandler(Configuration configuration) => _configuration = configuration;

        public bool CanMoveCursorLeft() => _commandIndex > 0;
        public bool CanMoveCursorRight(int currentCommandLength) => _commandIndex <= currentCommandLength - 1;

        public void ReturnCursorToCommandLineStartPosition()
        {
            SetCursorPosition(CommandLineConfiguration.X, CommandLineConfiguration.Y);
        }

        public void ClearCommandLine()
        {
            ReturnCursorToCommandLineStartPosition();
            Write(new string(' ', CommandLineConfiguration.Width));
            ReturnCursorToCommandLineStartPosition();
            _commandIndex = 0;
        }

        public void ReturnCursor()
        {
            if (CursorTop != CommandLineConfiguration.Y)
                ReturnCursorToCommandLineStartPosition();
            if (_commandIndex != 0)
                CursorLeft = CommandLineConfiguration.X + _commandIndex;
        }

        public int MoveCursorRight()
        {
            CursorLeft++;
            _commandIndex++;
            return _commandIndex;
        }

        public int MoveCursorLeft()
        {
            CursorLeft--;
            _commandIndex--;
            return _commandIndex;
        }

        public void ReplaceCommandLineText(int index, string toReplace)
        {
            var startPoint = GetCursorPosition();
            ReturnCursorToCommandLineStartPosition();
            CursorLeft += index;
            Write(toReplace);
            SetCursorPosition(startPoint.Left, startPoint.Top);
        }

        public void AppendCharToCommandLine(char toAppend)
        {
            _commandIndex++;
            Write(toAppend);
        }

        private void ConfigureConsole()
        {
            Console.BufferHeight = Console.WindowHeight = _configuration.WindowHeight;
            Console.BufferWidth = Console.WindowWidth = _configuration.WindowWidth;
        }

        public void ShowView()
        {
            ConfigureConsole();
            var (windowWidth, windowHeight) = (_configuration.WindowWidth - 1, _configuration.WindowHeight);
            var windowBorder = new Border(0, 0, windowWidth, windowHeight);
            var fileManagerWidth = (int) (windowWidth * 0.6);
            var fileManagerHeight = (int) (windowHeight * 0.8);
            var fileManager = new Border(1, 1, fileManagerWidth, fileManagerHeight)
            {
                Padding = (1, 0)
            };
            CommandLineConfiguration.Width = fileManagerWidth - 2;
            var messageBoxBorder = new Border(fileManagerWidth + 2, fileManagerHeight + 1, windowWidth - fileManagerWidth -3,
                windowHeight - fileManagerHeight-2);
            _messageBoxContentStartPoint = (fileManagerWidth + 2, fileManagerHeight + 1);
            var (x, y, size) = messageBoxBorder.GetContentPlace();
            _messageBoxContentSize = size;
            _messageBoxContentStartPoint = (x: x, y: y);
            windowBorder.Print();
            fileManager.Print();
            messageBoxBorder.Print();

            ReturnCursorToCommandLineStartPosition();
        }


        internal string Request(string[] message)
        {
            Report(message);

            ClearCommandLine();
            return GetResponse();
        }

        private string GetResponse()
        {
            var width = CommandLineConfiguration.Width;
            StringBuilder buffer = new();
            while (true)
            {
                var key = ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    return buffer.ToString();
                if (buffer.Length + 1 == width)
                    continue;
                buffer.Append(key.KeyChar);
                Write(key.KeyChar);
            }
        }

        public void Report(string[] message)
        {
            if (message.Length <= 0) throw new ArgumentException("Message was empty");
            var (startX, startY, width, height) =
            (
                _messageBoxContentStartPoint.x,
                _messageBoxContentStartPoint.y,
                _messageBoxContentSize.Width,
                _messageBoxContentSize.Height
            );

            var block = PrepareBlock(message, startX, startY, width, height);
            PrintBlock(block);

            ReturnCursor();
        }

        public (int width, int maxLines) GetReportSize()
        {
            var (width,maxLines) = _messageBoxContentSize;
            return (width,maxLines);
        }

        public void PrintCommand(string command)
        {
            ClearCommandLine();
            Write(command);
            _commandIndex = command.Length;
        }

        private Line[] PrepareBlock(string[] message, int x, int y, int width, int height)
        {
            var block = new Line[height];
            var existLines = message.Length;
            for (int i = 0,j = y; i < height; i++, j++)
            {
                var current = existLines <= i 
                    ? new Line(x, j, width, null) 
                    : new Line(x, j, width, message[i]);
                block[i] = current;
            }

            return block;
        }


        private static void PrintWithReturnToLastPosition(Action action)
        {
            var lastPosition = GetCursorPosition();
            action?.Invoke();
            SetCursorPosition(lastPosition.Left, lastPosition.Top);
        }

        private static void PrintLine(Line line)
        {
                SetCursorPosition(line.X, line.Y);
                Write(line.Content);
        }

        private static void PrintBlock(Line[] block)
        {
             for (var i = 0; i < block.Length; i++)
             {
                 var line = block[i];
                PrintLine(line);
             }
        }


        private readonly struct Line
        {
            public readonly int X;
            public readonly int Y;
            public readonly string Content;

            public Line(int x, int y, int length, string content)
            {
                X = x;
                Y = y;

                if (content is null)
                {
                    Content = new string(' ', length);
                    return;
                }

                if (content.Length > length)
                {
                    Content = content[..length];
                    return;
                }

                if (content.Length < length)
                {
                    Content = content + new string(' ', length - content.Length);
                    return;
                }

                Content = content;
            }
        }
    }
}