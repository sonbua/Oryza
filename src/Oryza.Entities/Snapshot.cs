using System.Collections.Generic;

namespace Oryza.Entities
{
    /// <summary>
    /// Contains information about a single snapshot captured from http://oryza.com.
    /// </summary>
    public class Snapshot : IEntity
    {
        public long Id { get; set; }

        public ICollection<Category> Categories { get; set; }

        /// <summary>
        /// Holds the price unit of this snapshot, mostly USD per ton.
        /// </summary>
        public string PriceUnit { get; set; }

        /// <summary>
        /// Holds original HTML data that might be used to verify or re-analyze in the future.
        /// </summary>
        public string RawData { get; set; }

        /// <summary>
        /// Holds price table in HTML, that are necessarily ready for analysis.
        /// </summary>
        public string PriceTableData { get; set; }

        public Metadata Metadata { get; set; }
    }
}