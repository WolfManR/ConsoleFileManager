using System.IO;
using ConsoleFileManager.Infrastructure.Exceptions;
using ConsoleFileManager.Render;

namespace ConsoleFileManager.Infrastructure.Commands.FileSystemCLI
{
    public class CopyPathCommand : Command
    {
        #region Overrides of Command

        /// <inheritdoc />
        public override string Name { get; } = "Copy path";

        /// <inheritdoc />
        public override string[] Abbreviations { get; } = {"cp", "copy"};

        /// <inheritdoc />
        public override bool CanHandle(string[] args)
        {
            if (args.Length != 2)
                throw ExceptionsFactory.IncorrectArgument("Path to copy and path to where copy", nameof(args));
            var (toCopy,toWhere) = (args[0],args[1]);
            return (FilesManager.StringPathIsDirectory(toCopy), FilesManager.StringPathIsDirectory(toWhere)) switch
                   {
                       (false,false) or (_, true) => true,
                       _ => false
                   };
        }

        /// <inheritdoc />
        public override void Handle(string[] args)
        {
            var (toCopy, toWhere) = (args[0], args[1]);
            switch (FilesManager.StringPathIsDirectory(toCopy), FilesManager.StringPathIsDirectory(toWhere))
            {
                case (false, false):
                    FilesManager.CopyFile(toCopy,toWhere);
                    break;
                case (true, true):
                    FilesManager.CopyDirectory(toCopy,toWhere);
                    break;
                case (false, true):
                    var fileName = Path.GetFileName(toCopy);
                    FilesManager.CopyFile(toCopy,Path.Combine(toWhere,fileName));
                    break;
            }
        }

        #endregion
    }
}