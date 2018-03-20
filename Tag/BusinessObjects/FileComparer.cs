using System.Collections.Generic;

namespace Alkl.WinTag.BusinessObjects
{
    internal class FileComparer : IEqualityComparer<File>
    {
        public bool Equals(File x, File y)
        {
            return x.Name == y.Name;
        }

        public int GetHashCode(File obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}