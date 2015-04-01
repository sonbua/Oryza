using System.Collections.Generic;
using Oryza.Entities;

namespace Oryza.ServiceInterfaces
{
    public interface ICategoriesExtractor
    {
        IEnumerable<Category> ExtractCategories(string priceTable);
    }
}