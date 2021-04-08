namespace ConsoleFileManager.FileManager.Models
{
    public class Info
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsFile { get; set; }
        public string FileExtension { get; set; }


        public override string ToString() => $"{Name}  {Path}  {IsFile}  {FileExtension}";
    }
}