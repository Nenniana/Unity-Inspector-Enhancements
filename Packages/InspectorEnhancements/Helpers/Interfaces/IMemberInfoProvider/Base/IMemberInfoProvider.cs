using System;
using System.Collections.Generic;
using System.Reflection;

namespace InspectorEnhancements
{
    public interface IMemberInfoProvider
    {
        public TInfo TryGetMemberInfo<TInfo>(object target, string conditionName, BindingFlags bindingFlags) where TInfo : MemberInfo;
        public List<TInfo> TryGetAllMemberInfo<TInfo>(object target, BindingFlags bindingFlags) where TInfo : MemberInfo;
    }
}