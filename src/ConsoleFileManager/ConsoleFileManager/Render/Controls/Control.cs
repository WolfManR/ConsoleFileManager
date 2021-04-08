using ConsoleFileManager.Render.Primitives;

namespace ConsoleFileManager.Render.Controls
{
    public abstract class Control
    {
        public int X { get; set; }
        public int Y { get; set; }
        public virtual Size Size { get; set; }

        public Thickness Margin { get; set; }
        public Thickness Padding { get; set; }
        public abstract void Print();
    }
}