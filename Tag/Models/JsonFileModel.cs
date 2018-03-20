using Alkl.WinTag.BusinessObjects;
using Newtonsoft.Json;
using System.Collections.Generic;
using File = Alkl.WinTag.BusinessObjects.File;

namespace Alkl.WinTag.Models
{
    internal class JsonFileModel : BasicPersistance
    {
        private const string DataFileName = "tags.wintag";

        protected override IEnumerable<File> ReadData(string path)
        {
            var filename = System.IO.Path.Combine(path, DataFileName);

            if (!System.IO.File.Exists(filename))
                System.IO.File.WriteAllText(filename, string.Empty);

            var content = System.IO.File.ReadAllText(filename);
            var folder = JsonConvert.DeserializeObject<Folder>(content) ?? new Folder();
            
            return folder.Files;
        }

        protected override void WriteData(string path, IEnumerable<File> data)
        {
            var filename = System.IO.Path.Combine(path, DataFileName);
            var folder = new Folder {Files = new List<File>(data)};
            var content = JsonConvert.SerializeObject(folder);
            System.IO.File.WriteAllText(filename, content);
        }

    }
}
