using ConsoleFileManager.Render.Abstracts;
using ConsoleFileManager.Render.Primitives;

namespace ConsoleFileManager.Render.Controls
{
    public class Border : ContentControl
    {
        public ContentControl ContentControl
        {
            get => _contentControl;
            init
            {
                _contentControl = value;
                _contentControl.X = X + 1 + Padding.Left;
                _contentControl.Y = Y + 1 + Padding.Top;
                _contentControl.Size = new Size(
                    Size.Width - Padding.Left - Padding.Right - 1,
                    Size.Height - Padding.Top - Padding.Bottom - 1);
            }
        }

        private readonly Figure _border = new();
        private readonly ContentControl _contentControl;

        public Border(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Size = new Size(width, height);
        }

        private void GenerateBorder()
        {
            _border.Clear();
            _border.Add(new Point(X, Y, Alphabet.BorderCornerUpLeft));
            _border.Add(new Point(X + Size.Width, Y, Alphabet.BorderCornerUpRight));
            _border.Add(new Point(X, Y + Size.Height - 1, Alphabet.BorderCornerDownLeft));
            _border.Add(new Point(X + Size.Width, Y + Size.Height - 1, Alphabet.BorderCornerDownRight));

            _border.Add(new HorizontalLine(X + 1, Y, Size.Width - 2, Alphabet.BorderHorizontal));
            _border.Add(new VerticalLine(X, Y + 1, Size.Height - 3, Alphabet.BorderVertical));
            _border.Add(new VerticalLine(X + Size.Width, Y + 1, Size.Height - 3, Alphabet.BorderVertical));
            _border.Add(new HorizontalLine(X + 1, Y + Size.Height - 1, Size.Width - 2, Alphabet.BorderHorizontal));
        }
        

        public override void Print()
        {
            GenerateBorder();
            _border.Draw();
            ContentControl?.Print();
        }
    }
}