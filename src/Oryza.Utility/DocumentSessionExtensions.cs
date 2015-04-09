using System;
using System.Collections.Generic;
using Oryza.Entities;
using Raven.Client;

namespace Oryza.Utility
{
    public static class DocumentSessionExtensions
    {
        public static void BatchStoreWithMetadata<T>(this IDocumentSession session, IEnumerable<T> entities) where T : IEntity
        {
            foreach (var entity in entities)
            {
                session.StoreWithMetadata(entity);
            }
        }

        public static void StoreWithMetadata<T>(this IDocumentSession session, T entity) where T : IEntity
        {
            UpdateMetadata(entity);

            session.Store(entity);
        }

        private static void UpdateMetadata<T>(T entity) where T : IEntity
        {
            if (entity.Metadata == default (Metadata))
            {
                entity.Metadata = new Metadata(DateTime.UtcNow, DateTime.UtcNow);
                return;
            }

            entity.Metadata = new Metadata(entity.Metadata.CreatedAt, DateTime.UtcNow);
        }
    }
}