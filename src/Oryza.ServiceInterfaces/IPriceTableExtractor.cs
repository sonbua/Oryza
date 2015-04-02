namespace Oryza.ServiceInterfaces
{
    public interface IPriceTableExtractor : IDateExtractor, ICategoriesExtractor, IPriceUnitExtractor, IEntryTypeNameConverter
    {
    }
}