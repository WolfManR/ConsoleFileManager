using System;
using ConsoleFileManager.Infrastructure;
using ConsoleFileManager.Render;

namespace ConsoleFileManager
{
    public class InputHandler
    {
        private InputCommandsParser parser;

        public InputHandler(InputCommandsParser parser)
        {
            this.parser = parser;
        }

        public void Handle(string input)
        {
            var toHandle = input.Split(' ').AsSpan();
            var commandName = toHandle[0];
            var args = toHandle.Slice(1);

            var command = parser.Find(commandName);
            if (command is null)
                Console.WriteLine($"Not registered command for {commandName} to handle input:\n{string.Join(" ", args.ToArray())}");
            try
            {
                parser.ExecuteCommand(command, args.ToArray());
            }
            catch (Exception e)
            {
                ViewHandler.PrintException(e);
            }
        }
    }
}