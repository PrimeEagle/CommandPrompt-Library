using System.Collections.Generic;
using System.Linq;

namespace VNet.Utility
{
    public static class EnumerableUtility
    {

        public static bool AllExistsIn<T>(this IEnumerable<T> sourceList, IEnumerable<T> secondList)
        {
            return sourceList.All(x => secondList.Any(y => x.Equals(y)));
        }
    }
}
