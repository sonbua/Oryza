using System.Collections.Generic;
using Oryza.Entities;

namespace Oryza.ServiceInterfaces
{
    public interface IEntryNameMatcher
    {
        /// <summary>
        /// Try to match this new captured entry name <paramref name="entryName"/> in the existing entry types <paramref name="existingEntryTypes"/> stored in database.
        /// </summary>
        /// <param name="entryName"></param>
        /// <param name="existingEntryTypes"></param>
        /// <param name="entryNameConverter"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        bool TryMatchEntryName(string entryName, IEnumerable<EntryType> existingEntryTypes, IEntryNameConverter entryNameConverter, out EntryType match);
    }
}