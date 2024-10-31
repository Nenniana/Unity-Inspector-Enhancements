using System.Reflection;

namespace InspectorEnhancements
{
    public interface IMemberInfoProvider
    {
        public TInfo TryGetMemberInfo<TInfo>(object target, string conditionName) where TInfo : MemberInfo;
        public TInfo[] TryGetAllMemberInfo<TInfo>(object target, string conditionName) where TInfo : MemberInfo;
    }
}