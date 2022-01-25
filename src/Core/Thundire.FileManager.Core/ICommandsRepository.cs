using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core;

public interface ICommandsRepository
{
    ICommandsRepository Register(FileManagerCommand command);
}