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
                // Draw primitive value
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
    }
}