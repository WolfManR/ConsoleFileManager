using System;
using System.IO;
using ConsoleFileManager.Infrastructure.Exceptions;
using ConsoleFileManager.Infrastructure.Extensions;

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
            if (args.Length < 2)
                throw ExceptionsFactory.IncorrectArgument("Path to copy and path to where copy", nameof(args));
            var (toCopy,toWhere) = (args[0],args[1]);
            return (toCopy.StringPathIsDirectory(), toWhere.StringPathIsDirectory()) switch
                   {
                       (false,false) or (_, true) => true,
                       _ => false
                   };
        }

        /// <inheritdoc />
        public override void Handle(string[] args)
        {
            var (toCopy, toWhere) = (args[0], args[1]);
            switch (toCopy.StringPathIsDirectory(), toWhere.StringPathIsDirectory())
            {
                case (false, false):
                    CopyFile(toCopy,toWhere);
                    break;
                case (true, true):
                    CopyDirectory(toCopy,toWhere);
                    break;
                case (false, true):
                    var fileName = Path.GetFileName(toCopy);
                    CopyFile(toCopy,Path.Combine(toWhere,fileName));
                    break;
            }
        }

        #endregion


        public void CopyFile(string from, string to, bool rewrite = false)
        {
            if (!File.Exists(from))
                throw ExceptionsFactory.PathNotExist(from);
            if (!rewrite && File.Exists(to))
                throw ExceptionsFactory.SamePathAlreadyExist(to, nameof(to));
            File.Copy(from, to, true);
        }

        public void CopyDirectory(string from, string to)
        {
            if (!Directory.Exists(from))
                throw ExceptionsFactory.PathNotExist(from);
            if (!Directory.Exists(to))
                Directory.CreateDirectory(to);
            Directory.Move(from, to);
        }
    }
}