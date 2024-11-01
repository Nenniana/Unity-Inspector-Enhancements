using System;
using System.Collections.Generic;
using System.Reflection;

namespace InspectorEnhancements
{
    public interface IMemberInfoProvider
    {
        public TInfo TryGetMemberInfo<TInfo>(Type type, string conditionName, BindingFlags bindingFlags) where TInfo : MemberInfo;
        public List<TInfo> TryGetAllMemberInfo<TInfo>(Type type, BindingFlags bindingFlags) where TInfo : MemberInfo;
    }
}