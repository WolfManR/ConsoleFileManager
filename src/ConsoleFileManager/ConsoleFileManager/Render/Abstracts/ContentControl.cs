using ConsoleFileManager.Render.Primitives;

namespace ConsoleFileManager.Render.Abstracts
{
    public abstract class ContentControl
    {
        public Size Size { get; set; }

        public Thickness Margin { get; set; }
        public Thickness Padding { get; set; }

        public abstract void Print(int x, int y);

        protected (int,int) CalculateOuterStartPoint(int x, int y) => (x + Margin.Left, y + Margin.Top);

        protected (int x, int y, int width, int height) CalculateContentPlace(int x, int y)
        {
            var (outX,outY) = CalculateOuterStartPoint(x, y);
            var width = Size.Width - Padding.Right - Padding.Left;
            var height = Size.Height - Padding.Top - Padding.Bottom;
            return (outX + Padding.Left, outY + Padding.Top, width, height);
        }
    }
}