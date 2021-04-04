using ConsoleFileManager.Infrastructure;
using ConsoleFileManager.Infrastructure.Extensions;

namespace ConsoleFileManager
{
    class Program
    {
        static void Main()
        {
            var commandsParser = new InputCommandsParser().CommandsRegistration();
            var inputHandler = new InputHandler(new Configuration(), commandsParser);
            inputHandler.OnClose += Close;
            inputHandler.Start();
        }

        public static void Close()
        {
            
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
