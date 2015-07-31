using System;
using System.Collections.Generic;
using System.Linq;
using LegoBuildingBlock;
using Oryza.Entities;
using Oryza.Utility;

namespace Oryza.Infrastructure.DataAccess
{
    public class EntryNameMatcherBlock : IBlock<EntriesMatching, Match<EntryType>>
    {
        public EntryNameMatcherBlock(NameToTypeConverterBlock nameToTypeConverterBlock)
        {
            Handle = entriesMatching =>
                     {
                         var newEntryTypeName = nameToTypeConverterBlock.Handle(entriesMatching.EntryName);

                         foreach (var entryType in entriesMatching.ExistingEntryTypes)
                         {
                             if (entryType.NameVariants.Any(x => x == entriesMatching.EntryName))
                             {
                                 return new Match<EntryType>
                                        {
                                            IsMatched = true,
                                            Matched = entryType
                                        };
                             }

                             if (entryType.Name.EqualsIgnoreCase(newEntryTypeName))
                             {
                                 return new Match<EntryType>
                                        {
                                            IsMatched = true,
                                            Matched = entryType
                                        };
                             }
                         }

                         return new Match<EntryType> {IsMatched = false};
                     };
        }

        public Func<EntriesMatching, Match<EntryType>> Handle { get; private set; }
    }

    public class EntriesMatching
    {
        public string EntryName { get; set; }

        public IEnumerable<EntryType> ExistingEntryTypes { get; set; }
    }
}