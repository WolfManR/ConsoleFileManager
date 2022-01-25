using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core;

public interface IRenderer
{
    void ShowDetails(Info info);
    void ShowView();
}