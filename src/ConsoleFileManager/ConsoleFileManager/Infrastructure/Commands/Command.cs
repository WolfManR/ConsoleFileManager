namespace ConsoleFileManager.Infrastructure.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public abstract string[] Abbreviations { get; }

        public abstract bool CanHandle(string[] args);

        public abstract void Handle(string[] args);
    }
}