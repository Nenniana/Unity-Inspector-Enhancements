using UnityEngine;
using UnityEditor;

namespace InspectorEnhancements
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (IsUnassigned(property))
            {
                RequiredAttribute inlineAttribute = (RequiredAttribute)attribute;
                // Calculate height for the error message from the attribute
                float errorHeight = EditorStyles.helpBox.CalcHeight(new GUIContent(inlineAttribute.ErrorMessage), position.width);
                // Draw the error message
                Rect errorPosition = new Rect(position.x, position.y, position.width, errorHeight);
                EditorGUI.HelpBox(errorPosition, inlineAttribute.ErrorMessage, MessageType.Error);
    
                // Adjust the position for the next element
                position.y += errorHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            EditorGUI.PropertyField(position, property, label, true);
        }
    
        private static bool IsUnassigned(SerializedProperty property)
        {
            return  (property.propertyType == SerializedPropertyType.ManagedReference && property.managedReferenceValue == null) ||
                    (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null);
        }
    
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float propertyHeight = EditorGUI.GetPropertyHeight(property, true);
    
            if (!IsUnassigned(property))
            {
                return propertyHeight;
            }
    
            // Calculate and return the height for the error message, including spacing
            RequiredAttribute inlineAttribute = (RequiredAttribute)attribute;
            float errorHeight = EditorStyles.helpBox.CalcHeight(new GUIContent(inlineAttribute.ErrorMessage), EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth);

            return errorHeight + EditorGUIUtility.standardVerticalSpacing + propertyHeight;
        }
    }
}