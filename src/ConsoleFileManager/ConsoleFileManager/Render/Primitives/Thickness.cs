namespace ConsoleFileManager.Render.Primitives
{
    public struct Thickness
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }


        public Thickness(int padding) => Left = Top = Right = Bottom = padding;

        public Thickness(int horizontal, int vertical)
        {
            Left = Right = horizontal;
            Top = Bottom = vertical;
        }
        public Thickness(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        

        public static implicit operator Thickness(int padding) => new(padding);

        public static implicit operator Thickness((int horizontal, int vertical) padding) =>
            new(padding.horizontal, padding.vertical);

        public static implicit operator Thickness((int left, int top, int right, int bottom) padding) =>
            new(padding.left, padding.top, padding.right, padding.bottom);
    }
}