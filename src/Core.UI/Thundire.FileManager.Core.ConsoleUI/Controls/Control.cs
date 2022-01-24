using Thundire.FileManager.Core.ConsoleUI.Primitives;

namespace Thundire.FileManager.Core.ConsoleUI.Controls
{
    public abstract class Control
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Size Size { get; set; }

        public Thickness Padding { get; set; }

        public virtual ContentPlace GetContentPlace()
        {
            var size = new Size(Size.Width - Padding.Left - Padding.Right, Size.Height - Padding.Top - Padding.Bottom);
            return new(X + Padding.Left, Y + Padding.Top, size);
        }

        public abstract void Print();
    }
}