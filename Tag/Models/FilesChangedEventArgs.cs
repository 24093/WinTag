using System;
using System.Collections.Generic;
using Alkl.WinTag.BusinessObjects;

namespace Alkl.WinTag.Models
{
    internal class FilesChangedEventArgs : EventArgs
    {
        public readonly IEnumerable<File> Files;

        public FilesChangedEventArgs(IEnumerable<File> files)
        {
            Files = files;
        }
    }
}