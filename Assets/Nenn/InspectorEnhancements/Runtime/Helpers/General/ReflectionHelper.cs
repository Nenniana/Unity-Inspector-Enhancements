using System;
using System.Reflection;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.General
{
    public static class ReflectionHelper
    {
        public static TInfo FindMemberInfo<TInfo>(Type type, string name, BindingFlags bindingFlags) where TInfo : MemberInfo
        {
            try
            {
                if (type == null)
                {
                    EditorOnlyLogger.LogWarning("Type parameter is null.");
                    return null;
                }

                if (string.IsNullOrEmpty(name))
                {
                    EditorOnlyLogger.LogWarning("Name parameter is null or empty.");
                    return null;
                }

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
                else
                {
                    EditorOnlyLogger.LogWarning($"Unsupported MemberInfo type: {typeof(TInfo)}");
                    return null;
                }

                if (member is TInfo result)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                EditorOnlyLogger.LogError($"Error finding member '{name}' in type '{type}': {ex.Message}");
                return null;
            }
        }

        public static TInfo[] FindAllMemberInfo<TInfo>(Type type, BindingFlags bindingFlags) where TInfo : MemberInfo
        {
            try
            {
                if (type == null)
                {
                    EditorOnlyLogger.LogWarning("Type parameter is null.");
                    return null;
                }

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
                else if (typeof(TInfo) == typeof(MemberInfo))
                {
                    members = type.GetMembers(bindingFlags);
                }
                else
                {
                    EditorOnlyLogger.LogWarning($"Unsupported MemberInfo type: {typeof(TInfo)}");
                    return null;
                }

                if (members is TInfo[] result)
                {
                    return result;
                }
                else
                {
                    EditorOnlyLogger.LogWarning($"Type '{type.FullName}' is not of the expected type '{typeof(TInfo).Name}' array.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                EditorOnlyLogger.LogError($"Error finding member of '{type}': {ex.Message}");
                return null;
            }
        }
    }
}
