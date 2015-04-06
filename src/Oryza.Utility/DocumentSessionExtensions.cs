using System.Collections.Generic;
using Raven.Client;

namespace Oryza.Utility
{
    public static class DocumentSessionExtensions
    {
        public static void BatchStore<T>(this IDocumentSession session, IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                session.Store(entity);
            }
        }
    }
}