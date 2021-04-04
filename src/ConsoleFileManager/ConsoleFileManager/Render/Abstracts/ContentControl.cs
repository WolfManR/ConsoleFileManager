using ConsoleFileManager.Render.Primitives;

namespace ConsoleFileManager.Render.Abstracts
{
    public abstract class ContentControl
    {
        public int X { get; set; }
        public int Y { get; set; }
        public virtual Size Size { get; set; }

        public Thickness Margin { get; set; }
        public Thickness Padding { get; set; }
        public abstract void Print();

        protected (int,int) CalculateOuterStartPoint() => (X + Margin.Left, Y + Margin.Top);

        protected (int x, int y, int width, int height) CalculateContentPlace()
        {
            (int x, int y, int width, int height) result = (0, 0, 0, 0);
            (result.x,result.y) = CalculateOuterStartPoint();
            result.width = Size.Width - Padding.Right - Padding.Left;
            result.height = Size.Height - Padding.Top - Padding.Bottom;

            return result;
        }
    }
}