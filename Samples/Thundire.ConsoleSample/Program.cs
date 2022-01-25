using Thundire.FileManager.Core;
using Thundire.FileManager.Core.ConsoleUI;
using Thundire.Infrastructure.FIlesManagement;

new ThundireFileManager()
    {
        Configuration = new() { OnClose = Close },
    }
    .SetFileManager()
    .StartConsole();


static void Close()
{

}