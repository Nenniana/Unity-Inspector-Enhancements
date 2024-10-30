using System;
using System.Reflection;

namespace InspectorEnhancements
{
    public static class ReflectionHelper
    {
        public static TInfo FindMemberInfo<TInfo>(object target, string name) where TInfo : MemberInfo
        {
            if (target == null) 
                throw new ArgumentNullException(nameof(target));

            if (string.IsNullOrEmpty(name)) 
                throw new ArgumentException("Name cannot be null or empty", nameof(name));

            var type = target.GetType();
            MemberInfo member = null;

            if (typeof(TInfo) == typeof(MethodInfo))
            {
                member = type.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            }
            else if (typeof(TInfo) == typeof(FieldInfo))
            {
                member = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            }
            else if (typeof(TInfo) == typeof(PropertyInfo))
            {
                member = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            }

            return member as TInfo;
        }

        public static MethodInfo TryGetMethodInfo(object target, string conditionName) {
            MethodInfo methodInfo = CacheHelper<MethodInfo>.GetOrAdd(
                target, conditionName,
                () => FindMethod(target, conditionName)
            );

            if (methodInfo == null)
            {
                return null;
            }

            return methodInfo;
        }
    }
}
