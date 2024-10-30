using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorEnhancements
{
    [CustomPropertyDrawer(typeof(ArrayDropdownAttribute))]
    public class ArrayDropdownDrawer : PropertyDrawer
    {
        private IMemberInfoProvider memberInfoProvider = new CacheMemberInfoProvider();
        private IMethodInvoker methodInvoker = new DefaultMethodInvoker();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            object target = property.serializedObject.targetObject;
            FieldInfo propertyFieldInfo = memberInfoProvider.TryGetMemberInfo<FieldInfo>(target, property.name);

            if (propertyFieldInfo == null)
            {
                Debug.LogWarning("No Property FieldInfo is found.");
                return;
            }

            ArrayDropdownAttribute dropdownAttribute = (ArrayDropdownAttribute)attribute;

            if (string.IsNullOrEmpty(dropdownAttribute.Condition))
            {
                Debug.LogWarning("Parameter is either null or empty.");
                return;
            }

            Array values = GetDropdownValues(dropdownAttribute, target, propertyFieldInfo);

            CreateDropdown(position, property, label, values, propertyFieldInfo, target);

            property.serializedObject.ApplyModifiedProperties();
        }
        
        private void CreateDropdown(Rect position, SerializedProperty property, GUIContent label, Array options, FieldInfo propertyFieldInfo, object target)
        {
            if (options != null && options.Length > 0)
            {
                string[] optionStrings = options.Cast<object>()
                    .Select(option => option?.ToString() ?? "null")
                    .ToArray();

                // Find the current index of the selected value in the list
                int currentIndex = Array.IndexOf(options, propertyFieldInfo.GetValue(target));
                if (currentIndex == -1) currentIndex = 0; // Default to the first item if not found

                // Create the dropdown
                int selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, optionStrings);

                // Update the property value with the selected key
                object selectedOption = options.GetValue(selectedIndex);
                propertyFieldInfo.SetValue(target, selectedOption);
                property.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Array is null or empty.");
            }
        }

        private Array GetDropdownValues(ArrayDropdownAttribute dropdownAttribute, object target, FieldInfo propertyFieldInfo) 
        {
            FieldInfo fieldInfo = memberInfoProvider.TryGetMemberInfo<FieldInfo>(target, dropdownAttribute.Condition);
            if (fieldInfo != null)
            {
                return TryGetFieldValueArray(fieldInfo, propertyFieldInfo, target);
            }

            PropertyInfo propertyInfo = memberInfoProvider.TryGetMemberInfo<PropertyInfo>(target, dropdownAttribute.Condition);
            if (propertyInfo != null)
            {
                return TryGetPropertyValueArray(propertyInfo, propertyFieldInfo, target);
            }

            MethodInfo methodInfo = memberInfoProvider.TryGetMemberInfo<MethodInfo>(target, dropdownAttribute.Condition);
            if (methodInfo != null)
            {
                return TryGetMethodValueArray(dropdownAttribute.Parameters, methodInfo, propertyFieldInfo, target);
            }
            
            Debug.LogWarning("No Dropdown values were found.");
            return null;
        }

        private Array TryGetMethodValueArray(object[] parameters, MethodInfo methodInfo, FieldInfo propertyFieldInfo, object target)
        {
            object methodResult = methodInvoker.InvokeMethod(target, parameters, methodInfo);

            if (methodResult == null)
            {
                Debug.LogError("Method returns null.");
                return null;
            }

            if (!methodResult.GetType().IsArray) 
            {
                Debug.LogError("Parameter is not an array.");
                return null;
            }

            if (methodResult.GetType().GetElementType() != propertyFieldInfo.FieldType)
            {
                DebugIncompatibleTypes(methodResult.GetType().GetElementType(), propertyFieldInfo.FieldType);
                return null;
            }

            return methodResult as Array;
        }

        private Array TryGetPropertyValueArray(PropertyInfo propertyInfo, FieldInfo propertyFieldInfo, object target)
        {
            if (!propertyInfo.PropertyType.IsArray) 
            {
                Debug.LogError("Parameter is not an array.");
                return null;
            }

            if (propertyInfo.PropertyType.GetElementType() != propertyFieldInfo.FieldType)
            {
                DebugIncompatibleTypes(propertyInfo.PropertyType.GetElementType(), propertyFieldInfo.FieldType);
                return null;
            }

            Array values = propertyInfo.GetValue(target) as Array;

            return values;
        }

        private Array TryGetFieldValueArray(FieldInfo fieldInfo, FieldInfo propertyFieldInfo, object target)
        {
            if (!fieldInfo.FieldType.IsArray) 
            {
                Debug.LogError("Parameter is not an array.");
                return null;
            }

            if (fieldInfo.FieldType.GetElementType() != propertyFieldInfo.FieldType)
            {
                DebugIncompatibleTypes(fieldInfo.FieldType.GetElementType(), propertyFieldInfo.FieldType);
                return null;
            }

            Array values = fieldInfo.GetValue(target) as Array;

            return values;
        }

        private void DebugIncompatibleTypes(Type elementType, Type propertyType)
        {
            Debug.LogError($"Parameter element type '{elementType}' and property type '{propertyType}' are not equal.");
        }
    }
}