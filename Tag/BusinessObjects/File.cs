using System.Collections.Generic;

namespace Alkl.WinTag.BusinessObjects
{
    internal class File
    {
        public string Name { get; set; }

        public List<string> Tags { get; set; } = new List<string>();
    }
}