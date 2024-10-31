using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorEnhancements
{
    [CustomPropertyDrawer(typeof(MethodButtonAttribute))]
    public class MethodButtonDrawer : PropertyDrawer
    {
        private readonly IMemberInfoProvider memberInfoProvider = new CacheMemberInfoProvider();
        // OnGUI Implementation

        // PropertyHeight Implementation

        private ParameterInfo[] GetMethodParams(object target, SerializedProperty property) 
        {
            MethodInfo methodInfo = memberInfoProvider.TryGetMemberInfo<MethodInfo>(target, property.name);

            if (methodInfo == null)
            {
                Debug.LogError($"No MethodInfo found for {property.name} on {target.GetType()}.");
                return null;
            }

            return methodInfo.GetParameters();
        }

        // DrawMethodParams Implementation
        
        // InvokeMethod Implementation
    }
}