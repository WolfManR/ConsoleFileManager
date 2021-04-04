using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConsoleFileManager.Infrastructure.Exceptions;
using ConsoleFileManager.Render;

namespace ConsoleFileManager.Infrastructure.Commands.FileSystemCLI
{
    public class ListPathCommand : Command
    {
        #region Implementation of ICommand

        /// <inheritdoc />
        public override string Name { get; } = "list";

        /// <inheritdoc />
        public override string[] Abbreviations { get; } = {"ls"};
        

        /// <inheritdoc />
        public override bool CanHandle(string[] args) => true;

        public override void Handle(string[] args)
        {
            var toPrint = FilesManager.GetDirectoryStruct().Select(info => info.ToString());
            ViewHandler.ContinuousPrint(toPrint);
        }

        #endregion
    }
}