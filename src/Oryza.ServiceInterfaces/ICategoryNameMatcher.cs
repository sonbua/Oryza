using System.Collections.Generic;
using Oryza.Entities;

namespace Oryza.ServiceInterfaces
{
    public interface ICategoryNameMatcher
    {
        /// <summary>
        /// Try to match this new captured category name <paramref name="categoryName"/> in the existing category types <paramref name="existingCategoryTypes"/> stored in database.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="existingCategoryTypes"></param>
        /// <param name="categoryNameConverter"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        bool TryMatchCategoryName(string categoryName, IEnumerable<CategoryType> existingCategoryTypes, ICategoryNameConverter categoryNameConverter, out CategoryType match);
    }
}