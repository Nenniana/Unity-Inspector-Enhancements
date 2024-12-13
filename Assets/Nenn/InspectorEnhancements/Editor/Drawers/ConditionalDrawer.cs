using System;
using System.Reflection;
using Nenn.InspectorEnhancements.Runtime.Attributes.Conditional;
using Nenn.InspectorEnhancements.Runtime.Attributes.Conditional.Base;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberInfoProvider;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberInfoProvider.Base;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMethodInvoker;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMethodInvoker.Base;
using UnityEditor;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(ConditionalAttribute), true)] 
    public class ConditionalDrawer : PropertyDrawer
    {
        private readonly IMemberInfoProvider memberInfoProvider = new CacheMemberInfoProvider();
        private readonly IMethodResolver methodResolver = new DefaultMethodResolver();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string controlName = property.propertyPath;
            var attribute = this.attribute as ConditionalAttribute;
            string conditionName = attribute?.MemberName;

            GUI.SetNextControlName(controlName);

            EditorGUI.BeginChangeCheck();

            if (IsInvalidCustomClassOrStruct(property, conditionName))
            {
                DrawWarningForStructClass(position, property, label);
            }

            else if (EvaluateCondition(property, attribute, conditionName))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }

            if (GUI.GetNameOfFocusedControl() == controlName)
            {
                EditorGUI.FocusTextInControl(controlName);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var attribute = this.attribute as ConditionalAttribute;
            string conditionName = attribute?.MemberName;

            if (IsInvalidCustomClassOrStruct(property, conditionName))
            {
                return CalculateHelpBoxHeight(property);
            }

            if (EvaluateCondition(property, attribute, conditionName)) 
            {
                return EditorGUI.GetPropertyHeight(property, true);
            }
           
            // Otherwise return normal height.
            return 0f;
        }

        private bool EvaluateCondition(SerializedProperty property, ConditionalAttribute attribute, string conditionName)
        {
            bool shouldShow = true;
            bool invertCondition = attribute is HideIfAttribute;
            object target = property.serializedObject.targetObject;

            if (!string.IsNullOrEmpty(conditionName))
            {
                return InvertCondition(invertCondition, FindMemberAndEvaluate(attribute, target, conditionName));
            }

            // If no condition is found, check if property can be a null condition
            if (TryEvaluateField(target, property.name, ref shouldShow)) 
            {
                return InvertCondition(invertCondition, shouldShow);
            }      
            
            return true;
        }

        private bool IsInvalidCustomClassOrStruct(SerializedProperty property, string conditionName)
        {
            return string.IsNullOrEmpty(conditionName) 
                && IsTypeCustomClassOrStruct(property.GetType()) 
                || IsCustomClassOrStructField(property.serializedObject.targetObject, conditionName);
        }

        private bool IsCustomClassOrStructField(object target, string conditionName)
        {
            if (string.IsNullOrEmpty(conditionName)) return false;

            FieldInfo fieldInfo = memberInfoProvider.TryGetMemberInfo<FieldInfo>(target.GetType(), conditionName);
            PropertyInfo propertyInfo = memberInfoProvider.TryGetMemberInfo<PropertyInfo>(target.GetType(), conditionName);

            Type fieldType = null;

            if (fieldInfo != null)
            {
                fieldType = fieldInfo.FieldType;
            }
            else if (propertyInfo != null)
            {
                fieldType = propertyInfo.PropertyType;
            }

            return IsTypeCustomClassOrStruct(fieldType);
        }

        private static bool IsTypeCustomClassOrStruct(Type fieldType)
        {
            if (fieldType != null)
            {
                // Disregard if Unity native object or SerializedProperty
                if (typeof(UnityEngine.Object).IsAssignableFrom(fieldType) || fieldType == typeof(SerializedProperty))
                    return false;

                // Check if it's a class or struct, and not a primitive type
                return fieldType.IsClass || (fieldType.IsValueType && !fieldType.IsPrimitive);
            }

            return false;
        }

        private bool FindMemberAndEvaluate(ConditionalAttribute attribute, object target, string conditionName)
        {
            bool shouldShow = true;

            if (TryEvaluateMethod(attribute, target, conditionName, ref shouldShow))
                return shouldShow;

            if (TryEvaluateField(target, conditionName, ref shouldShow))
                return shouldShow;

            if (TryEvaluateProperty(target, conditionName, ref shouldShow))
                return shouldShow;

            // Log a warning if the condition is not found
            Debug.LogWarning($"Condition '{conditionName}' not found in {target.GetType()}");
            return shouldShow;
        }

        private bool InvertCondition (bool invertCondition, bool result) {
            return invertCondition ? !result : result;
        }

        private bool TryEvaluateMethod(ConditionalAttribute attribute, object target, string conditionName, ref bool shouldShow)
        {
            MethodInfo methodInfo = memberInfoProvider.TryGetMemberInfo<MethodInfo>(target.GetType(), conditionName);

            if (methodInfo == null)
                return false;

            object[] methodParameters = methodResolver.InvokeMethod(target, attribute.Parameters, methodInfo);
            object methodResult = methodInfo.Invoke(target, methodParameters);
            
            shouldShow = (bool)methodResult;
            return true;
        }

        private bool TryEvaluateProperty(object target, string conditionName, ref bool shouldShow)
        {
            PropertyInfo propertyInfo = memberInfoProvider.TryGetMemberInfo<PropertyInfo>(target.GetType(), conditionName);

            if (propertyInfo == null)
                return false;

            bool result = IsPropertyBoolean(target, propertyInfo);
            shouldShow = result;
            return true;
        }

        private bool TryEvaluateField(object target, string conditionName, ref bool shouldShow)
        {
            FieldInfo fieldInfo = memberInfoProvider.TryGetMemberInfo<FieldInfo>(target.GetType(), conditionName);

            if (fieldInfo == null)
                return false;

            bool result = IsFieldBoolean(target, fieldInfo);
            shouldShow = result;
            return true;
        }

        public bool IsFieldBoolean(object target, FieldInfo fieldInfo)
        {
            var fieldValue = fieldInfo.GetValue(target);
            return fieldInfo.FieldType == typeof(bool) ? (bool)fieldValue : fieldValue != null;
        }

        public bool IsPropertyBoolean(object target, PropertyInfo propertyInfo)
        {
            var propertyValue = propertyInfo.GetValue(target);
            return propertyInfo.PropertyType == typeof(bool) ? (bool)propertyValue : propertyValue != null;
        }

        private void DrawWarningForStructClass(Rect position, SerializedProperty property, GUIContent label)
        {
            string warningString = CreateStructClassWarningString();

            float helpBoxHeight = EditorStyles.helpBox.CalcHeight(new GUIContent(warningString), EditorGUIUtility.currentViewWidth);

            Rect helpBoxRect = new Rect(position.x, position.y, position.width, helpBoxHeight);
            EditorGUI.HelpBox(helpBoxRect, warningString, MessageType.Warning);

            Rect propertyRect = new Rect(position.x, position.y + helpBoxHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUI.GetPropertyHeight(property, true));
            EditorGUI.PropertyField(propertyRect, property, label, true);
        }

        private string CreateStructClassWarningString()
        {
            string warningString = " is not compatible with custom class or struct null checks. Use a separate bool condition or method instead.";
            if (attribute is HideIfAttribute)
                warningString = "[HideIf]" + warningString;
            else if (attribute is ShowIfAttribute)
                warningString = "[ShowIf]" + warningString;
            else
                warningString = "This system" + warningString;
            return warningString;
        }

        private float CalculateHelpBoxHeight(SerializedProperty property)
        {
            string helpBoxText = CreateStructClassWarningString();
            float helpBoxHeight = EditorStyles.helpBox.CalcHeight(new GUIContent(helpBoxText), EditorGUIUtility.currentViewWidth);
            return helpBoxHeight + EditorGUI.GetPropertyHeight(property, true);
        }
    }
}
