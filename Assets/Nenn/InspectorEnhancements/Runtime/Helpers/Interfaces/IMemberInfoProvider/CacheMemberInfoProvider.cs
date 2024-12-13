using System;
using System.Collections.Generic;
using System.Reflection;
using Nenn.InspectorEnhancements.Runtime.Helpers.General;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberInfoProvider
{
    public class CacheMemberInfoProvider : Base.IMemberInfoProvider
    {
        public List<TInfo> TryGetAllMemberInfo<TInfo>(Type type, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy) where TInfo : MemberInfo
        {
            List<TInfo> allMemberInfo = TypeBasedCacheHelper<TInfo>.GetOrAddList(
                type, 
                () => ReflectionHelper.FindAllMemberInfo<TInfo>(type, bindingFlags),
                bindingFlags
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