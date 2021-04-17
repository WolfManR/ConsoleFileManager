using System;
using System.Collections.Generic;
using System.IO;
using ConsoleFileManager.FilesManager.Extensions;
using ConsoleFileManager.FilesManager.Models;
using ConsoleFileManager.Render.Primitives;

namespace ConsoleFileManager.Render.Presenters
{
    public class DetailsInfoPresenter : Presenter<Info>
    {
        private List<string> lines = new();
        private int x;
        private int y;
        private int width;
        private int height;
        public override void Print(ContentPlace contentPlace, Info toPresent)
        {
            var archive = FileAttributes.Archive;
            var temporary = FileAttributes.Temporary;
            var compressed = FileAttributes.Compressed;
            var encrypted = FileAttributes.Encrypted;
            var system = FileAttributes.System;

            lines.Clear();
            (x, y, width, height) = contentPlace;
            if (toPresent.IsFile)
            {
                var file = toPresent.ToFile();
                lines.Add(Space("<File>"));
                lines.Add(Space());
                Separate(file.Name);

                if (CantAddMore()) goto EndPrint;

                Separate(toPresent.Path);

                if (CantAddMore()) goto EndPrint;


                if (file.IsReadOnly) lines.Add(Space("Read Only"));

                var fileAttributes = file.Attributes;


                lines.Add(Space($"archive:    {(archive == (fileAttributes & archive))}"));
                lines.Add(Space($"temporary:  {(temporary == (temporary & fileAttributes))}"));
                lines.Add(Space($"compressed: {(compressed == (compressed & fileAttributes))}"));
                lines.Add(Space($"encrypted:  {(encrypted == (encrypted & fileAttributes))}"));
                lines.Add(Space($"system:     {(system == (system & fileAttributes))}"));
                goto EndPrint;
            }

            var directory = toPresent.ToDirectory();
            lines.Add(Space("<Directory>"));
            lines.Add(Space());
            Separate(directory.Name);
            if (CantAddMore()) goto EndPrint;
            Separate(toPresent.Path);
            if (CantAddMore()) goto EndPrint;

            var dirAttributes = directory.Attributes;
            lines.Add(Space($"temporary:  {(temporary == (temporary & dirAttributes))}"));
            lines.Add(Space($"encrypted:  {(encrypted == (encrypted & dirAttributes))}"));
            lines.Add(Space($"system:     {(system == (system & dirAttributes))}"));

            EndPrint:
            if (lines.Count < height)
            {
                for (var i = 0; i < height - lines.Count; i++)
                {
                    lines.Add(Space());
                }
            }
            PrintLines();
        }

        private bool CantAddMore() => lines.Count >= height;

        private void PrintLines()
        {
            for (var i = y; i < lines.Count; i++)
            {
                Console.SetCursorPosition(x, i);
                Console.Write(lines[i]);
            }
        }

        private string Space(string line = null) => line is null ? new(' ', width) : line + new string(' ', width - line.Length);

        private void Separate(string toSeparate)
        {
            var lastStartPosition = 0;
            var lastEndPosition = width;
            while (true)
            {
                if (CantAddMore()) return;

                if (toSeparate.Length < width)
                {
                    lines.Add(Space(toSeparate));
                    break;
                }

                toSeparate = toSeparate[lastStartPosition..lastEndPosition];
                lines.Add(toSeparate);
                lastStartPosition = lastEndPosition;
                lastEndPosition += width;
            }
        }
    }
}