using Thundire.FileManager.Core;
using Thundire.FileManager.Core.Exceptions;
using Thundire.FileManager.Core.Models;

namespace Thundire.Infrastructure.FIlesManagement.Commands
{
    public class CopyPathCommand : FileManagerCommand
    {
        private readonly IFilesManager _fileManager;


        public CopyPathCommand(IFilesManager fileManager) => _fileManager = fileManager;


        public override string Name { get; } = "Copy path";
        public override string Description { get; } = "Copy path";
        public override string[] Abbreviations { get; } = { "cp", "copy" };


        public override bool CanHandle(string[] args)
        {
            if (args.Length != 2)
                throw ExceptionsFactory.IncorrectArgument("Path to copy and path to where copy", nameof(args));
            var (toCopy, toWhere) = (args[0], args[1]);
            return (_fileManager.StringPathIsDirectory(toCopy), _fileManager.StringPathIsDirectory(toWhere)) switch
                   {
                       (false, false) or (_, true) => true,
                       _                           => false
                   };
        }

        public override void Handle(string[] args)
        {
            var (toCopy, toWhere) = (args[0], args[1]);
            switch (_fileManager.StringPathIsDirectory(toCopy), _fileManager.StringPathIsDirectory(toWhere))
            {
                case (false, false):
                    _fileManager.CopyFile(toCopy, toWhere);
                    break;
                case (true, true):
                    _fileManager.CopyDirectory(toCopy, toWhere);
                    break;
                case (false, true):
                    var fileName = Path.GetFileName(toCopy);
                    _fileManager.CopyFile(toCopy, Path.Combine(toWhere, fileName));
                    break;
            }
        }
    }
}