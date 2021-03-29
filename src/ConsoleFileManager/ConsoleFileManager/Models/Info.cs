namespace ConsoleFileManager.Models
{
    public class Info
    {
        public string Name { get; set; }
        public string Path { get; init; }
        public bool IsFile { get; init; }
        public string FileExtension { get; set; }

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString() => $"{Name}  {Path}  {IsFile}  {FileExtension}";

        #endregion
    }
}