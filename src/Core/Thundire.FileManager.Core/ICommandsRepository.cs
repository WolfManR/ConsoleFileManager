using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core;

public interface ICommandsRepository
{
    void ExecuteCommand();
    ICommandsRepository Register(FileManagerCommand command);
    void SetNextCommand();
    void SetPreviousCommand();
    void SwitchMode();
}