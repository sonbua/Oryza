using System.Collections.Generic;

namespace Oryza.Entities
{
    public class CategoryType : IEntity
    {
        public CategoryType()
        {
            Name = string.Empty;
            NameVariants = new List<string>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public ICollection<string> NameVariants { get; set; }
    }
}