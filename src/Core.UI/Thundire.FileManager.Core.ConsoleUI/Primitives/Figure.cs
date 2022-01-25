using System.Collections;

namespace Thundire.FileManager.Core.ConsoleUI.Primitives
{
    public class Figure : IEnumerable<CharacterPoint>
    {
        protected readonly List<CharacterPoint> Points = new();

        public virtual void Draw()
        {
            for (var i = 0; i < Points.Count; i++)
            {
                Points[i].Draw();
            }
        }

        public void Add(CharacterPoint point) => Points.Add(point);
        public void Add(IEnumerable<CharacterPoint> points) => Points.AddRange(points);
        public void Clear() => Points.Clear();

        public IEnumerator<CharacterPoint> GetEnumerator() => Points.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}