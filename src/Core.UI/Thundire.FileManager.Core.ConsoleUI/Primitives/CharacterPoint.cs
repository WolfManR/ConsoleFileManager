namespace Thundire.FileManager.Core.ConsoleUI.Primitives
{
    public readonly struct CharacterPoint
    {
        public CharacterPoint(int x, int y, char symbol)
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