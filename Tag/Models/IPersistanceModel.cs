using System.Collections.Generic;
using Tag.BusinessObjects;

namespace Tag.Models
{
    internal interface IPersistanceModel
    {
        void AddTag(string fileName, string tag);

        List<BusinessObjects.Tag> GetTags();

        void AddFiles(IEnumerable<string> fileNames);
        
        List<File> GetFiles();
    }
}