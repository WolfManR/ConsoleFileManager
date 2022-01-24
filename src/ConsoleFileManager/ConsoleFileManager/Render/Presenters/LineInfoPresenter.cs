using System;
using ConsoleFileManager.FilesManager.Models;
using ConsoleFileManager.Render.Primitives;

namespace ConsoleFileManager.Render.Presenters
{
    public class LineInfoPresenter : Presenter<Info>
    {
        public override void Print(ContentPlace contentPlace, Info toPresent)
        {
            var (x, y, width, _) = contentPlace;
            Console.SetCursorPosition(x, y);
            if (toPresent is null)
            {
                Console.Write(new string(' ', width));
                return;
            }
            var nameWidth = width - 14;
            var name = toPresent.Name.Length < nameWidth
                ? toPresent.Name + new string(' ', nameWidth - toPresent.Name.Length)
                : toPresent.Name[..nameWidth];
            Console.Write("{0} {1,-14}", name, toPresent.IsFile ? $"{toPresent.FileExtension}" : "dir");
        }
    }
}