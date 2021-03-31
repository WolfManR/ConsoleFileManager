using ConsoleFileManager.Render.Abstracts;
using ConsoleFileManager.Render.Primitives;

namespace ConsoleFileManager.Render.Controls
{
    public class Border : Figure
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int Padding { get; set; }
        
        public ContentControl ContentControl { get; set; }


        public Border(int x, int y, int width, int height)
        {
            Top = x;
            Left = y;
            Width = width;
            Height = height;
        }

        private void GenerateBorder()
        {
            points.Add(new Point(Left, Top, Alphabet.BorderCornerUpLeft));
            points.Add(new Point(Left + Width, Top, Alphabet.BorderCornerUpRight));
            points.Add(new Point(Left, Top + Height - 1, Alphabet.BorderCornerDownLeft));
            points.Add(new Point(Left + Width, Top + Height - 1, Alphabet.BorderCornerDownRight));

            points.AddRange(new HorizontalLine(Left + 1, Top, Width - 2, Alphabet.BorderHorizontal));
            points.AddRange(new VerticalLine(Left, Top + 1, Height - 3, Alphabet.BorderVertical));
            points.AddRange(new VerticalLine(Left + Width, Top + 1, Height - 3, Alphabet.BorderVertical));
            points.AddRange(new HorizontalLine(Left + 1, Top + Height - 1, Width - 2, Alphabet.BorderHorizontal));
        }

        #region Overrides of Figure

        /// <inheritdoc />
        public override void Draw()
        {
            GenerateBorder();
            base.Draw();
            ContentControl.Print(Left + 1 + Padding, Top + 1 + Padding);
        }

        #endregion
    }
}