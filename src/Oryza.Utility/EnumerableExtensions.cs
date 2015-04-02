using System.Collections.Generic;

namespace Oryza.Utility
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                return true;
            }

            return !source.GetEnumerator().MoveNext();
        }
    }
}