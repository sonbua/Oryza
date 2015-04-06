using Oryza.Entities;

namespace Oryza.ServiceInterfaces
{
    public interface IPriceTableExtractor : IDateExtractor, ICategoriesExtractor, ICategoryNameConverter, ICategoryNameMatcher, IEntryNameConverter, IEntryNameMatcher, IPriceUnitExtractor
    {
        Snapshot ExtractPriceTable(string priceTable);
    }
}