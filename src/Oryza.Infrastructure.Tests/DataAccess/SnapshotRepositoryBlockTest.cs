using System.IO;
using Oryza.Infrastructure.DataAccess;
using Oryza.Infrastructure.Extract;
using Oryza.TestBase;
using Oryza.TestBase.Composition;
using Xunit;

namespace Oryza.Infrastructure.Tests.DataAccess
{
    public class SnapshotRepositoryBlockTest : IntegrationTest
    {
        [Fact]
        public void ASnapshot_StoresWithoutException()
        {
            // arrange
            var snapshotRepository = _serviceProvider.GetService<SnapshotRepositoryBlock>();
            var priceTable = File.ReadAllText("oryza_price_table.txt");
            var snapshot = _serviceProvider.GetService<ExtractBlock>().Work(priceTable);

            // act
            snapshotRepository.Work(snapshot);

            // assert
        }
    }
}