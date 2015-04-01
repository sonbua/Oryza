using System.Collections.Generic;
using Oryza.Entities;

namespace Oryza.ServiceInterfaces
{
    public interface ICategoryExtractor
    {
        IEnumerable<Category> ExtractCategories(string priceTable);
    }
}