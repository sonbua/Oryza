using System;
using System.Collections.Generic;

namespace Oryza.Entities
{
    public class Category : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Entry> Entries { get; set; }

        public Metadata Metadata { get; set; }
    }
}