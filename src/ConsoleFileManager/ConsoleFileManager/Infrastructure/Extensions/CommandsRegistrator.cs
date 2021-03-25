using ConsoleFileManager.Infrastructure.Commands;

namespace ConsoleFileManager.Infrastructure.Extensions
{
    public static class CommandsRegistrator
    {
        public static InputCommandsParser CommandsRegistration(this InputCommandsParser parser) => parser
            .Register(new ListPathCommand())
            .Register(new ExitCommand())
        ;
    }
}