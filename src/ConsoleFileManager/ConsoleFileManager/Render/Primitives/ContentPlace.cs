using System;

namespace ConsoleFileManager.Render.Primitives
{
    public readonly struct ContentPlace
    {
        public readonly int X;
        public readonly int Y;
        public readonly Size Size;


        public ContentPlace(int x, int y, Size size)
        {
            X = x;
            Y = y;
            Size = size;
        }

        public void Deconstruct(out int x, out int y, out Size size)
        {
            x = X;
            y = Y;
            size = Size;
        }

        internal void Deconstruct(out int x, out int y, out int width, out int height)
        {
            x = X;
            y = Y;
            width = Size.Width;
            height = Size.Height;
        }
    }
}