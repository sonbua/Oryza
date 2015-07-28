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

        public string Input { get; set; }

        public Func<string, Snapshot> Work
        {
            get { return priceTable => _thisBlock.Work(priceTable); }
        }
    }
}