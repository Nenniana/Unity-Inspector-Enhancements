using System;
using System.Collections.Generic;
using System.Reflection;

namespace InspectorEnhancements
{
    public interface IMemberInfoProvider
    {
        public TInfo TryGetMemberInfo<TInfo>(Type type, string conditionName, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy) where TInfo : MemberInfo;
        public List<TInfo> TryGetAllMemberInfo<TInfo>(Type type, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy) where TInfo : MemberInfo;
    }
}