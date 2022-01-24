namespace Thundire.FileManager.Core.Models
{
    public abstract class ConsoleKeyCommand
    {
        public abstract bool CanHandle(ConsoleKeyInfo keyInfo);
        public abstract void Handle(ConsoleKeyInfo keyInfo);
    }
}