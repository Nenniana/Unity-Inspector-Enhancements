using System;
using System.Collections.Generic;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.General
{
    public static class StringBasedCacheHelper<TValue>
    {
        private static Dictionary<string, TValue> cache = new Dictionary<string, TValue>();

        public static TValue GetOrAdd(Type target, string conditionName, System.Func<TValue> computeValue)
        {
            string key = GenerateCacheKey(target, conditionName);
            if (!cache.TryGetValue(key, out var value))
            {
                value = computeValue();
                cache[key] = value;
            }
            return value;
        }

        public static void ClearCache()
        {
            cache.Clear();
        }

        private static string GenerateCacheKey(Type target, string conditionName)
        {
            string key = $"{target.FullName}.{conditionName}";
            return key;
        }
    }
}
