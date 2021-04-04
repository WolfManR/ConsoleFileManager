using System.Collections;
using System.Collections.Generic;
using ConsoleFileManager.Render.Primitives;

namespace ConsoleFileManager.Render.Abstracts
{
    public class Figure : IEnumerable<Point>
    {
        protected readonly List<Point> Points = new();

        public virtual void Draw()
        {
            for (var i = 0; i < Points.Count; i++)
            {
                Points[i].Draw();
            }
        }

        public void Add(Point point) => Points.Add(point);
        public void Add(IEnumerable<Point> points) => Points.AddRange(points);
        public void Clear() => Points.Clear();

        public IEnumerator<Point> GetEnumerator() => Points.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}