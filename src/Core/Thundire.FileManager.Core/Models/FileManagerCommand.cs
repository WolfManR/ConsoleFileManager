namespace Thundire.FileManager.Core.Models
{
    public abstract class FileManagerCommand
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string[] Abbreviations { get; }

        public abstract bool CanHandle(string[] args);

        public abstract void Handle(string[] args);
    }
}