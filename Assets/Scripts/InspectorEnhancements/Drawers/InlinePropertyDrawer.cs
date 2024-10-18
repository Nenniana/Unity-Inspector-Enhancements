using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;

namespace InspectorEnhancements
{
    [CustomPropertyDrawer(typeof(InlinePropertyAttribute), true)]
    public class InlinePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Determine which attribute to use (prioritize defined field-level attributes, otherwise fallback to type-level)
            InlinePropertyAttribute effectiveAttribute = GetEffectiveAttribute();

            // If no effective attribute is found, fallback to default Unity property drawing 
            if (effectiveAttribute == null)
            {
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }

            if (effectiveAttribute.DisplayMode == InlinePropertyNameMode.HeaderName) 
            {
                position = DrawHeader(position, property, effectiveAttribute);
            }

            DrawInlineProperties(position, property, label, effectiveAttribute);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Determine which attribute to use (prioritize defined field-level attributes, otherwise fallback to type-level)
            InlinePropertyAttribute effectiveAttribute = GetEffectiveAttribute();

            float headerHeight = 0f;

            if (effectiveAttribute != null)
            {
                if (effectiveAttribute.DisplayMode == InlinePropertyNameMode.HeaderName)
                {
                    headerHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                }
                
                return headerHeight + CalculateInlinePropertyHeight(property);
            }

            // Fallback to default height calculation
            return headerHeight + EditorGUI.GetPropertyHeight(property, label, true);
        }

        private InlinePropertyAttribute GetEffectiveAttribute()
        {
            InlinePropertyAttribute fieldAttribute = attribute as InlinePropertyAttribute;

            Type fieldType = fieldInfo.FieldType;

            // Check if the field's type is decorated with InlinePropertyAttribute
            InlinePropertyAttribute typeAttribute = fieldType.GetCustomAttribute<InlinePropertyAttribute>();

            if (typeAttribute == null || !fieldAttribute.IsDefault)
                return fieldAttribute;

            return typeAttribute;
        }

        private Rect DrawHeader(Rect position, SerializedProperty property, InlinePropertyAttribute effectiveAttribute)
        {
            string header = GetLabelName(effectiveAttribute.CustomName, property.displayName);

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(position, header, EditorStyles.boldLabel);

            position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
            return position;
        }

        private string GetLabelName(string priorityLabel, string defaultLabel) {
            if (!string.IsNullOrEmpty(priorityLabel))
            {
                return priorityLabel;
            }

            return defaultLabel;
        }

        private void DrawInlineProperties(Rect position, SerializedProperty property, GUIContent label, InlinePropertyAttribute effectiveAttribute)
        {
            EditorGUI.BeginProperty(position, label, property);
            SerializedProperty iterator = property.Copy();
            string displayName = GetLabelName(effectiveAttribute.CustomName, label.text);

            DrawChildrenInline(position, effectiveAttribute, iterator, displayName);

            EditorGUI.EndProperty();
        }

        private void DrawChildrenInline(Rect position, InlinePropertyAttribute effectiveAttribute, SerializedProperty iterator, string displayName)
        {
            var endProperty = iterator.GetEndProperty();
            bool enterChildren = true;
            string fullName;

            while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, endProperty))
            {
                if (effectiveAttribute.DisplayMode == InlinePropertyNameMode.PrependName)
                {
                    fullName = $"{displayName} - {iterator.displayName}";
                } 
                else 
                {
                    fullName = iterator.displayName;
                }

                // Only enter children for the first-level properties
                enterChildren = false;

                position.height = EditorGUI.GetPropertyHeight(iterator, true);
                EditorGUI.PropertyField(position, iterator, new GUIContent(fullName), true);
                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
            }
        }

        private float CalculateInlinePropertyHeight(SerializedProperty property)
        {
            float totalHeight = 0f;
            SerializedProperty iterator = property.Copy();
            var endProperty = iterator.GetEndProperty();
            bool enterChildren = true;

            // Calculate height for each child property
            while (iterator.NextVisible(enterChildren) && !SerializedProperty.EqualContents(iterator, endProperty))
            {
                enterChildren = false;
                totalHeight += EditorGUI.GetPropertyHeight(iterator, true) + EditorGUIUtility.standardVerticalSpacing;
            }

            return totalHeight;
        }
    }
}
