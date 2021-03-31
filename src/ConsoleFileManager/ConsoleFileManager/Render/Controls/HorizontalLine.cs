using ConsoleFileManager.Render.Abstracts;

namespace ConsoleFileManager.Render.Controls
{
    public class HorizontalLine : Figure
    {
        public HorizontalLine(int x, int y, int length, char symbol)
        {
            var last = length + x;
            for (var i = x; i <= last; i++)
            {
                points.Add(new(i, y, symbol));
            }
        }
    }
}