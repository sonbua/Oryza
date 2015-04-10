using System.Collections.Generic;
using Oryza.Entities;
using Raven.Client;

namespace Oryza.Utility
{
    public static class DocumentSessionExtensions
    {
        public static void BatchStore<T>(this IDocumentSession session, IEnumerable<T> entities) where T : IEntity
        {
            foreach (var entity in entities)
            {
                session.Store(entity);
            }
        }
    }
}