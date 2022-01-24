using Thundire.FileManager.Core.ConsoleUI.Presenters;
using Thundire.FileManager.Core.ConsoleUI.Primitives;

namespace Thundire.FileManager.Core.ConsoleUI.Controls
{
    public class ListView
    {
        private List<Info> Output { get; set; }

        public Info Selected { get; set; }
        private int CurrentLine { get; set; }
        private int CurrentSelectedLine { get; set; }


        private ContentPlace _contentPlace;
        public ContentPlace ContentPlace
        {
            get => _contentPlace;
            set
            {
                _contentPlace = value;
                _x = value.X;
                _topContentLine = value.Y;
                _bottomContentLine = value.Y + value.Size.Height;
                _pageSize = value.Size.Height;
                _contentWidth = value.Size.Width;
            }
        }

        private int _x;
        private int _pageFirstLine;
        private int _pageSize;
        private int _topContentLine;
        private int _bottomContentLine;
        private int _contentWidth;
        

        private Info[] _currentPage;
        private LineInfoPresenter presenter = new();

        public ListView(ContentPlace contentPlace) => ContentPlace = contentPlace;

        private void ConfigurePage()
        {
            if (Output is {Count:>0})
            {
                Selected = Output[0];
                _currentPage = Output.Take(_pageSize).ToArray();
                return;
            }

            _currentPage = new Info[_pageSize];
        }

        public void Prev()
        {
            if (Output.Count <= 0) return;

            if (CurrentLine == 0) return;

            Deselect();
            if (CurrentLine == _pageFirstLine)
            {
                _pageFirstLine = CurrentLine - _pageSize;
                _currentPage = Output.Skip(_pageFirstLine).Take(_pageSize).ToArray();
                Print();
                CurrentSelectedLine = _bottomContentLine;
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
            if (CurrentLine == _pageFirstLine + _pageSize-1)
            {
                _pageFirstLine = CurrentLine + 1;
                _currentPage = Output.Skip(_pageFirstLine).Take(_pageSize).ToArray();
                Print();
                CurrentSelectedLine = _topContentLine;
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
            presenter.Print(new(_x, CurrentSelectedLine, new(_contentWidth,1)), Selected);
            SetDefaultConsoleColor();
        }

        private void Deselect()
        {
            SetDefaultConsoleColor();
            presenter.Print(new(_x, CurrentSelectedLine, new(_contentWidth, 1)), Selected);
        }

        private void SetDefaultConsoleColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void Print()
        {
            if(Output is null) return;
            var page = _currentPage;
            var (x, y, width, height) = _contentPlace;
            for (int i = y, j = 0; i <= height; i++, j++)
            {
                Info current = null;
                if(j < page.Length)
                    current = page[j];

                presenter.Print(new(x, i, new (width,1)), current);
            }

            if (CurrentLine == 0)
            {
                CurrentSelectedLine = _topContentLine;
                Select();
            }
        }


        public void ChangeOutput(IEnumerable<Info> output)
        {
            Output = output.ToList();
            ConfigurePage();
            Print();
            Select();
        }

        public void SetSelected(Info currentSelected)
        {
        }
    }
}