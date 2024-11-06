using System;
using System.Reflection;

namespace InspectorEnhancements
{
    public static class ReflectionHelper
    {
        public static TInfo FindMemberInfo<TInfo>(Type type, string name, BindingFlags bindingFlags) where TInfo : MemberInfo
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));

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
            else if (typeof (TInfo) == typeof(MemberInfo))
            {
                member = type.GetMember(name, bindingFlags);
            }

            return member as TInfo;
        }

        public static TInfo[] FindAllMemberInfo<TInfo>(Type type, BindingFlags bindingFlags) where TInfo : MemberInfo
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

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
            else if (typeof (TInfo) == typeof(MemberInfo))
            {
                members = type.GetMembers(bindingFlags);
            }
 
            return members as TInfo[];
        }
    }
}
