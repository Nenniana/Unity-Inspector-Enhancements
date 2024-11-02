using System;
using UnityEditor;
using UnityEngine;

namespace InspectorEnhancements
{
    public class PrimitiveFieldDrawer : ITypeDrawer
    {
        public bool Draw(string label, ref object value, Type type, bool isEditable)
        {
            if (!type.IsPrimitive && type != typeof(string))
                return false;

            EditorGUI.BeginDisabledGroup(!isEditable);

            switch (value)
            {
                case int intValue:
                    value = EditorGUILayout.IntField(label, intValue);
                    break;
                case float floatValue:
                    value = EditorGUILayout.FloatField(label, floatValue);
                    break;
                case bool boolValue:
                    value = EditorGUILayout.Toggle(label, boolValue);
                    break;
                case string stringValue:
                    value = EditorGUILayout.TextField(label, stringValue);
                    break;
                default:
                    Debug.LogWarning($"Unsupported primitive type: {type.Name}");
                    EditorGUILayout.LabelField(label, value.ToString());
                    break;
            }

            EditorGUI.EndDisabledGroup();
            return true;
        }
    }
}