using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Oryza.Entities;
using Oryza.ServiceInterfaces;
using Oryza.TestBase;
using Oryza.TestBase.Composition;
using Oryza.TestBase.Xunit.Extensions;
using Xunit;

namespace Oryza.Infrastructure.Tests.Extract
{
    public class PriceTableExtractorTest : Test
    {
        private readonly string _priceTable;

        public PriceTableExtractorTest()
        {
            _priceTable = File.ReadAllText("oryza_price_table.txt");
        }

        [Fact]
        public void ExtractDate_PriceTable_ReturnsCorrectDate()
        {
            // arrange
            var extractor = _serviceProvider.GetService<IDateExtractor>();

            // act
            var publishedDate = extractor.ExtractDate(_priceTable);

            // assert
            Assert.Equal(new DateTime(2015, 3, 30), publishedDate);
        }

        [Fact]
        public void ExtractPriceUnit_PriceTable_ReturnsCorrectPriceUnit()
        {
            // arrange
            var extractor = _serviceProvider.GetService<IPriceUnitExtractor>();

            // act
            var priceUnit = extractor.ExtractPriceUnit(_priceTable);

            // assert
            Assert.Equal("USD per ton", priceUnit);
        }

        [Fact]
        public void ExtractCategories_PriceTable_ReturnsCorrectCategoriesCount()
        {
            // arrange
            var extractor = _serviceProvider.GetService<ICategoriesExtractor>();

            // act
            var categories = extractor.ExtractCategories(_priceTable);

            // assert
            Assert.Equal(6, categories.Count());
        }

        [Fact]
        public void ExtractCategories_PriceTable_ReturnsCorrectCategoryName()
        {
            // arrange
            var extractor = _serviceProvider.GetService<ICategoriesExtractor>();

            // act
            var categories = extractor.ExtractCategories(_priceTable);

            // assert
            Assert.Equal("Long grain white rice - high quality", categories.First().Name);
        }

        [Fact]
        public void ExtractCategories_PriceTable_ReturnsCorrectEntryName()
        {
            // arrange
            var extractor = _serviceProvider.GetService<ICategoriesExtractor>();

            // act
            var categories = extractor.ExtractCategories(_priceTable);

            // assert
            Assert.Equal("Thailand 100% B grade", categories.First().Entries.First().Name);
        }

        [Fact]
        public void ExtractCategories_PriceTable_ReturnsCorrectEntryPrice()
        {
            // arrange
            var extractor = _serviceProvider.GetService<ICategoriesExtractor>();

            // act
            var categories = extractor.ExtractCategories(_priceTable);

            // assert
            Assert.Equal(395M, categories.First().Entries.First().LowPrice);
            Assert.Equal(405M, categories.First().Entries.First().HighPrice);
        }

        [Theory]
        [InlineData("Long grain white rice - high quality", "LongGrainWhiteRiceHighQuality")]
        public void ConvertCategoryName_CategoryName_ReturnsCorrectCategoryTypeName(string categoryName, string expectedEntryTypeName)
        {
            // arrange
            var categoryNameConverter = _serviceProvider.GetService<ICategoryNameConverter>();

            // act
            var categoryTypeName = categoryNameConverter.ConvertCategoryName(categoryName);

            // assert
            Assert.Equal(expectedEntryTypeName, categoryTypeName);
        }

        [Theory]
        [ClassData(typeof (CategoryTypeList))]
        public void TryMatchCategoryName_GivenACategoryNameAndAListOfCategoryTypes_ReturnsExpectedResult(string categoryName, IEnumerable<CategoryType> existingCategoryTypes, bool expected)
        {
            // arrange
            var categoryNameMatcher = _serviceProvider.GetService<ICategoryNameMatcher>();
            var categoryNameConverter = _serviceProvider.GetService<ICategoryNameConverter>();
            CategoryType match;

            // act
            var isMatch = categoryNameMatcher.TryMatchCategoryName(categoryName, existingCategoryTypes, categoryNameConverter, out match);

            // assert
            Assert.Equal(expected, isMatch);
        }

        [Theory]
        [InlineData("Thailand 100% B grade", "Thailand100percentBGrade")]
        [InlineData("U.S. 4% broken ", "US4percentBroken")]
        [InlineData("U.S. 4% half-broken ", "US4percentHalfBroken")]
        public void ConvertEntryName_EntryName_ReturnsCorrectEntryTypeName(string entryName, string expectedEntryTypeName)
        {
            // arrange
            var entryTypeNameConverter = _serviceProvider.GetService<IEntryNameConverter>();

            // act
            var entryTypeName = entryTypeNameConverter.ConvertEntryName(entryName);

            // assert
            Assert.Equal(expectedEntryTypeName, entryTypeName);
        }

        [Theory]
        [ClassData(typeof (EntryTypeList))]
        public void TryMatchEntryName_GivenAnEntryNameAndAListOfEntryTypes_ReturnsExpectedResult(string entryName, IEnumerable<EntryType> existingEntryTypes, bool expected)
        {
            // arrange
            var entryNameMatcher = _serviceProvider.GetService<IEntryNameMatcher>();
            var entryTypeNameConverter = _serviceProvider.GetService<IEntryNameConverter>();
            EntryType match;

            // act
            var isMatch = entryNameMatcher.TryMatchEntryName(entryName, existingEntryTypes, entryTypeNameConverter, out match);

            // assert
            Assert.Equal(expected, isMatch);
        }

        [Fact]
        public void ExtractPriceTable_GivenAPriceTable_ReturnsCorrectSnapshot()
        {
            // arrange
            var priceTableExtractor = _serviceProvider.GetService<IPriceTableExtractor>();

            // act
            var snapshot = priceTableExtractor.ExtractPriceTable(_priceTable);

            // assert
            Assert.Equal(_priceTable, snapshot.PriceTableData);
            Assert.Equal(new DateTime(2015, 3, 30), snapshot.PublishDate);
            Assert.Equal(6, snapshot.Categories.Count);
            Assert.Equal("USD per ton", snapshot.PriceUnit);
        }

        private class CategoryTypeList : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                             {
                                 "Long grain white rice - high quality",
                                 new List<CategoryType>(),
                                 false
                             };

                yield return new object[]
                             {
                                 "Long grain white rice - high quality",
                                 new List<CategoryType> {new CategoryType {NameVariants = new List<string> {"Long grain white rice - high quality"}}},
                                 true
                             };

                yield return new object[]
                             {
                                 "Long   grain   white   rice   -   high   quality",
                                 new List<CategoryType> {new CategoryType {NameVariants = new List<string> {"Long grain white rice - high quality"}}},
                                 false
                             };

                yield return new object[]
                             {
                                 "Long   grain   white   rice   -   high   quality",
                                 new List<CategoryType> {new CategoryType {Name = "LongGrainWhiteRiceHighQuality", NameVariants = new List<string> {"Long grain white rice - high quality"}}},
                                 true
                             };

                yield return new object[]
                             {
                                 "XXX",
                                 new List<CategoryType> {new CategoryType {Name = "LongGrainWhiteRiceHighQuality", NameVariants = new List<string> {"Long grain white rice - high quality"}}},
                                 false
                             };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private class EntryTypeList : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                             {
                                 "Thailand 100% B grade",
                                 new List<EntryType>(),
                                 false
                             };

                yield return new object[]
                             {
                                 "Thailand 100% B grade",
                                 new List<EntryType> {new EntryType {NameVariants = new List<string> {"Thailand 100% B grade"}}},
                                 true
                             };

                yield return new object[]
                             {
                                 "Thailand   100%   B   grade",
                                 new List<EntryType> {new EntryType {NameVariants = new List<string> {"Thailand 100% B grade"}}},
                                 false
                             };

                yield return new object[]
                             {
                                 "Thailand   100%   B   grade",
                                 new List<EntryType> {new EntryType {Name = "Thailand100percentBGrade", NameVariants = new List<string> {"Thailand 100% B grade"}}},
                                 true
                             };

                yield return new object[]
                             {
                                 "XXX",
                                 new List<EntryType> {new EntryType {Name = "Thailand100percentBGrade", NameVariants = new List<string> {"Thailand 100% B grade"}}},
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