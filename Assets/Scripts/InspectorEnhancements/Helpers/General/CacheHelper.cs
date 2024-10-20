using System.Collections.Generic;

namespace InspectorEnhancements
{
    public static class CacheHelper<TValue>
    {
        private static Dictionary<string, TValue> cache = new Dictionary<string, TValue>();

        public static TValue GetOrAdd(object target, string conditionName, System.Func<TValue> computeValue)
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

        private static string GenerateCacheKey(object target, string conditionName)
        {
            string key = $"{target.GetType().FullName}.{conditionName}";
            return key;
        }
    }
}
