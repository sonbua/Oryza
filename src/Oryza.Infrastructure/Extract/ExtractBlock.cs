using System;
using LegoBuildingBlock;
using Oryza.Entities;

namespace Oryza.Infrastructure.Extract
{
    public class ExtractBlock : IBlock<string, Snapshot>
    {
        private readonly IBlock<string, Snapshot> _thisBlock;

        public ExtractBlock(SnapshotInitializationBlock snapshotInitializationBlock,
                            PriceUnitExtractorBlock priceUnitExtractorBlock,
                            DateExtractorBlock dateExtractorBlock,
                            CategoriesExtractorBlock categoriesExtractorBlock)
        {
            _thisBlock = snapshotInitializationBlock.ContinuesWith(priceUnitExtractorBlock)
                                                    .ContinuesWith(dateExtractorBlock)
                                                    .ContinuesWith(categoriesExtractorBlock);
        }

        public Func<string, Snapshot> Handle
        {
            get { return priceTable => _thisBlock.Handle(priceTable); }
        }
    }
}