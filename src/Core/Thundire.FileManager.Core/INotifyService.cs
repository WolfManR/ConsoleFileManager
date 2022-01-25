namespace Thundire.FileManager.Core;

public interface INotifyService
{
    bool Confirm(string message);
    void PrintCommand(string command);
    void Report(string message);
    void Report(Exception exception);
}