using System;

namespace ConsoleFileManager.Render.Primitives
{
    public struct Point
    {
        public Point(int x, int y, char symbol)
        {
            Symbol = symbol;
            X = x;
            Y = y;
        }

        public readonly int X;
        public readonly int Y;
        public readonly char Symbol;

        public void Draw()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(Symbol);
        }
    }
}