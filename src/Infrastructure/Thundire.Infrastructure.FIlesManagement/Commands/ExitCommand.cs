using Thundire.FileManager.Core;
using Thundire.FileManager.Core.Models;

namespace Thundire.Infrastructure.FIlesManagement.Commands
{
    public class ExitCommand : FileManagerCommand
    {
        private readonly IFilesManagementSystem _system;

        public ExitCommand(IFilesManagementSystem system) => _system = system;

        public override string Name { get; } = "Exit";
        public override string Description { get; } = "Exit";
        public override string[] Abbreviations { get; } = { "q", "quit" };
        public override bool CanHandle(string[] args) => true;

        public override void Handle(string[] args) => _system.Exit();
    }
}