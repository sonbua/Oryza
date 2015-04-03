using System.Collections.Generic;

namespace Oryza.Entities
{
    public class Category : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public CategoryType Type { get; set; }

        public ICollection<Entry> Entries { get; set; }

        public Metadata Metadata { get; set; }
    }
}