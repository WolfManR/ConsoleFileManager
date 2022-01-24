using ConsoleFileManager.FilesManager;

namespace ConsoleFileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            new FilesManagerSystem()
                .Configure(config =>
                {
                    config.OnClose = Close;
                })
                .Start();
        }

        private static void Close()
        {

        }
    }
}
