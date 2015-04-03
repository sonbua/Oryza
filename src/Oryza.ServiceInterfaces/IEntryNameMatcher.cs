using System.Collections.Generic;
using Oryza.Entities;

namespace Oryza.ServiceInterfaces
{
    public interface IEntryNameMatcher
    {
        bool MatchEntryName(string entryName, ICollection<EntryType> existingEntryTypes, IEntryTypeNameConverter entryTypeNameConverter, out EntryType match);
    }
}