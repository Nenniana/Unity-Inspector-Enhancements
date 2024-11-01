using System;
using System.Collections.Generic;
using System.Reflection;

namespace InspectorEnhancements
{
    public class CacheMemberInfoProvider : IMemberInfoProvider
    {
        public List<TInfo> TryGetAllMemberInfo<TInfo>(object target, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy) where TInfo : MemberInfo
        {
            List<TInfo> allMemberInfo = TypeBasedCacheHelper<TInfo>.GetOrAddList(
                target, 
                () => ReflectionHelper.FindAllMemberInfo<TInfo>(target, bindingFlags)
            );

            if (allMemberInfo == null)
            {
                return null;
            }

            return allMemberInfo;
        }

        public TInfo TryGetMemberInfo<TInfo>(object target, string conditionName, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy) where TInfo : MemberInfo
        {
            TInfo memberInfo = StringBasedCacheHelper<TInfo>.GetOrAdd(
                target, conditionName,
                () => ReflectionHelper.FindMemberInfo<TInfo>(target, conditionName, bindingFlags)
            );

            if (memberInfo == null)
            {
                return null;
            }

            return memberInfo;
        }
    }
}