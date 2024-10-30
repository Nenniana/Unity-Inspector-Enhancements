using System;
using UnityEditor;
using UnityEngine;

namespace InspectorEnhancements
{
    public class ArrayDropdownDrawer : PropertyDrawer
    {
        private void CreateDropdown(Rect position, SerializedProperty property, GUIContent label, string[] options)
        {
            if (options != null && options.Length > 0)
            {
                // Find the current index of the selected value in the list
                int currentIndex = Array.IndexOf(options, property.stringValue);
                if (currentIndex == -1) currentIndex = 0; // Default to the first item if not found

                // Create the dropdown
                int selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, options);

                // Update the property value with the selected key
                property.stringValue = options[selectedIndex];
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Array is null or empty.");
            }
        }
    }
}