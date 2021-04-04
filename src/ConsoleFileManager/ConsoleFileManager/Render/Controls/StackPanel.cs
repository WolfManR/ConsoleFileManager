using System.Collections.Generic;
using ConsoleFileManager.Render.Abstracts;

namespace ConsoleFileManager.Render.Controls
{
    public class StackPanel : ContentControl
    {
        private List<object> content = new();

        public ContentPresenter ContentPresenter { get; set; } = new();

        public StackPanel(params object[] content)
        {
            for (var i = 0; i < content.Length; i++)
            {
                this.content.Add(content[i]);
            }
        }

        public override void Print()
        {
            if(content.Count <= 0) return;
            var (startX,startY, width, height) = CalculateContentPlace();
            var bottomLine = height + startY;
            for (int i = startY, j = 0; j < content.Count && i < bottomLine; i++, j++)
            {
                var current = content[j].ToString();
                if(current is null) continue;
                
                ContentPresenter.Print(startX, i, width, current);
            }
        }
    }
}