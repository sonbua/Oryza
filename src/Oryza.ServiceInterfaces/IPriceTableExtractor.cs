namespace Oryza.ServiceInterfaces
{
    public interface IPriceTableExtractor : IDateExtractor, ICategoriesExtractor, IPriceUnitExtractor, ICategoryNameConverter, ICategoryNameMatcher, IEntryNameConverter, IEntryNameMatcher
    {
    }
}