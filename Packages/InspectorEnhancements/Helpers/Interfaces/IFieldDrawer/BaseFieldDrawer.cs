using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace InspectorEnhancements
{
    public abstract class BaseFieldDrawer : IFieldDrawer
    {
        private IMemberInfoProvider memberInfoProvider = new CacheMemberInfoProvider();
        public virtual void DrawField(string fieldName, object fieldValue, Type type, bool isEditable)
        {
            EvaluateType(fieldName, fieldValue, type, isEditable);
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
                DrawUnityNativeField(label, value, type);
            }
            else if (type.IsClass || type.IsValueType)
            {
                DrawComplexField(label, value, type, isEditable);
            }
        }

        protected virtual void DrawUnityNativeField(string label, object value, Type type)
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

        protected virtual void DrawComplexField(string label, object value, Type type, bool isEditable)
        {
            EditorGUILayout.LabelField(label, type.Name);
            EditorGUI.indentLevel++;

            List<FieldInfo> fieldInfos = memberInfoProvider.TryGetAllMemberInfo<FieldInfo>(value);

            foreach (var field in fieldInfos)
            {
                if (!field.IsPublic || field.IsNotSerialized)
                    continue;

                object fieldValue = field.GetValue(value);
                EvaluateType(field.Name, fieldValue, field.FieldType, !field.IsInitOnly);
            }

            EditorGUI.indentLevel--;
        }
    }
}