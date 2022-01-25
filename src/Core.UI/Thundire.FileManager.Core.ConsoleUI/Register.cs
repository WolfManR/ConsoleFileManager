using Thundire.FileManager.Core.ConsoleUI.Commands.CommandModeCommands;
using Thundire.FileManager.Core.ConsoleUI.Commands.ViewModeCommands;
using Thundire.FileManager.Core.ConsoleUI.Commands;
using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core.ConsoleUI;

public static class Register
{
    public static void StartConsole(this ThundireFileManager self)
    {
        var consoleLifeTimeService = new ConsoleLifeTimeService(self);

        self.LifeTimeService = consoleLifeTimeService;

        self.OnInitialized(manager =>
        {
            if (manager is not { Renderer: IConsoleRenderer renderer, CommandsRepository: IConsoleCommandsRepository repository })
            {
                throw new InvalidOperationException("Console renderer not correctly setup, maybe you rewrite some services");
            }
            CommandModeCommandsRegister(repository, renderer);
            ViewModeCommandsRegister(repository, renderer);
            SharedCommandsRegister(repository);
        });

        consoleLifeTimeService.Start();
    }

    private static void CommandModeCommandsRegister(IConsoleCommandsRepository holder, IConsoleRenderer renderer) => holder
        .Register(new AppendCharToCommandLineCommand(holder, renderer))
        .Register(new ExecuteFileManagerCommand(holder))
        .Register(new MoveCursorLeftCommand(renderer))
        .Register(new MoveCursorRightCommand(holder, renderer))
        .Register(new NextCommandCommand(holder))
        .Register(new PreviousCommandCommand(holder))
        .Register(new RemovePreviousCharFromCommandLineCommand(holder, renderer))
    ;

    private static void ViewModeCommandsRegister(IConsoleCommandsRepository holder, IConsoleRenderer renderer) => holder
        .Register(new MoveCursorToNextLineCommand(renderer), InputHandleMode.List)
        .Register(new MoveCursorToPreviousLineCommand(renderer), InputHandleMode.List)
        .Register(new ShowSelectedLineInfoCommand(renderer), InputHandleMode.List)
    ;

    private static void SharedCommandsRegister(IConsoleCommandsRepository holder) => holder
        .Register(new SwitchInputHandleModeCommand(holder), InputHandleMode.Shared)
    ;

    private static void LifeTimeCommandsRegister(IConsoleCommandsRepository holder, ILifeTimeService lifeTimeService) => holder
        .Register(new ExitCommand(lifeTimeService))
    ;
}