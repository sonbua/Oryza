using System.Collections;
using System.Collections.Generic;
using Oryza.Entities;
using Oryza.Infrastructure.DataAccess;
using Oryza.TestBase;
using Oryza.TestBase.Composition;
using Xunit;

namespace Oryza.Infrastructure.Tests.DataAccess
{
    public class CategoryNameMatcherBlockTest : Test
    {
        [Theory]
        [ClassData(typeof (CategoryTypeList))]
        public void GivenACategoryNameAndAListOfCategoryTypes_ReturnsExpectedResult(CategoriesMatching categoriesMatching, bool expected)
        {
            // arrange
            var categoryNameMatcherBlock = _serviceProvider.GetService<CategoryNameMatcherBlock>();

            // act
            var match = categoryNameMatcherBlock.Handle(categoriesMatching);

            // assert
            Assert.Equal(expected, match.IsMatched);
        }

        private class CategoryTypeList : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                             {
                                 new CategoriesMatching
                                 {
                                     CategoryName = "Long grain white rice - high quality",
                                     ExistingCategoryTypes = new List<CategoryType>()
                                 },
                                 false
                             };

                yield return new object[]
                             {
                                 new CategoriesMatching
                                 {
                                     CategoryName = "Long   grain   white   rice   -   high   quality",
                                     ExistingCategoryTypes = new List<CategoryType> {new CategoryType {Name = "LongGrainWhiteRiceHighQuality", NameVariants = new List<string> {"Long grain white rice - high quality"}}}
                                 },
                                 true
                             };

                yield return new object[]
                             {
                                 new CategoriesMatching
                                 {
                                     CategoryName = "LONG   GRAIN   WHITE   RICE   -   HIGH   QUALITY",
                                     ExistingCategoryTypes = new List<CategoryType> {new CategoryType {Name = "LongGrainWhiteRiceHighQuality", NameVariants = new List<string> {"Long grain white rice - high quality"}}}
                                 },
                                 true
                             };

                yield return new object[]
                             {
                                 new CategoriesMatching
                                 {
                                     CategoryName = "XXX",
                                     ExistingCategoryTypes = new List<CategoryType> {new CategoryType {Name = "LongGrainWhiteRiceHighQuality", NameVariants = new List<string> {"Long grain white rice - high quality"}}}
                                 },
                                 false
                             };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}