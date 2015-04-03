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

namespace Oryza.Extract.Tests
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
            Assert.Equal("Long grain white rice - high quality", categories.ElementAt(0).Name);
        }

        [Fact]
        public void ExtractCategories_PriceTable_ReturnsCorrectEntryName()
        {
            // arrange
            var extractor = _serviceProvider.GetService<ICategoriesExtractor>();

            // act
            var categories = extractor.ExtractCategories(_priceTable);

            // assert
            Assert.Equal("Thailand 100% B grade", categories.ElementAt(0).Entries.First().Name);
        }

        [Fact]
        public void ExtractCategories_PriceTable_ReturnsCorrectEntryPrice()
        {
            // arrange
            var extractor = _serviceProvider.GetService<ICategoriesExtractor>();

            // act
            var categories = extractor.ExtractCategories(_priceTable);

            // assert
            Assert.Equal(395M, categories.ElementAt(0).Entries.First().LowPrice);
            Assert.Equal(405M, categories.ElementAt(0).Entries.First().HighPrice);
        }

        [Theory]
        [InlineData("Thailand 100% B grade", "Thailand100percentBGrade")]
        [InlineData("U.S. 4% broken ", "US4percentBroken")]
        [InlineData("U.S. 4% half-broken ", "US4percentHalfBroken")]
        public void ConvertEntryName_EntryName_ReturnsCorrectEntryTypeName(string entryName, string expectedEntryTypeName)
        {
            // arrange
            var entryTypeNameConverter = _serviceProvider.GetService<IEntryTypeNameConverter>();

            // act
            var entryTypeName = entryTypeNameConverter.ConvertEntryName(entryName);

            // assert
            Assert.Equal(expectedEntryTypeName, entryTypeName);
        }

        [Theory]
        [ClassData(typeof (EntryTypeList))]
        public void MatchEntryName_GivenAnEntryNameAndAListOfEntryTypes_ReturnsExpectedResult(string entryName, ICollection<EntryType> existingEntryTypes, bool expected)
        {
            // arrange
            var entryNameMatcher = _serviceProvider.GetService<IEntryNameMatcher>();
            var entryTypeNameConverter = _serviceProvider.GetService<IEntryTypeNameConverter>();
            EntryType match;

            // act
            var isMatch = entryNameMatcher.MatchEntryName(entryName, existingEntryTypes, entryTypeNameConverter, out match);

            // assert
            Assert.Equal(expected, isMatch);
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