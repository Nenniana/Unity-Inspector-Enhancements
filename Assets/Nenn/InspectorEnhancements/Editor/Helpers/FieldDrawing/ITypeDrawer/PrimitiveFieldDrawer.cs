using System;
using UnityEditor;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.ITypeDrawer
{
    public class PrimitiveFieldDrawer : Base.ITypeDrawer
    {
        public bool Draw(string label, ref object value, Type type, bool isEditable)
        {
            // Ensure the type is either a supported primitive or string
            if (!type.IsPrimitive && type != typeof(string))
                return false;

            EditorGUI.BeginDisabledGroup(!isEditable);

            try
            {
                if (type == typeof(int))
                {
                    // Convert value to int if possible, otherwise use 0 as a default
                    value = EditorGUILayout.IntField(label, (int)Convert.ChangeType(value, typeof(int)));
                }
                else if (type == typeof(float))
                {
                    // Convert value to float if possible, otherwise use 0f as a default
                    value = EditorGUILayout.FloatField(label, (float)Convert.ChangeType(value, typeof(float)));
                }
                else if (type == typeof(bool))
                {
                    // Convert value to bool if possible, otherwise use false as a default
                    value = EditorGUILayout.Toggle(label, (bool)Convert.ChangeType(value, typeof(bool)));
                }
                else if (type == typeof(string))
                {
                    // Convert value to string if possible, otherwise use empty string as a default
                    value = EditorGUILayout.TextField(label, (string)Convert.ChangeType(value, typeof(string)));
                }
                else
                {
                    Debug.LogWarning($"Unsupported primitive type: {type.Name}");
                    EditorGUILayout.LabelField(label, value?.ToString() ?? "null");
                }
            }
            catch (InvalidCastException ex)
            {
                Debug.LogError($"Invalid cast for field '{label}' with type '{type.Name}': {ex.Message}");
            }

            EditorGUI.EndDisabledGroup();
            return true;
        }
    }
}
