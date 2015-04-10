using System.Collections.Generic;

namespace Oryza.Entities
{
    public class Category : IEntity
    {
        public Category()
        {
            Name = string.Empty;
            Type = new CategoryType();
            Entries = new List<Entry>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public CategoryType Type { get; set; }

        public ICollection<Entry> Entries { get; set; }
    }
}