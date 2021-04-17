namespace ConsoleFileManager.Render.Primitives
{
    public readonly struct Line
    {
        public readonly int X;
        public readonly int Y;
        public readonly string Content;

        public Line(int x, int y, int length, string content)
        {
            X = x;
            Y = y;

            if (content is null)
            {
                Content = new string(' ', length);
                return;
            }

            if (content.Length > length)
            {
                Content = content[..length];
                return;
            }

            if (content.Length < length)
            {
                Content = content + new string(' ', length - content.Length);
                return;
            }

            Content = content;
        }
    }
}