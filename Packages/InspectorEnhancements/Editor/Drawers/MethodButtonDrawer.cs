using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorEnhancements
{
    [CustomPropertyDrawer(typeof(MethodButtonAttribute))]
    public class MethodButtonDrawer : PropertyDrawer
    {
        private readonly IMemberInfoProvider memberInfoProvider = new CacheMemberInfoProvider();
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            object target = property.serializedObject.targetObject;
            MethodButtonAttribute attribute = this.attribute as MethodButtonAttribute;
            ParameterInfo[] methodParameters = GetMethodParams(target, property);

            if (methodParameters == null || methodParameters.Length <= 0)
            {
                // Method is parameterless
            }
        }

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