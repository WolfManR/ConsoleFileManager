using System;
using System.Collections.Generic;
using ConsoleFileManager.Render;

namespace ConsoleFileManager.FilesManager.Services
{
    public class Messenger
    {
        private readonly ConsoleHandler _consoleHandler;

        public Messenger(ConsoleHandler consoleHandler) => _consoleHandler = consoleHandler;


        public bool Confirm(string message)
        {
            do
            {
                var response = _consoleHandler.Request(CutMessage(message));
                if (response == "y")
                    return true;
                if (response == "n")
                    return false;
            } while (true);
        }

        public void Report(string message)
        {
            _consoleHandler.Report(CutMessage(message));
        }

        public void Report(Exception exception)
        {
            Report(exception.Message);
        }

        public void PrintCommand(string command)
        {
            _consoleHandler.PrintCommand(command);
        }

        private string[] CutMessage(string message)
        {
            var (width, maxLines) = _consoleHandler.GetReportSize();
            var lines = SeparateLines(message, width, maxLines, new ());
            return lines.ToArray();
        }

        private Queue<string> SeparateLines(string line, int width, int maxLines, Queue<string> queue)
        {
            if (line is null) return queue;
            if (queue.Count == maxLines) return queue;
            if (line.Contains('\n'))
            {
                var index = line.IndexOf('\n');
                if (index + 1 <= width)
                {
                    queue.Enqueue(line[..(index - 1)]);
                    SeparateLines(line[(index + 1)..], width, maxLines, queue);
                    return queue;
                }
                
                queue.Enqueue(line[..width]);
                SeparateLines(line[width..], width, maxLines, queue);
                return queue;
            }

            if (line.Length < width)
            {
                queue.Enqueue(line);
                return queue;
            }

            queue.Enqueue(line[..width]);
            SeparateLines(line[width..], width, maxLines, queue);
            return queue;
        }
    }
}