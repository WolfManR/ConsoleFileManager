using Thundire.FileManager.Core.ConsoleUI.Commands;
using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core.ConsoleUI;

public interface IConsoleCommandsRepository : ICommandsRepository
{
    bool HasCommandLine { get; }
    int CommandLineLength { get; }
    bool HasHistory { get; }

    event Action<string> OnCommandChanged;
    event Action OnCommandExecuted;
    event Action<InputHandleMode> OnInputHandleModeChanged;

    void AppendCharToCommandLine(char toAppend);
    void HandleInput(ConsoleKeyInfo key);
    CommandsRepository Register(ConsoleKeyCommand command, InputHandleMode mode = InputHandleMode.CommandLine);
    string RemoveChar(int index);
}