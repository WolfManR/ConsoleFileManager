using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core.ConsoleUI;

public interface IConsoleRenderer : IRenderer
{
    void PrintCommand(string command);
    string Request(string[] message);
    void Report(string[] cutMessage);
    (int width, int maxLines) GetReportSize();
    Info GetSelectedInfo();
    bool CanMoveCursorLeft();
    bool CanMoveCursorRight(int currentCommandLength);
    int MoveCursorRight();
    int MoveCursorLeft();
    void ReplaceCommandLineText(int index, string toReplace);
    void AppendCharToCommandLine(char toAppend);
    void UpdateDirectoryView(List<Info> directoryInfo, Info currentSelected);
    void NextLine();
    void PrevLine();
    void ReturnCursor();
    void ClearCommandLine();
    void ReturnCursorToCommandLineStartPosition();
}