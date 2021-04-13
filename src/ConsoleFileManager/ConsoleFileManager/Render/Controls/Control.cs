using ConsoleFileManager.Render.Primitives;

namespace ConsoleFileManager.Render.Controls
{
    public abstract class Control
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Size Size { get; set; }
        
        public Thickness Padding { get; set; }

        public virtual (int x, int y, Size size) GetContentPlace()
        {
            var size = new Size(Size.Width - Padding.Left - Padding.Right, Size.Height - Padding.Top - Padding.Bottom);
            return (X+Padding.Left,Y+Padding.Top,size);
        }

        public abstract void Print();
    }
}