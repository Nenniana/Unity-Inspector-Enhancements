using System;
using System.Collections.Generic;

namespace InspectorEnhancements
{
    public static class OverwriteableStringCache<TValue>
    {
        private static Dictionary<string, TValue> cache = new Dictionary<string, TValue>();

        public static TValue GetOrAdd(Type target, string conditionName, System.Func<TValue> computeValue)
        {
            string key = GenerateCacheKey(target, conditionName);
            TValue value = GetOrAddByKey(key, computeValue);
            return value;
        }

        private static TValue GetOrAddByKey(string key, Func<TValue> computeValue)
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

        private static string GenerateCacheKey(Type target, string conditionName)
        {
            string key = $"{target.FullName}.{conditionName}";
            return key;
        }

        public static void OverwriteOrAdd(Type target, string conditionName, TValue value)
        {
            string key = GenerateCacheKey(target, conditionName);

            if (cache.ContainsKey(key)) 
            {
                cache[key] = value;
                return;
            } 

            GetOrAddByKey(key, () => value);
        }
    }
}