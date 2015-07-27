using System;
using LegoBuildingBlock;
using Oryza.Entities;

namespace Oryza.Infrastructure.Extract
{
    public class SnapshotInitializationBlock : IBlock<string, Snapshot>
    {
        public string Input { get; set; }

        public Func<string, Snapshot> Work
        {
            get { return priceTable => new Snapshot {PriceTableData = priceTable}; }
        }
    }
}