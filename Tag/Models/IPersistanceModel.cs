using System;

namespace Alkl.WinTag.Models
{
    internal interface IPersistanceModel
    {
        event EventHandler<FilesChangedEventArgs> FilesChanged;

        event EventHandler<FileChangedEventArgs> FileChanged;
        
        void AddTag(string fileName, string tag);

        void RemoveTag(string fileName, string tag);

        void Load(string path);
    }
}