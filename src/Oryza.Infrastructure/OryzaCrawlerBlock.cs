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
        private readonly IBlock<Uri, Nothing> _compositeBlock;

        public OryzaCrawlerBlock(WebCaptureBlock webCaptureBlock,
                                 PriceTableParserBlock priceTableParserBlock,
                                 ExtractBlock extractBlock,
                                 SnapshotRepositoryBlock snapshotRepositoryBlock)
        {
            _compositeBlock = webCaptureBlock.ContinuesWith(priceTableParserBlock)
                                             .ContinuesWith(extractBlock)
                                             .ContinuesWith(snapshotRepositoryBlock);
        }

        public Uri Input { get; set; }

        public Func<Uri, Nothing> Work
        {
            get { return uri => _compositeBlock.Work(uri); }
        }
    }
}