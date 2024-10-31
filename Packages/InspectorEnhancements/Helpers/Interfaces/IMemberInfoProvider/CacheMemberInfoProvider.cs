using System.Reflection;

namespace InspectorEnhancements
{
    public class CacheMemberInfoProvider : IMemberInfoProvider
    {
        public TInfo[] TryGetAllMemberInfo<TInfo>(object target) where TInfo : MemberInfo
        {
            throw new System.NotImplementedException();
        }

        public TInfo TryGetMemberInfo<TInfo>(object target, string conditionName) where TInfo : MemberInfo
        {
            TInfo memberInfo = CacheHelper<TInfo>.GetOrAddByName(
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