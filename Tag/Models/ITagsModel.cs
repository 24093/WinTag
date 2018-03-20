using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alkl.WinTag.BusinessObjects;

namespace Alkl.WinTag.Models
{
    internal interface ITagsModel
    {
        IEnumerable<Tag> Tags { get; }

        void AddTag(string tagName);
    }

    internal class TagManager : ITagsModel
    {
        public IEnumerable<Tag> Tags => new[]
        {
            new Tag {Count = 34, Text = "dog1"},
            new Tag {Count = 90, Text = "dog2"},
            new Tag {Count = 51, Text = "dog3"}
        };

        public void AddTag(string tagName)
        {
            throw new NotImplementedException();
        }
    }
}
