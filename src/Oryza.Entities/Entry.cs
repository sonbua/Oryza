namespace Oryza.Entities
{
    public class Entry : IEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public EntryType Type { get; set; }

        public bool IsPriceAvailable { get; set; }

        public decimal LowPrice { get; set; }

        public decimal HighPrice { get; set; }

        public Metadata Metadata { get; set; }
    }
}