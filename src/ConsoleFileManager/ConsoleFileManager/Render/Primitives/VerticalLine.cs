namespace ConsoleFileManager.Render.Primitives
{
    public class VerticalLine : Figure
    {
        public VerticalLine(int x, int y, int length, char symbol)
        {
            var last = length + y;
            for (var i = y; i <= last; i++)
            {
                Points.Add(new(x, i, symbol));
            }
        }
    }
}