using System;

namespace ConsoleFileManager.FilesManager.Services
{
    public class Messenger
    {
        public bool Confirm(string message)
        {

            return true;
        }

        public void Report(string message)
        {
        }

        public void Report(Exception exception)
        {
        }

        public void PrintCommand(string command)
        {
        }
    }
}