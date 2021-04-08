using ConsoleFileManager.Render.Primitives;

namespace ConsoleFileManager.Render.Controls
{
    public sealed class Border : Control
    {
        private readonly Figure _border = new();

        public Border(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Size = new Size(width, height);
        }

        private void GenerateBorder()
        {
            _border.Clear();
            _border.Add(new CharacterPoint(X, Y, Alphabet.BorderCornerUpLeft));
            _border.Add(new CharacterPoint(X + Size.Width, Y, Alphabet.BorderCornerUpRight));
            _border.Add(new CharacterPoint(X, Y + Size.Height - 1, Alphabet.BorderCornerDownLeft));
            _border.Add(new CharacterPoint(X + Size.Width, Y + Size.Height - 1, Alphabet.BorderCornerDownRight));

            _border.Add(new HorizontalLine(X + 1, Y, Size.Width - 2, Alphabet.BorderHorizontal));
            _border.Add(new VerticalLine(X, Y + 1, Size.Height - 3, Alphabet.BorderVertical));
            _border.Add(new VerticalLine(X + Size.Width, Y + 1, Size.Height - 3, Alphabet.BorderVertical));
            _border.Add(new HorizontalLine(X + 1, Y + Size.Height - 1, Size.Width - 2, Alphabet.BorderHorizontal));
        }


        public override void Print()
        {
            GenerateBorder();
            _border.Draw();
        }

        private static class Alphabet
        {
            public const char BorderVertical = '║';
            public const char BorderVerticalLeft = '╣';
            public const char BorderVerticalRight = '╠';

            public const char BorderCenter = '╬';

            public const char BorderHorizontal = '═';
            public const char BorderHorizontalUp = '╩';
            public const char BorderHorizontalDown = '╦';


            public const char BorderCornerUpLeft = '╔';
            public const char BorderCornerUpRight = '╗';
            public const char BorderCornerDownRight = '╝';
            public const char BorderCornerDownLeft = '╚';
        }
    }
}