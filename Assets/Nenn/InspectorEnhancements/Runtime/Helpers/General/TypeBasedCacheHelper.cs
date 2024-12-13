using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.General
{
    public static class TypeBasedCacheHelper<TValue>
    {
        private static Dictionary<(Type, BindingFlags), List<TValue>> cache = new Dictionary<(Type, BindingFlags), List<TValue>>();


        public static List<TValue> GetOrAddList(Type type, Func<TValue[]> computeValue, BindingFlags bindingFlags)
        {
            var cacheKey = (type, bindingFlags);
            if (!cache.TryGetValue(cacheKey, out var valueList))
            {
                valueList = computeValue().ToList();
                cache[cacheKey] = valueList;
            }

            return valueList;
        }

        public static void ClearCache()
        {
            cache.Clear();
        }
    }
}
