using System;
using ConsoleFileManager.FilesManager.Configurations;
using static System.Console;
namespace ConsoleFileManager.Render
{
    public class ConsoleHandler
    {
        private Configuration _configuration;
        private CommandLineConfiguration CommandLineConfiguration => _configuration.CommandLineConfiguration;
        private int _commandIndex;
        public bool CanMoveCursorLeft() => _commandIndex <= 0;
        public bool CanMoveCursorRight(int currentCommandLength) => _commandIndex > currentCommandLength - 1;
        public ConsoleHandler(Configuration configuration)
        {
            _configuration = configuration;
            ReturnCursorToCommandLineStartPosition();
        }


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
    }
}