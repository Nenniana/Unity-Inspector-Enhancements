using System;
using System.Collections.Generic;
using System.Reflection;

namespace InspectorEnhancements
{
    public class CacheMemberInfoProvider : IMemberInfoProvider
    {
        public List<TInfo> TryGetAllMemberInfo<TInfo>(Type type, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy) where TInfo : MemberInfo
        {
            List<TInfo> allMemberInfo = TypeBasedCacheHelper<TInfo>.GetOrAddList(
                type, 
                () => ReflectionHelper.FindAllMemberInfo<TInfo>(type, bindingFlags)
            );

            if (allMemberInfo == null)
            {
                return null;
            }

            return allMemberInfo;
        }

        public TInfo TryGetMemberInfo<TInfo>(Type type, string conditionName, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy) where TInfo : MemberInfo
        {
            TInfo memberInfo = StringBasedCacheHelper<TInfo>.GetOrAdd(
                type, conditionName,
                () => ReflectionHelper.FindMemberInfo<TInfo>(type, conditionName, bindingFlags)
            );

            if (memberInfo == null)
            {
                return null;
            }

            return memberInfo;
        }
    }
}