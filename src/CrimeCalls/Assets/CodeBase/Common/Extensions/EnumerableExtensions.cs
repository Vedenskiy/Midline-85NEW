using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<TValue>(this IEnumerable<TValue> enumerable)
        {
            if (enumerable == null)
                return true;

            if (enumerable is ICollection<TValue> collection)
                return collection.Count == 0;

            return enumerable.Any();
        }
    }
}