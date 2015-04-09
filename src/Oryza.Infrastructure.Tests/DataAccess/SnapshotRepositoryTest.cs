using System.IO;
using Oryza.ServiceInterfaces;
using Oryza.TestBase;
using Oryza.TestBase.Composition;
using Xunit;

namespace Oryza.Infrastructure.Tests.DataAccess
{
    public class SnapshotRepositoryTest : IntegrationTest
    {
        [Fact]
        public void Store_ASnapshot_StoresWithoutException()
        {
            // arrange
            var snapshotRepository = _serviceProvider.GetService<ISnapshotRepository>();
            var priceTable = File.ReadAllText("oryza_price_table.txt");
            var snapshot = _serviceProvider.GetService<IPriceTableExtractor>().ExtractPriceTable(priceTable);

            // act
            snapshotRepository.Store(snapshot);

            // assert
        }
    }
}