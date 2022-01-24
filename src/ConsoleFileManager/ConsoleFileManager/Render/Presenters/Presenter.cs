using ConsoleFileManager.Render.Primitives;

namespace ConsoleFileManager.Render.Presenters
{
    public abstract class Presenter<T>
    {
        public abstract void Print(ContentPlace contentPlace, T toPresent);
    }
}