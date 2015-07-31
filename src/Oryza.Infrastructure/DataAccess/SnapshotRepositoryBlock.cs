using System;
using System.Collections.Generic;
using System.Linq;
using LegoBuildingBlock;
using Oryza.Entities;
using Oryza.Utility;
using Raven.Client;

namespace Oryza.Infrastructure.DataAccess
{
    public class SnapshotRepositoryBlock : IBlock<Snapshot, Nothing>
    {
        private readonly IDocumentSession _session;
        private readonly NameToTypeConverterBlock _nameToTypeConverterBlock;
        private readonly CategoryNameMatcherBlock _categoryNameMatcherMatcher;
        private readonly EntryNameMatcherBlock _entryNameMatcherMatcher;

        public SnapshotRepositoryBlock(IDocumentSession session, NameToTypeConverterBlock nameToTypeConverterBlock, CategoryNameMatcherBlock categoryNameMatcherMatcher, EntryNameMatcherBlock entryNameMatcherMatcher)
        {
            _session = session;
            _nameToTypeConverterBlock = nameToTypeConverterBlock;
            _categoryNameMatcherMatcher = categoryNameMatcherMatcher;
            _entryNameMatcherMatcher = entryNameMatcherMatcher;
        }

        public Func<Snapshot, Nothing> Handle
        {
            get
            {
                return snapshot =>
                       {
                           var existingCategoryTypes = _session.Query<CategoryType>().ToList();
                           var existingEntryTypes = _session.Query<EntryType>().ToList();

                           foreach (var category in snapshot.Categories)
                           {
                               foreach (var entry in category.Entries)
                               {
                                   var entryNameMatch = _entryNameMatcherMatcher.Handle(new EntriesMatching {EntryName = entry.Name, ExistingEntryTypes = existingEntryTypes});

                                   if (entryNameMatch.IsMatched)
                                   {
                                       if (!entryNameMatch.Matched.NameVariants.Contains(entry.Name))
                                       {
                                           entryNameMatch.Matched.NameVariants.Add(entry.Name);
                                           entry.Type = entryNameMatch.Matched;
                                       }
                                   }
                                   else
                                   {
                                       var newEntryType = new EntryType
                                                          {
                                                              Name = _nameToTypeConverterBlock.Handle(entry.Name),
                                                              NameVariants = new List<string> {entry.Name}
                                                          };

                                       existingEntryTypes.Add(newEntryType);
                                       entry.Type = newEntryType;
                                   }
                               }

                               var categoryNameMatch = _categoryNameMatcherMatcher.Handle(new CategoriesMatching {CategoryName = category.Name, ExistingCategoryTypes = existingCategoryTypes});

                               if (categoryNameMatch.IsMatched)
                               {
                                   if (!categoryNameMatch.Matched.NameVariants.Contains(category.Name))
                                   {
                                       categoryNameMatch.Matched.NameVariants.Add(category.Name);
                                       category.Type = categoryNameMatch.Matched;
                                   }
                               }
                               else
                               {
                                   var newCategoryType = new CategoryType
                                                         {
                                                             Name = _nameToTypeConverterBlock.Handle(category.Name),
                                                             NameVariants = new List<string> {category.Name}
                                                         };

                                   existingCategoryTypes.Add(newCategoryType);
                                   category.Type = newCategoryType;
                               }
                           }

                           _session.BatchStore(existingEntryTypes);
                           _session.BatchStore(existingCategoryTypes);

                           foreach (var category in snapshot.Categories)
                           {
                               foreach (var entry in category.Entries)
                               {
                                   _session.Store(entry);
                               }

                               _session.Store(category);
                           }

                           _session.Store(snapshot);

                           _session.SaveChanges();

                           return new Nothing();
                       };
            }
        }
    }
}