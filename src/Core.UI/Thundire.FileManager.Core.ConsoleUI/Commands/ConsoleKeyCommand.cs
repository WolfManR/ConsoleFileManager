namespace Thundire.FileManager.Core.ConsoleUI.Commands
{
    public abstract class ConsoleKeyCommand
    {
        public abstract bool CanHandle(ConsoleKeyInfo keyInfo);
        public abstract void Handle(ConsoleKeyInfo keyInfo);
    }
}