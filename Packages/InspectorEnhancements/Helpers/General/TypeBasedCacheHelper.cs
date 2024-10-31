using System;
using System.Collections.Generic;
using System.Linq;

namespace InspectorEnhancements
{
    public static class TypeBasedCacheHelper<TValue>
    {
        private static Dictionary<Type, List<TValue>> cache = new Dictionary<Type, List<TValue>>();

        public static List<TValue> GetOrAddList(object target, Func<TValue[]> computeValue)
        {
            Type typeKey = target.GetType();
            if (!cache.TryGetValue(typeKey, out var valueList))
            {
                valueList = computeValue().ToList();
                cache[typeKey] = valueList;
            }

            return valueList;
        }

        public static void ClearCache()
        {
            cache.Clear();
        }
    }
}
