using System;
using LegoBuildingBlock;
using Oryza.Entities;

namespace Oryza.Infrastructure.Extract
{
    public class ExtractBlock : IBlock<string, Snapshot>
    {
        private readonly SnapshotInitializationBlock _snapshotInitializationBlock;
        private readonly PriceUnitExtractorBlock _priceUnitExtractorBlock;
        private readonly DateExtractorBlock _dateExtractorBlock;
        private readonly CategoriesExtractorBlock _categoriesExtractorBlock;

        public ExtractBlock(SnapshotInitializationBlock snapshotInitializationBlock,
                            PriceUnitExtractorBlock priceUnitExtractorBlock,
                            DateExtractorBlock dateExtractorBlock,
                            CategoriesExtractorBlock categoriesExtractorBlock)
        {
            _snapshotInitializationBlock = snapshotInitializationBlock;
            _priceUnitExtractorBlock = priceUnitExtractorBlock;
            _dateExtractorBlock = dateExtractorBlock;
            _categoriesExtractorBlock = categoriesExtractorBlock;
        }

        public string Input { get; set; }

        public Func<string, Snapshot> Work
        {
            get
            {
                return priceTable => _snapshotInitializationBlock.ContinuesWith(_priceUnitExtractorBlock)
                                                                 .ContinuesWith(_dateExtractorBlock)
                                                                 .ContinuesWith(_categoriesExtractorBlock)
                                                                 .Work(priceTable);
            }
        }
    }
}