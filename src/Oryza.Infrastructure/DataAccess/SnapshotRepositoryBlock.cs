using System;
using System.Collections.Generic;
using System.Linq;
using LegoBuildingBlock;
using Oryza.Entities;
using Oryza.ServiceInterfaces;
using Oryza.Utility;
using Raven.Client;

namespace Oryza.Infrastructure.DataAccess
{
    public class SnapshotRepositoryBlock : IBlock<Snapshot, Nothing>
    {
        private readonly IDocumentSession _session;
        private readonly ICategoryNameMatcher _categoryNameMatcher;
        private readonly ICategoryNameConverter _categoryNameConverter;
        private readonly IEntryNameMatcher _entryNameMatcher;
        private readonly IEntryNameConverter _entryNameConverter;

        public SnapshotRepositoryBlock(IDocumentSession session, ICategoryNameMatcher categoryNameMatcher, ICategoryNameConverter categoryNameConverter, IEntryNameMatcher entryNameMatcher, IEntryNameConverter entryNameConverter)
        {
            _session = session;
            _categoryNameMatcher = categoryNameMatcher;
            _categoryNameConverter = categoryNameConverter;
            _entryNameMatcher = entryNameMatcher;
            _entryNameConverter = entryNameConverter;
        }

        public Snapshot Input { get; set; }

        public Func<Snapshot, Nothing> Work
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
                                   EntryType matchEntryType;

                                   if (_entryNameMatcher.TryMatchEntryName(entry.Name, existingEntryTypes, _entryNameConverter, out matchEntryType))
                                   {
                                       if (!matchEntryType.NameVariants.Contains(entry.Name))
                                       {
                                           matchEntryType.NameVariants.Add(entry.Name);
                                           entry.Type = matchEntryType;
                                       }
                                   }
                                   else
                                   {
                                       var newEntryType = new EntryType
                                                          {
                                                              Name = _entryNameConverter.ConvertEntryName(entry.Name),
                                                              NameVariants = new List<string> {entry.Name}
                                                          };

                                       existingEntryTypes.Add(newEntryType);
                                       entry.Type = newEntryType;
                                   }
                               }

                               CategoryType matchCategoryType;

                               if (_categoryNameMatcher.TryMatchCategoryName(category.Name, existingCategoryTypes, _categoryNameConverter, out matchCategoryType))
                               {
                                   if (!matchCategoryType.NameVariants.Contains(category.Name))
                                   {
                                       matchCategoryType.NameVariants.Add(category.Name);
                                       category.Type = matchCategoryType;
                                   }
                               }
                               else
                               {
                                   var newCategoryType = new CategoryType
                                                         {
                                                             Name = _categoryNameConverter.ConvertCategoryName(category.Name),
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