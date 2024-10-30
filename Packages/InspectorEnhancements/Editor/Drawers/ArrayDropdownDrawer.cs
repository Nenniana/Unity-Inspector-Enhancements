using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorEnhancements
{
    public class ArrayDropdownDrawer : PropertyDrawer
    {
        private IMemberInfoProvider memberInfoProvider = new CacheMemberInfoProvider();
        
        private void CreateDropdown(Rect position, SerializedProperty property, GUIContent label, object[] options, FieldInfo propertyFieldInfo, object target)
        {
            if (options != null && options.Length > 0)
            {
                string[] optionStrings = options.Select(option => option?.ToString() ?? "null").ToArray();

                // Find the current index of the selected value in the list
                int currentIndex = Array.IndexOf(options, property.stringValue);
                if (currentIndex == -1) currentIndex = 0; // Default to the first item if not found

                // Create the dropdown
                int selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, optionStrings);

                // Update the property value with the selected key
                propertyFieldInfo.SetValue(target, options[selectedIndex]);
                property.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Array is null or empty.");
            }
        }

        private object[] GetDropdownValues(string parameterName, SerializedProperty property) 
        {
            object target = property.serializedObject.targetObject;
            FieldInfo propertyFieldInfo = memberInfoProvider.TryGetMemberInfo<FieldInfo>(target, property.name);

            if (propertyFieldInfo == null)
            {
                return null;
            }
            
            FieldInfo fieldInfo = memberInfoProvider.TryGetMemberInfo<FieldInfo>(target, parameterName);
            if (fieldInfo != null)
            {
                return TryGetFieldStringValues();
            }

            PropertyInfo propertyInfo = memberInfoProvider.TryGetMemberInfo<PropertyInfo>(target, parameterName);
            if (propertyInfo != null)
            {
                return TryGetPropertyStringValues();
            }

            MethodInfo methodInfo = memberInfoProvider.TryGetMemberInfo<MethodInfo>(target, parameterName);
            if (methodInfo != null)
            {
                return TryGetMethodStringValues();
            }

            return null;
        }

        private object[] TryGetMethodStringValues()
        {
            return null;
        }

        private object[] TryGetPropertyStringValues()
        {
            return null;
        }

        private object[] TryGetFieldStringValues()
        {
            return null;
        }
    }
}