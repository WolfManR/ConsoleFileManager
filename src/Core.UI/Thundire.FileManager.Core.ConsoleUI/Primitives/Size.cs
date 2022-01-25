namespace Thundire.FileManager.Core.ConsoleUI.Primitives
{
    public struct Size : IEquatable<Size>
    {
        public int Width { get; set; }
        public int Height { get; set; }


        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }


        public override bool Equals(object obj) => obj is Size size && Equals(size);
        public bool Equals(Size other) => Width == other.Width && Height == other.Height;

        internal void Deconstruct(out int width, out int height)
        {
            width = Width;
            height = Height;
        }

        public static bool operator ==(Size left, Size right) => left.Equals(right);

        public static bool operator !=(Size left, Size right) => !(left == right);


        public static Size operator -(Size self, int value) => new(self.Width - value, self.Height - value);
    }
}