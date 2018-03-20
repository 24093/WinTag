using System;
using Alkl.WinTag.BusinessObjects;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using File = Alkl.WinTag.BusinessObjects.File;

namespace Alkl.WinTag.Models
{
    internal abstract class BasicPersistance : IPersistanceModel
    {
        private IEnumerable<File> _files;

        private string _path;

        public event EventHandler<FilesChangedEventArgs> FilesChanged;

        public event EventHandler<FileChangedEventArgs> FileChanged;
        
        public void AddTag(string fileName, string tag)
        {
            var file = GetFile(fileName);

            if (!file.Tags.Contains(tag))
            {
                file.Tags.Add(tag);
            }

            SaveData(_path, _files);
            FileChanged?.Invoke(this, new FileChangedEventArgs(file));
        }

        public void RemoveTag(string fileName, string tag)
        {
            var file = GetFile(fileName);

            if (file.Tags.Contains(tag))
            {
                file.Tags.Remove(tag);
            }

            SaveData(_path, _files);
            FileChanged?.Invoke(this, new FileChangedEventArgs(file));
        }

        public void Load(string path)
        {
            _path = path;
            _files = LoadData(_path);

            FilesChanged?.Invoke(this, new FilesChangedEventArgs(_files));

            foreach (var file in _files)
                FileChanged?.Invoke(this, new FileChangedEventArgs(file));
        }

        private IEnumerable<File> LoadData(string path)
        {
            return ReadData(path).Union(ScanFolder(path), new FileComparer());
        }

        protected abstract IEnumerable<File> ReadData(string path);

        private void SaveData(string path, IEnumerable<File> data)
        {
            WriteData(path, data);
        }

        protected abstract void WriteData(string path, IEnumerable<File> data);

        private File GetFile(string fileName)
        {
            return _files.SingleOrDefault(f => f.Name == fileName);
        }

        private IEnumerable<Tag> ExtractTags()
        {
            var tags = new List<Tag>();

            foreach (var file in _files)
            {
                foreach (var tag in file.Tags)
                {
                    var existing = tags.SingleOrDefault(t => t.Text == tag);

                    if (existing == null)
                    {
                        existing = new Tag { Text = tag, Count = 0};
                        tags.Add(existing);
                    }

                    existing.Count++;
                }
            }

            return tags;
        }

        private static IEnumerable<File> ScanFolder(string path)
        {
            var dir = new DirectoryInfo(path);

            foreach (var file in dir.GetFiles())
            {
                yield return new File { Name = file.Name };
            }
        }
    }
}
