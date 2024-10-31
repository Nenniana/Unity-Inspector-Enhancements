using System.Collections.Generic;
using System.Reflection;

namespace InspectorEnhancements
{
    public class CacheMemberInfoProvider : IMemberInfoProvider
    {
        public List<TInfo> TryGetAllMemberInfo<TInfo>(object target) where TInfo : MemberInfo
        {
            List<TInfo> allMemberInfo = TypeBasedCacheHelper<TInfo>.GetOrAddList(
                target, 
                () => ReflectionHelper.FindAllMemberInfo<TInfo>(target)
            );

            if (allMemberInfo == null)
            {
                return null;
            }

            return allMemberInfo;
        }

        public TInfo TryGetMemberInfo<TInfo>(object target, string conditionName) where TInfo : MemberInfo
        {
            TInfo memberInfo = StringBasedCacheHelper<TInfo>.GetOrAdd(
                target, conditionName,
                () => ReflectionHelper.FindMemberInfo<TInfo>(target, conditionName)
            );

            if (memberInfo == null)
            {
                return null;
            }

            return memberInfo;
        }
    }
}