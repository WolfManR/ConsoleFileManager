using System;
using ConsoleFileManager.FilesManager.Configurations;

namespace ConsoleFileManager.Render
{
    public class ConsoleHandler
    {
        private Configuration _configuration;
        private CommandLineConfiguration CommandLineConfiguration => _configuration.CommandLineConfiguration;

        public ConsoleHandler(Configuration configuration)
        {
            _configuration = configuration;
            ReturnCursorToCommandLineStartPosition();
        }


        public void ReturnCursorToCommandLineStartPosition()
        {
            Console.SetCursorPosition(CommandLineConfiguration.X, CommandLineConfiguration.Y);
        }

        public void ClearCommandLine()
        {
            ReturnCursorToCommandLineStartPosition();
            Console.Write(new string(' ', CommandLineConfiguration.Width));
            ReturnCursorToCommandLineStartPosition();
        }

        public void ReturnCursor(int currentCommandLength)
        {
            if (Console.CursorTop != CommandLineConfiguration.Y)
                ReturnCursorToCommandLineStartPosition();
            if (currentCommandLength != 0)
                Console.CursorLeft = CommandLineConfiguration.X + currentCommandLength;
        }
    }
}