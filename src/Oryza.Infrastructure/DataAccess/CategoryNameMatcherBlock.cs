using System;
using System.Collections.Generic;
using LegoBuildingBlock;
using Oryza.Entities;
using Oryza.Utility;

namespace Oryza.Infrastructure.DataAccess
{
    public class CategoryNameMatcherBlock : IBlock<CategoriesMatching, Match<CategoryType>>
    {
        private readonly NameToTypeConverterBlock _nameToTypeConverterBlock;

        public CategoryNameMatcherBlock(NameToTypeConverterBlock nameToTypeConverterBlock)
        {
            _nameToTypeConverterBlock = nameToTypeConverterBlock;
        }

        public Func<CategoriesMatching, Match<CategoryType>> Handle
        {
            get
            {
                return categoriesMatching =>
                       {
                           var newCategoryTypeName = _nameToTypeConverterBlock.Handle(categoriesMatching.CategoryName);

                           foreach (var categoryType in categoriesMatching.ExistingCategoryTypes)
                           {
                               if (categoryType.NameVariants.Contains(categoriesMatching.CategoryName))
                               {
                                   return new Match<CategoryType> {IsMatched = true, Matched = categoryType};
                               }

                               if (categoryType.Name.EqualsIgnoreCase(newCategoryTypeName))
                               {
                                   return new Match<CategoryType> {IsMatched = true, Matched = categoryType};
                               }
                           }

                           return new Match<CategoryType> {IsMatched = false};
                       };
            }
        }
    }

    public class CategoriesMatching
    {
        public string CategoryName { get; set; }

        public IEnumerable<CategoryType> ExistingCategoryTypes { get; set; }
    }
}