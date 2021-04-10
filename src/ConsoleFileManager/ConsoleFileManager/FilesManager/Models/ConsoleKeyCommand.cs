using System;

namespace ConsoleFileManager.FilesManager.Models
{
    public abstract class ConsoleKeyCommand
    {
        public abstract bool CanHandle(ConsoleKeyInfo keyInfo);
        public abstract void Handle();
    }
}