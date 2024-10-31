using System;
using System.Collections.Generic;

namespace InspectorEnhancements
{
    public static class CacheHelper<TValue>
    {
        private static Dictionary<string, TValue> cache = new Dictionary<string, TValue>();

        // Public method for name-based key
        public static TValue GetOrAddByName(object target, string conditionName, Func<TValue> computeValue)
        {
            string key = GenerateCacheKeyByName(target, conditionName);
            return GetOrAddInternal(key, computeValue);
        }

        // Public method for type-based key
        public static TValue GetOrAddByType(object target, Func<TValue> computeValue)
        {
            string key = GetTypeKey(target);
            return GetOrAddInternal(key, computeValue);
        }

        // Internal method for retrieving or adding to the cache
        private static TValue GetOrAddInternal(string key, Func<TValue> computeValue)
        {
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

        // Private helper to generate a key based on object type and condition name
        private static string GenerateCacheKeyByName(object target, string conditionName)
        {
            return $"{target.GetType().FullName}.{conditionName}";
        }

        private static string GetTypeKey(object target)
        {
            return target.GetType().FullName;
        }
    }
}
