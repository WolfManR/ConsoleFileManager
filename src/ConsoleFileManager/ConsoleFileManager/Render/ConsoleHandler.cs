using System;
using System.Collections.Generic;
using System.Text;

using ConsoleFileManager.FilesManager.Configurations;
using ConsoleFileManager.FilesManager.Models;
using ConsoleFileManager.Render.Controls;
using ConsoleFileManager.Render.Presenters;
using ConsoleFileManager.Render.Primitives;

using static System.Console;


namespace ConsoleFileManager.Render
{
    public partial class ConsoleHandler
    {
        private readonly Configuration _configuration;
        private CommandLineConfiguration CommandLineConfiguration => _configuration.CommandLineConfiguration;
        private int _commandIndex;

        private ContentPlace _messageBoxPlace;

        private ListView _directoryView;

        private Border _fileManagerBorder;
        private Border _messageBoxBorder;
        private Border _windowBorder;
        private ContentPlace _detailsPlace;
        private DetailsInfoPresenter _detailsPresenter = new();
        public ConsoleHandler(Configuration configuration)
        {
            _configuration = configuration;
            ConfigureView();
        }


        #region View Configuration

        private void ConfigureView()
        {
            BufferHeight = WindowHeight = _configuration.WindowHeight;
            BufferWidth = WindowWidth = _configuration.WindowWidth;

            // Configure Controls

            var (windowWidth, windowHeight) = (_configuration.WindowWidth - 1, _configuration.WindowHeight);
            // Outer
            _windowBorder = new Border(0, 0, windowWidth, windowHeight);

            // Current Directory
            var fileManagerWidth = (int)(windowWidth * 0.6);
            var fileManagerHeight = (int)(windowHeight * 0.8);
            _fileManagerBorder = new Border(1, 1, fileManagerWidth, fileManagerHeight) { Padding = (1, 0) };
            _directoryView = new ListView(_fileManagerBorder.GetContentPlace());

            // Command Line
            CommandLineConfiguration.Width = fileManagerWidth - 2;

            // Message Box
            _messageBoxBorder = new Border(fileManagerWidth + 2, fileManagerHeight + 1, windowWidth - fileManagerWidth - 3,
                windowHeight - fileManagerHeight - 2);
            _messageBoxPlace = _messageBoxBorder.GetContentPlace();


            // Details Place
            _detailsPlace = new ContentPlace(fileManagerWidth + 4, 2,
                new Size(windowWidth - fileManagerWidth - 4, fileManagerHeight - 2));
        }

        public void ShowView()
        {
            _windowBorder.Print();
            _fileManagerBorder.Print();
            _messageBoxBorder.Print();
        }
        
        #endregion



        #region Command Line

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

        public void PrintCommand(string command)
        {
            ClearCommandLine();
            Write(command);
            _commandIndex = command.Length;
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
        
        #endregion

        

        #region Message Box

        public void Report(string[] message)
        {
            if (message.Length <= 0) throw new ArgumentException("Message was empty");
            var (startX, startY, width, height) = _messageBoxPlace;

            var block = PrepareBlock(message, startX, startY, width, height);
            PrintBlock(block);

            ReturnCursor();
        }

        public (int width, int maxLines) GetReportSize()
        {
            var (width, maxLines) = _messageBoxPlace.Size;
            return (width, maxLines);
        }
        
        #endregion

        

        #region Details Section

        public void ShowDetails(Info info)
        {
            _detailsPresenter.Print(_detailsPlace, info);
            ReturnCursor();
        }
        
        #endregion



        #region Directory View

        public void UpdateDirectoryView(List<Info> directoryInfo, Info currentSelected)
        {
            _directoryView.ChangeOutput(directoryInfo);
            _directoryView.SetSelected(currentSelected);
        }

        #endregion



        #region Helpers

        private Line[] PrepareBlock(string[] message, int x, int y, int width, int height)
        {
            var block = new Line[height];
            var existLines = message.Length;
            for (int i = 0, j = y; i < height; i++, j++)
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

        #endregion
    }
}