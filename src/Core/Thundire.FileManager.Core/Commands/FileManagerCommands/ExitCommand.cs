using Thundire.FileManager.Core.Models;

namespace Thundire.FileManager.Core.Commands.FileManagerCommands
{
    public class ExitCommand : FileManagerCommand
    {
        private readonly FilesManagerSystem _system;

        public ExitCommand(FilesManagerSystem system) => _system = system;

        public override string Name { get; } = "Exit";
        public override string Description { get; } = "Exit";
        public override string[] Abbreviations { get; } = { "q", "quit" };
        public override bool CanHandle(string[] args) => true;

        public override void Handle(string[] args) => _system.Exit();
    }
}