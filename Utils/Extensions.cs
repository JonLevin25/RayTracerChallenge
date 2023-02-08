using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
        public static bool IsNullOrWhitespace(this string str) => string.IsNullOrWhiteSpace(str);

        public static string JoinStr(this IEnumerable<string> str, char separator) => string.Join(separator, str);
        public static string JoinStr(this IEnumerable<string> str, string separator) => string.Join(separator, str);
        public static void SetLength<T>(this List<T> list, int length, Func<int, T> itemGetter = null )
        {
            var oldLength = list.Count;
            if (oldLength == length) return;
            
            itemGetter ??= i => default;
            
            if (length < oldLength)
            {
                // contract
                list.RemoveRange(length, oldLength - length);
            }
            else
            {
                // expand
                list.AddRange(
                    Enumerable
                        .Range(0, length - oldLength)
                        .Select(itemGetter)
                );
            }
        }

    }
}