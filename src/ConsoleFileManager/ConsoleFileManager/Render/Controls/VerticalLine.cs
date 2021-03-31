using ConsoleFileManager.Render.Abstracts;

namespace ConsoleFileManager.Render.Controls
{
    public class VerticalLine : Figure
    {
        public VerticalLine(int x, int y, int length, char symbol)
        {
            var last = length + y;
            for (var i = y; i <= last; i++)
            {
                points.Add(new(x, i, symbol));
            }
        }
    }
}