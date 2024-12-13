using System;
using System.Collections.Generic;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.General
{
    public static class OverwritableStringCache<TValue>
    {
        private static readonly Dictionary<string, TValue> Cache = new Dictionary<string, TValue>();

        public static TValue GetOrAdd(Type target, string conditionName, Func<TValue> computeValue)
        {
            string key = GenerateCacheKey(target, conditionName);
            TValue value = GetOrAddByKey(key, computeValue);
            return value;
        }

        private static TValue GetOrAddByKey(string key, Func<TValue> computeValue)
        {
            if (!Cache.TryGetValue(key, out var value))
            {
                value = computeValue();
                Cache[key] = value;
            }

            return value;
        }

        public static void ClearCache()
        {
            Cache.Clear();
        }

        private static string GenerateCacheKey(Type target, string conditionName)
        {
            string key = $"{target.FullName}.{conditionName}";
            return key;
        }

        public static TValue OverwriteOrAdd(Type target, string conditionName, TValue value)
        {
            string key = GenerateCacheKey(target, conditionName);

            if (Cache.ContainsKey(key)) 
            {
                Cache[key] = value;
                return value;
            } 

            return GetOrAddByKey(key, () => value);
        }
    }
}