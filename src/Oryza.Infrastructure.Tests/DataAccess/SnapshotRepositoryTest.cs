using System.IO;
using Oryza.ServiceInterfaces;
using Oryza.TestBase;
using Oryza.TestBase.Composition;
using Xunit;

namespace Oryza.Infrastructure.Tests.DataAccess
{
    public class SnapshotRepositoryTest : Test
    {
        [Fact]
        public void Store_ASnapshot_StoresWithoutException()
        {
            // arrange
            var snapshotRepository = _serviceProvider.GetService<ISnapshotRepository>();
            var priceTable = File.ReadAllText("oryza_price_table.txt");
            
            var snapshot = _serviceProvider.GetService<IPriceTableExtractor>().ExtractPriceTable(priceTable);
            snapshot.RawData = File.ReadAllText("oryza_web_page.txt");
            snapshot.PriceUnit = _serviceProvider.GetService<IPriceUnitExtractor>().ExtractPriceUnit(priceTable);
            snapshot.PublishDate = _serviceProvider.GetService<IDateExtractor>().ExtractDate(priceTable);

            TruncateDatabase();

            // act
            snapshotRepository.Store(snapshot);

            // assert
        }
    }
}