using Thundire.FileManager.Core.ConsoleUI.Primitives;

namespace Thundire.FileManager.Core.ConsoleUI.Presenters
{
    public abstract class Presenter<T>
    {
        public abstract void Print(ContentPlace contentPlace, T toPresent);
    }
}