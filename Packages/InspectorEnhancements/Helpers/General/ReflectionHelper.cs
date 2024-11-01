using System;
using System.Reflection;

namespace InspectorEnhancements
{
    public static class ReflectionHelper
    {
        public static TInfo FindMemberInfo<TInfo>(object target, string name, BindingFlags bindingFlags) where TInfo : MemberInfo
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));

            var type = target.GetType();
            MemberInfo member = null;

            if (typeof(TInfo) == typeof(MethodInfo))
            {
                member = type.GetMethod(name, bindingFlags);
            }
            else if (typeof(TInfo) == typeof(FieldInfo))
            {
                member = type.GetField(name, bindingFlags);
            }
            else if (typeof(TInfo) == typeof(PropertyInfo))
            {
                member = type.GetProperty(name, bindingFlags);
            }

            return member as TInfo;
        }

        public static TInfo[] FindAllMemberInfo<TInfo>(object target, BindingFlags bindingFlags) where TInfo : MemberInfo
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            var type = target.GetType();
            MemberInfo[] members = null;

            if (typeof(TInfo) == typeof(MethodInfo))
            {
                members = type.GetMethods(bindingFlags);
            }
            else if (typeof(TInfo) == typeof(FieldInfo))
            {
                members = type.GetFields(bindingFlags);
            }
            else if (typeof(TInfo) == typeof(PropertyInfo))
            {
                members = type.GetProperties(bindingFlags);
            }

            return members as TInfo[];
        }
    }
}
