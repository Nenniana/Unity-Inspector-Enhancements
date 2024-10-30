using System.Reflection;

namespace InspectorEnhancements
{
    public class CacheMemberInfoProvider : IMemberInfoProvider
    {
        public TInfo TryGetMemberInfo<TInfo>(object target, string conditionName) where TInfo : MemberInfo
        {
            TInfo memberInfo = CacheHelper<TInfo>.GetOrAdd(
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