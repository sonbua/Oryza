using System;
using LegoBuildingBlock;
using Oryza.Infrastructure.Capture;
using Oryza.Infrastructure.DataAccess;
using Oryza.Infrastructure.Extract;
using Oryza.Infrastructure.Parsing;

namespace Oryza.Infrastructure
{
    public class OryzaCrawlerBlock : IBlock<Uri, Nothing>
    {
        private readonly IBlock<Uri, Nothing> _thisBlock;

        public OryzaCrawlerBlock(WebCaptureAsyncBlock webCaptureAsyncBlock,
                                 PriceTableParserBlock priceTableParserBlock,
                                 ExtractBlock extractBlock,
                                 SnapshotRepositoryBlock snapshotRepositoryBlock)
        {
            _thisBlock = webCaptureAsyncBlock.ContinuesWith(new ResultSynchronizationBlock<string>())
                                             .ContinuesWith(priceTableParserBlock)
                                             .ContinuesWith(extractBlock)
                                             .ContinuesWith(snapshotRepositoryBlock);
        }

        public Func<Uri, Nothing> Handle
        {
            get { return uri => _thisBlock.Handle(uri); }
        }
    }
}