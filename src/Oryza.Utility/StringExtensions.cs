using System;
using System.Collections.Generic;

namespace Oryza.Utility
{
    public static class StringExtensions
    {
        public static string JoinStrings(this IEnumerable<string> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return string.Join(string.Empty, source);
        }

        public static string JoinStrings(this IEnumerable<string> source, string separator)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (separator == null)
            {
                throw new ArgumentNullException("separator");
            }

            return string.Join(string.Empty, source);
        }
    }
}