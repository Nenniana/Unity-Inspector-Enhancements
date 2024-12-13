using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.General
{
    public static class TypeBasedCacheHelper<TValue>
    {
        private static readonly Dictionary<(Type, BindingFlags), List<TValue>> Cache = new Dictionary<(Type, BindingFlags), List<TValue>>();


        public static List<TValue> GetOrAddList(Type type, Func<TValue[]> computeValue, BindingFlags bindingFlags)
        {
            var cacheKey = (type, bindingFlags);
            if (!Cache.TryGetValue(cacheKey, out var valueList))
            {
                valueList = computeValue().ToList();
                Cache[cacheKey] = valueList;
            }

            return valueList;
        }

        public static void ClearCache()
        {
            Cache.Clear();
        }
    }
}
