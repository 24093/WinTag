using System.Collections.Generic;

namespace Tag.BusinessObjects
{
    internal class File
    {
        public string Name { get; set; } = string.Empty;

        public List<string> Tags { get; set; } = new List<string>();
    }
}