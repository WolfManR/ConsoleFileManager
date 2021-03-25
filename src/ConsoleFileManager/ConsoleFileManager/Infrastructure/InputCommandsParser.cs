using System.Collections.Generic;
using ConsoleFileManager.Infrastructure.Commands;

namespace ConsoleFileManager.Infrastructure
{
    public class InputCommandsParser
    {
        private List<Command> commands = new();

        public Command Find(string commandName)
        {
            static bool IsCorrectCommand(Command command, string commandName)
            {
                if(command.Name == commandName) return true;
                for (var i = 0; i < command.Abbreviations.Length; i++)
                {
                    if (command.Abbreviations[i] == commandName)
                        return true;
                }
                return false;
            }

            for (var i = 0; i < commands.Count; i++)
            {
                if (IsCorrectCommand(commands[i], commandName)) 
                    return commands[i];
            }

            return null;
        }

        public InputCommandsParser Register(Command command)
        {
            var abbreviations = string.Join(' ', command.Abbreviations);
            if (commands.Find(c => string.Join(' ',c.Abbreviations) == abbreviations) is null)
                commands.Add(command);
            return this;
        }

        public void ExecuteCommand(Command command, string[] args)
        {
            if(command.CanHandle(args))
                command.Handle(args);
        }
    }
}