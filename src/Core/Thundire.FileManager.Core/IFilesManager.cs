using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core;

public interface IFilesManager
{
    void ChangeDirectory(string directory);
    void ChangeDirectory(DirectoryMove move, string path = null);
    void CopyFile(string from, string to);
    void CopyDirectory(string from, string to);
    void DeleteDirectory(string directory, bool withChild = false);
    void DeleteFile(string filePath);
    IEnumerable<Info> GetDirectoryStruct();
    bool StringPathIsDirectory(string path);
    string RebasePath(string path);

    event Action OnDirectoryChanged;
}