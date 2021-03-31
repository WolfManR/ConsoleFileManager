using System;

namespace ConsoleFileManager.Render.Abstracts
{
    public class ContentPresenter
    {
        public string Content { get; set; }

        public void Print(int x, int y, int width)
        {
            Console.SetCursorPosition(x, y);
            var toPrint = Content.Length < width ? Content : Content[..width];
            Console.Write(toPrint);
        }
    }
}