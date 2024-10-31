using System;
using UnityEditor;

namespace InspectorEnhancements
{
    public abstract class BaseFieldDrawer : IFieldDrawer
    {
        public virtual void DrawField(string fieldName, object fieldValue, Type type, bool isEditable)
        {
            throw new NotImplementedException();
        }

        protected virtual void EvaluateType(string label, object value, Type type, bool isEditable)
        {
            if (value == null)
            {
                EditorGUILayout.LabelField(label, "null");
                return;
            }
            if (type.IsPrimitive || type == typeof(string))
            {
                DrawPrimitiveField(label, value, isEditable);
            }
            else if (typeof(UnityEngine.Object).IsAssignableFrom(type))
            {
                DrawUnityNativeType(label, value, type);
            }
            else if (type.IsClass || type.IsValueType)
            {
                // Draw complex value
            }
        }

        protected virtual void DrawUnityNativeType(string label, object value, Type type)
        {
            EditorGUILayout.ObjectField(label, (UnityEngine.Object)value, type, true);
        }

        protected virtual void DrawPrimitiveField(string label, object value, bool isEditable)
        {
            if (!isEditable)
            {
                EditorGUILayout.LabelField(label, value.ToString());
                return;
            }

            switch (value)
            {
                case int intValue:
                    EditorGUILayout.IntField(label, intValue);
                    break;
                case float floatValue:
                    EditorGUILayout.FloatField(label, floatValue);
                    break;
                case bool boolValue:
                    EditorGUILayout.Toggle(label, boolValue);
                    break;
                case string stringValue:
                    EditorGUILayout.TextField(label, stringValue);
                    break;
                default:
                    EditorGUILayout.LabelField(label, value.ToString());
                    break;
            }
        }
    }
}