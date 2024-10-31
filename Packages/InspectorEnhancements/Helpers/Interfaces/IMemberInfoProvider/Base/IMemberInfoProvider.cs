using System.Collections.Generic;
using System.Reflection;

namespace InspectorEnhancements
{
    public interface IMemberInfoProvider
    {
        public TInfo TryGetMemberInfo<TInfo>(object target, string conditionName) where TInfo : MemberInfo;
        public List<TInfo> TryGetAllMemberInfo<TInfo>(object target) where TInfo : MemberInfo;
    }
}