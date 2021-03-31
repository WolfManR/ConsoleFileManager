using System.Collections;
using System.Collections.Generic;
using ConsoleFileManager.Render.Primitives;

namespace ConsoleFileManager.Render.Abstracts
{
    public class Figure : IEnumerable<Point>
    {
        protected List<Point> points = new();

        public virtual void Draw()
        {
            for (var i = 0; i < points.Count; i++)
            {
                points[i].Draw();
            }
        }

        public IEnumerator<Point> GetEnumerator() => points.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}