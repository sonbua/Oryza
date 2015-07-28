using System;
using System.IO;
using LegoBuildingBlock;
using Oryza.Infrastructure.DataAccess;
using Oryza.Infrastructure.Extract;
using Oryza.Infrastructure.Parsing;
using Oryza.TestBase;
using Oryza.TestBase.Composition;
using Xunit;

namespace Oryza.Infrastructure.Tests
{
    public class OryzaCrawlerBlockTest : IntegrationTest
    {
        [Fact]
        public void AnHtmlRawData_StoreWithoutException()
        {
            // arrange
            var oryzaCrawlerWithoutWebCaptureBlock = _serviceProvider.GetService<OryzaCrawlerWithoutWebCaptureBlock>();
            var priceTable = File.ReadAllText("oryza_web_page.txt");

            // act
            oryzaCrawlerWithoutWebCaptureBlock.Handle(priceTable);

            // assert
        }
    }

    internal class OryzaCrawlerWithoutWebCaptureBlock : IBlock<string, Nothing>
    {
        private readonly IBlock<string, Nothing> _thisBlock;

        public OryzaCrawlerWithoutWebCaptureBlock(PriceTableParserBlock priceTableParserBlock,
                                                  ExtractBlock extractBlock,
                                                  SnapshotRepositoryBlock snapshotRepositoryBlock)
        {
            _thisBlock = priceTableParserBlock.ContinuesWith(extractBlock)
                                              .ContinuesWith(snapshotRepositoryBlock);
        }

        public Func<string, Nothing> Handle
        {
            get { return html => _thisBlock.Handle(html); }
        }
    }
}