using System.IO;
using ConsoleFileManager.Render;

namespace ConsoleFileManager.Infrastructure.Commands.FileSystemCLI
{
    public class ChangeDirectoryCommand : Command
    {
        private const string Back = "..";
        private const string ToRoot = "./";

        #region Overrides of Command

        /// <inheritdoc />
        public override string Name { get; } = "Change directory";

        /// <inheritdoc />
        public override string[] Abbreviations { get; } = {"cd"};

        /// <inheritdoc />
        public override bool CanHandle(string[] args) => args.Length == 1;

        /// <inheritdoc />
        public override void Handle(string[] args)
        {
            var move = args[0];
            switch (move)
            {
                case Back:
                    FilesManager.ChangeDirectory(Move.Back);
                    break;
                case ToRoot:
                    FilesManager.ChangeDirectory(Move.ToRoot);
                    break;
                case var path when Path.IsPathRooted(path):
                    FilesManager.ChangeDirectory(path);
                    break;
                case var path when !Path.IsPathRooted(path):
                    FilesManager.ChangeDirectory(Move.Inner, path);
                    break;
            }

            ViewHandler.WriteLine(FilesManager.CurrentDirectoryPath);
        }

        #endregion
    }
}