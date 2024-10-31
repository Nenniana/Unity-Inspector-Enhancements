using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorEnhancements
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class MethodButtonDrawer : Editor
    {
        private readonly IMemberInfoProvider memberInfoProvider = new CacheMemberInfoProvider();
        private readonly IMethodResolver methodResolver = new DefaultMethodResolver();
        public override void OnInspectorGUI()
        {
            
        }

        private MethodInfo GetMethodInfo(object target, SerializedProperty property) 
        {
            MethodInfo methodInfo = memberInfoProvider.TryGetMemberInfo<MethodInfo>(target, property.name);

            if (methodInfo == null)
            {
                Debug.LogError($"No MethodInfo found for {property.name} on {target.GetType()}.");
                return null;
            }

            return methodInfo;
        }

        // DrawMethodParams Implementation
        
        private void InvokeMethod (object target, MethodInfo methodInfo, object[] methodParameterValues) 
        {
            methodInfo.Invoke(target, methodParameterValues);
        }
    }
}