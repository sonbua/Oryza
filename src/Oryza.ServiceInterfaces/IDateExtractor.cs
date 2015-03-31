using System;

namespace Oryza.ServiceInterfaces
{
    public interface IDateExtractor
    {
        DateTime ExtractDate(string priceTable);
    }
}