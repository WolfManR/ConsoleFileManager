using Thundire.FileManager.Core;
using Thundire.Infrastructure.FIlesManagement.Commands;

namespace Thundire.Infrastructure.FIlesManagement;

public static class Register
{
    public static void FileManagerCommandsRegister(
        this ICommandsRepository holder,
        IFilesManager filesManager,
        IRenderer renderer,
        IFilesManagementSystem filesManagementSystem) => holder
        .Register(new ChangeDirectoryCommand(filesManager))
        .Register(new CopyPathCommand(filesManager))
        .Register(new DeletePathCommand(filesManager))
        .Register(new ExitCommand(filesManagementSystem))
        .Register(new ShowDetailsCommand(filesManager, renderer))
    ;
}