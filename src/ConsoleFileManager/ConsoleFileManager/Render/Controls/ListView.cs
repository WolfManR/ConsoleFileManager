using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleFileManager.Render.Abstracts;
using ConsoleFileManager.Render.Primitives;

namespace ConsoleFileManager.Render.Controls
{
    public class ListView : ContentControl
    {
        private List<object> Output { get; }

        public object Selected { get; set; }
        private int CurrentLine { get; set; }
        private int CurrentSelectedLine { get; set; }

        public ContentPresenter Presenter { get; set; } = new();

        public override Size Size
        {
            get => _size;
            set
            {
                _size = value;
                ConfigurePage(value.Height - 1);
            }
        }

        private int PageFirstLine { get; set; }
        private int PageSize { get; set; }
        private int TopContentLine => Y;
        private int BottomContentLine => Y + Size.Height - 2;

        private int ContentWidth => Size.Width;

        private object[] currentPage;
        private Size _size;

        public ListView(int pageSize, List<object> content)
        {
            Output = content;
            ConfigurePage(pageSize);
        }

        private void ConfigurePage(int pageSize)
        {
            PageSize = pageSize;
            if (Output.Count > 0)
            {
                Selected = Output[0];
                currentPage = Output.Take(PageSize).ToArray();
                return;
            }

            currentPage = new object[PageSize];
        }

        public void Prev()
        {
            if (Output.Count <= 0) return;

            if (CurrentLine == 0) return;

            Deselect();
            if (CurrentLine == PageFirstLine)
            {
                PageFirstLine = CurrentLine - PageSize;
                currentPage = Output.Skip(PageFirstLine).Take(PageSize).ToArray();
                Print();
                CurrentSelectedLine = BottomContentLine;
            }
            else
                CurrentSelectedLine--;
            CurrentLine--;
            Selected = Output[CurrentLine];
            Select();
        }

        public void Next()
        {
            if (Output.Count <= 0) return;

            if (CurrentLine == Output.Count - 1) return;

            Deselect();
            if (CurrentLine == PageFirstLine + PageSize-1)
            {
                PageFirstLine = CurrentLine + 1;
                currentPage = Output.Skip(PageFirstLine).Take(PageSize).ToArray();
                Print();
                CurrentSelectedLine = TopContentLine;
            }
            else
                CurrentSelectedLine++;
            CurrentLine++;
            Selected = Output[CurrentLine];
            Select();
        }

        private void Select()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Presenter.Print(X, CurrentSelectedLine, ContentWidth, Selected);
            SetDefaultConsoleColor();
        }

        private void Deselect()
        {
            SetDefaultConsoleColor();
            Presenter.Print(X, CurrentSelectedLine, ContentWidth, Selected);
        }

        private void SetDefaultConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public override void Print()
        {
            var page = currentPage;
            var (x, y, width, height) = (X, TopContentLine, ContentWidth, BottomContentLine);
            for (int i = y, j = 0; i <= height; i++, j++)
            {
                string current = null;
                if(j < page.Length)
                    current = page[j].ToString();

                Presenter.Print(x, i, width, current);
            }

            if (CurrentLine == 0)
            {
                CurrentSelectedLine = TopContentLine;
                Select();
            }
        }
    }
}