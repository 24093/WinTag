using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using Tag.BusinessObjects;

namespace Tag.Models
{
    internal class JsonFileModel : IPersistanceModel
    {
        private List<File> _files;

        private readonly object _fileLock = new object();

        private readonly string _jsonFileName;

        public JsonFileModel()
        {
            _jsonFileName = ".tags";

            ReadFile();
        }

        public void AddTag(string fileName, string tag)
        {
            ReadFile();

            var file = _files.SingleOrDefault(f => f.Name == fileName);
            if (!file.Tags.Contains(tag)) file.Tags.Add(tag);
        }

        public List<BusinessObjects.Tag> GetTags()
        {
            ReadFile();

            var tags = _files.SelectMany(f => f.Tags).ToList();
            return tags.Distinct().Select(d => new BusinessObjects.Tag
            {
                Name = d,
                Count = tags.Count(t => t == d)
            }).ToList();
        }

        public void AddFiles(IEnumerable<string> fileNames)
        {
            ReadFile();

            foreach (var fileName in fileNames)
            {


                if (_files.All(f => f.Name != fileName))
                {
                    _files.Add(new File {Name = fileName});
                }
            }

            WriteFile();
        }

        public List<File> GetFiles()
        {
            ReadFile();
            return _files;
        } 

        private void ReadFile()
        {
            lock (_fileLock)
            {
                if (!System.IO.File.Exists(_jsonFileName))
                {
                    WriteFile();
                }

                var content = System.IO.File.ReadAllText(_jsonFileName);
                _files = JsonConvert.DeserializeObject<List<File>>(content) ?? new List<File>();
            }
        }

        private void WriteFile()
        {
            lock (_fileLock)
            {
                var content = JsonConvert.SerializeObject(_files);
                System.IO.File.WriteAllText(_jsonFileName, content);
            }
        }
    }
}
