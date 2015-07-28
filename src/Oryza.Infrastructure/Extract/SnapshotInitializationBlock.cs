using System;
using LegoBuildingBlock;
using Oryza.Entities;

namespace Oryza.Infrastructure.Extract
{
    public class SnapshotInitializationBlock : IBlock<string, Snapshot>
    {
        public Func<string, Snapshot> Handle
        {
            get { return priceTable => new Snapshot {PriceTableData = priceTable}; }
        }
    }
}