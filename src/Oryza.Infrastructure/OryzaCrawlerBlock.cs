using System;
using LegoBuildingBlock;
using LegoBuildingBlock.ControlStructure;
using Oryza.Infrastructure.Capture;
using Oryza.Infrastructure.DataAccess;
using Oryza.Infrastructure.Extract;
using Oryza.Infrastructure.Parsing;

namespace Oryza.Infrastructure
{
    public class OryzaCrawlerBlock : IBlock<Uri, Nothing>
    {
        public OryzaCrawlerBlock(WebCaptureAsyncBlock webCaptureAsyncBlock, PriceTableParserBlock priceTableParserBlock, ExtractBlock extractBlock, SnapshotRepositoryBlock snapshotRepositoryBlock)
        {
            Handle = uri => webCaptureAsyncBlock.ContinuesWith(new ResultSynchronizationBlock<string>())
                                                .ContinuesWith(priceTableParserBlock)
                                                .ContinuesWith(extractBlock)
                                                .ContinuesWith(snapshotRepositoryBlock)
                                                .Handle(uri);
        }

        public Func<Uri, Nothing> Handle { get; private set; }
    }
}