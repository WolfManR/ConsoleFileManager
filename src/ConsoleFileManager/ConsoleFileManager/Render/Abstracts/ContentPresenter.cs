using System;

namespace ConsoleFileManager.Render.Abstracts
{
    public class ContentPresenter
    {
        protected virtual string PrepairContent(int width, object content)
        {
            var msg = content?.ToString();
            if (msg is not null)
            {
                if(msg.Length < width)
                    return msg + new string(' ', width - msg.Length);
                if (msg.Length > width)
                    return msg[..width];
            }
            return msg ?? new string(' ', width);
        }

        public void Print(int x, int y, int width, object content)
        {
            Console.SetCursorPosition(x, y);
            var toPrint = PrepairContent(width,content);
            Console.Write(toPrint);
        }
    }
}