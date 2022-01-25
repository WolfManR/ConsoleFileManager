using Thundire.FileManager.Core;
using Thundire.Infrastructure.FIlesManagement.Commands;

namespace Thundire.Infrastructure.FIlesManagement;

public static class Register
{
    public static ThundireFileManager SetFileManager(this ThundireFileManager self)
    {
        self.FilesManager = new FileManager();
        self.OnInitialized(manager =>
        {
            manager.CommandsRepository.FileManagerCommandsRegister(manager.FilesManager, manager.Renderer);
        });
        return self;
    }

    private static void FileManagerCommandsRegister(
        this ICommandsRepository holder,
        IFilesManager filesManager,
        IRenderer renderer) => holder
        .Register(new ChangeDirectoryCommand(filesManager))
        .Register(new CopyPathCommand(filesManager))
        .Register(new DeletePathCommand(filesManager))
        .Register(new ShowDetailsCommand(filesManager, renderer))
    ;
}