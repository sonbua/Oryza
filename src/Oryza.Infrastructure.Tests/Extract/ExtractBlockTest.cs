using System;
using System.IO;
using System.Linq;
using Oryza.Entities;
using Oryza.Infrastructure.Extract;
using Oryza.TestBase;
using Oryza.TestBase.Composition;
using Xunit;

namespace Oryza.Infrastructure.Tests.Extract
{
    public class ExtractBlockTest : Test
    {
        private readonly string _priceTable;
        private Snapshot _snapshot;

        public ExtractBlockTest()
        {
            _priceTable = File.ReadAllText("oryza_price_table.txt");
            _snapshot = new Snapshot {PriceTableData = _priceTable};
        }

        [Fact]
        public void PriceTable_ReturnsCorrectDate()
        {
            // arrange
            var dateExtractorBlock = _serviceProvider.GetService<DateExtractorBlock>();

            // act
            _snapshot = dateExtractorBlock.Handle(_snapshot);

            // assert
            Assert.Equal(new DateTime(2015, 3, 30), _snapshot.PublishDate);
        }

        [Fact]
        public void PriceTable_ReturnsCorrectPriceUnit()
        {
            // arrange
            var priceUnitExtractorBlock = _serviceProvider.GetService<PriceUnitExtractorBlock>();

            // act
            _snapshot = priceUnitExtractorBlock.Handle(_snapshot);

            // assert
            Assert.Equal("USD per ton", _snapshot.PriceUnit);
        }

        [Fact]
        public void PriceTable_ReturnsCorrectCategoriesCount()
        {
            // arrange
            var categoriesExtractorBlock = _serviceProvider.GetService<CategoriesExtractorBlock>();

            // act
            _snapshot = categoriesExtractorBlock.Handle(_snapshot);

            // assert
            Assert.Equal(6, _snapshot.Categories.Count());
        }

        [Fact]
        public void PriceTable_ReturnsCorrectCategoryName()
        {
            // arrange
            var categoriesExtractorBlock = _serviceProvider.GetService<CategoriesExtractorBlock>();

            // act
            _snapshot = categoriesExtractorBlock.Handle(_snapshot);

            // assert
            Assert.Equal("Long grain white rice - high quality", _snapshot.Categories.First().Name);
        }

        [Fact]
        public void PriceTable_ReturnsCorrectEntryName()
        {
            // arrange
            var categoriesExtractorBlock = _serviceProvider.GetService<CategoriesExtractorBlock>();

            // act
            _snapshot = categoriesExtractorBlock.Handle(_snapshot);

            // assert
            Assert.Equal("Thailand 100% B grade", _snapshot.Categories.First().Entries.First().Name);
        }

        [Fact]
        public void PriceTable_ReturnsCorrectEntryPrice()
        {
            // arrange
            var categoriesExtractorBlock = _serviceProvider.GetService<CategoriesExtractorBlock>();

            // act
            _snapshot = categoriesExtractorBlock.Handle(_snapshot);

            // assert
            Assert.Equal(395M, _snapshot.Categories.First().Entries.First().LowPrice);
            Assert.Equal(405M, _snapshot.Categories.First().Entries.First().HighPrice);
        }

        [Fact]
        public void PriceTable_ReturnsCorrectSnapshot()
        {
            // arrange
            var extractBlock = _serviceProvider.GetService<ExtractBlock>();

            // act
            var snapshot = extractBlock.Handle(_priceTable);

            // assert
            Assert.Equal(_priceTable, snapshot.PriceTableData);
            Assert.Equal(new DateTime(2015, 3, 30), snapshot.PublishDate);
            Assert.Equal(6, snapshot.Categories.Count);
            Assert.Equal("USD per ton", snapshot.PriceUnit);
        }
    }
}