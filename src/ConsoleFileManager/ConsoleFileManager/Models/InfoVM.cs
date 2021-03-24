using System.Collections.Generic;

namespace ConsoleFileManager.Models
{
    public class InfoVM : List<InfoVM>
    {
        public InfoVM Parent { get; set; }
        public Info Info { get; set; }
        public bool IsSelected { get; set; }
        public int Depth { get; set; }
        public bool IsOpen { get; set; }
    }
}