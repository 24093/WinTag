using System;
using Alkl.WinTag.BusinessObjects;

namespace Alkl.WinTag.Models
{
    internal class FileChangedEventArgs : EventArgs
    {
        public readonly File File;

        public FileChangedEventArgs(File file)
        {
            File = file;
        }
    }
}