using System;
using System.Collections.Generic;

namespace Oryza.Entities
{
    public class EntryType : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<string> NameVariants { get; set; }

        public string Description { get; set; }

        public Metadata Metadata { get; set; }
    }
}