using ConsoleFileManager.Infrastructure;
using ConsoleFileManager.Infrastructure.Extensions;

namespace ConsoleFileManager
{
    class Program
    {
        private static bool _isExit;

        private static readonly InputHandler handler = new(
            new InputCommandsParser()
            .CommandsRegistration());



        static void Main()
        {
            new Printer(new Configuration()).HandleInput();

            //while (!_isExit)
            //{
            //    Console.WriteLine("Input command");
            //    var userInput = Console.ReadLine();
            //    if(string.IsNullOrWhiteSpace(userInput)) continue;

            //    handler.Handle(userInput);
            //}
        }



        public static void Close()
        {
            _isExit = true;
        }
    }


    public class Configuration
    {
        public string OpenedPath { get; set; } = "D:/demo";
        public int WindowWidth { get; set; } = 160;
        public int WindowHeight { get; set; } = 40;
    }

    public static class Alphabet
    {
        public const char BorderVertical = '║';
        public const char BorderVerticalLeft = '╣';
        public const char BorderVerticalRight = '╠';

        public const char BorderCenter = '╬';

        public const char BorderHorizontal = '═';
        public const char BorderHorizontalUp = '╩';
        public const char BorderHorizontalDown = '╦';


        public const char BorderCornerUpLeft = '╔';
        public const char BorderCornerUpRight = '╗';
        public const char BorderCornerDownRight = '╝';
        public const char BorderCornerDownLeft = '╚';
    }
}
