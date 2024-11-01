using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorEnhancements
{
    public class BaseFieldDrawer : IFieldDrawer
    {
        private readonly IMemberInfoProvider memberInfoProvider = new CacheMemberInfoProvider();

        public virtual void DrawField(string fieldName, ref object fieldValue, Type type, bool isEditable)
        {
            EvaluateType(fieldName, ref fieldValue, type, isEditable);
        }

        protected virtual void EvaluateType(string label, ref object value, Type type, bool isEditable)
        {
            if (value == null)
            {
                EditorGUILayout.LabelField(label, "null");
                return;
            }

            if (type.IsPrimitive || type == typeof(string))
            {
                DrawPrimitiveField(label, ref value, isEditable);
                return;
            }

            if (typeof(UnityEngine.Object).IsAssignableFrom(type))
            {
                DrawUnityNativeField(label, ref value, type);
                return;
            }

            if (IsUnityStruct(type) && DrawUnityStructField(label, ref value, type, isEditable))
            {
                return;
            }

            if (type.IsClass || type.IsValueType)
            {
                DrawComplexField(label, ref value, type, isEditable);
            }
        }

        private bool IsUnityStruct(Type type)
        {
            return type.Namespace == "UnityEngine" && type.IsValueType;
        }

        protected virtual bool DrawUnityStructField(string label, ref object value, Type type, bool isEditable)
        {
            // Attempt to find the exact method with the necessary parameters
            MethodInfo method = GetEditorGUILayoutMethod(type);

            if (method != null)
            {
                EditorGUI.BeginDisabledGroup(!isEditable);
                value = method.Invoke(null, new object[] { label, value, null });
                EditorGUI.EndDisabledGroup();
                return true;
            }
            
            return false;
        }

        protected virtual MethodInfo GetEditorGUILayoutMethod(Type fieldType)
        {
            string methodName = $"{fieldType.Name}Field";

            // TODO: Use memberInfoProvider to safely get methods from cache
            var methods = typeof(EditorGUILayout).GetMethods(BindingFlags.Public | BindingFlags.Static);

            // Find the first method with parameters matching: (string, fieldType, GUILayoutOption[])
            return methods.FirstOrDefault(method =>
            {
                var parameters = method.GetParameters();
                return method.Name == methodName &&
                    parameters.Length == 3 &&
                    parameters[0].ParameterType == typeof(string) &&
                    parameters[1].ParameterType == fieldType &&
                    parameters[2].ParameterType == typeof(GUILayoutOption[]);
            });
        }

        protected virtual void DrawUnityNativeField(string label, ref object value, Type type)
        {
            if (value is UnityEngine.Object unityObject)
            {
                value = EditorGUILayout.ObjectField(label, unityObject, type, true);
            }
            else
            {
                EditorGUILayout.LabelField(label, $"Invalid UnityEngine.Object: {value?.GetType().Name ?? "null"}");
            }
        }

        protected virtual void DrawPrimitiveField(string label, ref object value, bool isEditable)
        {
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
                    Debug.LogWarning($"Unsupported primitive type: {value.GetType().Name}");
                    EditorGUILayout.LabelField(label, value.ToString());
                    break;
            }

            EditorGUI.EndDisabledGroup();
        }

        protected virtual void DrawComplexField(string label, ref object value, Type type, bool isEditable)
        {
            EditorGUILayout.LabelField(label, type.Name);
            EditorGUI.indentLevel++;

            if (memberInfoProvider == null)
            {
                Debug.LogWarning("MemberInfoProvider is not set.");
                return;
            }

            List<FieldInfo> fieldInfos = memberInfoProvider.TryGetAllMemberInfo<FieldInfo>(value);
            foreach (var field in fieldInfos)
            {
                if (!field.IsPublic || field.IsNotSerialized)
                    continue;

                object fieldValue = field.GetValue(value);
                EvaluateType(field.Name, ref fieldValue, field.FieldType, !field.IsInitOnly);

                field.SetValue(value, fieldValue);
            }

            EditorGUI.indentLevel--;
        }
    }
}
