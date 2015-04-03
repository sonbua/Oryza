using System.Collections.Generic;

namespace Oryza.Entities
{
    public class CategoryType
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ICollection<string> NameVariants { get; set; }

        public Metadata Metadata { get; set; }

        public CategoryType()
        {
            Name = string.Empty;
            NameVariants = new List<string>();
        }
    }
}