namespace ConsoleFileManager.Infrastructure.Commands
{
    public class ExitCommand : Command
    {
        #region Overrides of Command

        /// <inheritdoc />
        public override string Name { get; } = "Exit";

        /// <inheritdoc />
        public override string[] Abbreviations { get; } = {"q", "quit"};

        /// <inheritdoc />
        public override bool CanHandle(string[] args) => true;

        /// <inheritdoc />
        public override void Handle(string[] args)
        {
            Program.Close();
        }

        #endregion
    }
}