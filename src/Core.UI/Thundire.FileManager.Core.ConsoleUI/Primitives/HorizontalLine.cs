namespace Thundire.FileManager.Core.ConsoleUI.Primitives
{
    public class HorizontalLine : Figure
    {
        public HorizontalLine(int x, int y, int length, char symbol)
        {
            var last = length + x;
            for (var i = x; i <= last; i++)
                Points.Add(new(i, y, symbol));
        }
    }
}