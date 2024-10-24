using UnityEngine;
using UnityEditor;

namespace InspectorEnhancements
{
    [CustomPropertyDrawer(typeof(HideLabelAttribute))]
    public class HideLabelDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Draw the property without the label
            EditorGUI.PropertyField(position, property, GUIContent.none, true);
        }
    
        // Ensure that Unity correctly calculates the height of the property
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }
    }
}