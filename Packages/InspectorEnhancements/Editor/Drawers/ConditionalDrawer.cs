using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;

namespace InspectorEnhancements
{
    [CustomPropertyDrawer(typeof(ConditionalAttribute), true)] 
    public class ConditionalDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string controlName = property.propertyPath;
            var attribute = this.attribute as ConditionalAttribute;
            string conditionName = attribute?.Condition;

            GUI.SetNextControlName(controlName);

            EditorGUI.BeginChangeCheck();

            if (IsInvalidCustomClassOrStruct(property, conditionName))
            {
                DrawWarningForStructClass(position, property, label);
            }

            else if (EvaluateCondition(property, attribute, conditionName))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }

            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }

            if (GUI.GetNameOfFocusedControl() == controlName)
            {
                EditorGUI.FocusTextInControl(controlName);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var attribute = this.attribute as ConditionalAttribute;
            string conditionName = attribute?.Condition;

            if (IsInvalidCustomClassOrStruct(property, conditionName))
            {
                return CalculateHelpBoxHeight(property);
            }

            if (EvaluateCondition(property, attribute, conditionName)) 
            {
                return EditorGUI.GetPropertyHeight(property, true);
            }
           
            // Otherwise return normal height.
            return 0f;
        }

        private bool EvaluateCondition(SerializedProperty property, ConditionalAttribute attribute, string conditionName)
        {
            bool shouldShow = true;
            bool invertCondition = attribute is HideIfAttribute;
            object target = property.serializedObject.targetObject;

            if (string.IsNullOrEmpty(conditionName) && TryEvaluateField(target, property.name, ref shouldShow))
            {
                return InvertCondition(invertCondition, shouldShow);
            }
            
            return InvertCondition(invertCondition, FindMemberAndEvaluate(attribute, property, target, conditionName));
        }

        private bool IsInvalidCustomClassOrStruct(SerializedProperty property, string conditionName)
        {
            return string.IsNullOrEmpty(conditionName) 
                && IsTypeCustomClassOrStruct(property.GetType()) 
                || IsCustomClassOrStructField(property.serializedObject.targetObject, conditionName);
        }

        private bool IsCustomClassOrStructField(object target, string conditionName)
        {
            if (string.IsNullOrEmpty(conditionName)) return false;

            FieldInfo fieldInfo = CacheHelper<FieldInfo>.GetOrAdd(
                target, conditionName,
                () => ReflectionHelper.FindField(target, conditionName)
            );

            PropertyInfo propertyInfo = CacheHelper<PropertyInfo>.GetOrAdd(
                target, conditionName,
                () => ReflectionHelper.FindProperty(target, conditionName)
            );

            Type fieldType = null;

            if (fieldInfo != null)
            {
                fieldType = fieldInfo.FieldType;
            }
            else if (propertyInfo != null)
            {
                fieldType = propertyInfo.PropertyType;
            }

            return IsTypeCustomClassOrStruct(fieldType);
        }

        private static bool IsTypeCustomClassOrStruct(Type fieldType)
        {
            if (fieldType != null)
            {
                // Disregard if Unity native object or SerializedProperty
                if (typeof(UnityEngine.Object).IsAssignableFrom(fieldType) || fieldType == typeof(SerializedProperty))
                    return false;

                // Check if it's a class or struct, and not a primitive type
                return fieldType.IsClass || (fieldType.IsValueType && !fieldType.IsPrimitive);
            }

            return false;
        }

        private bool FindMemberAndEvaluate(ConditionalAttribute attribute, SerializedProperty property, object target, string conditionName)
        {
            bool shouldShow = true;

            if (TryEvaluateMethod(attribute, property, target, conditionName, ref shouldShow))
                return shouldShow;

            if (TryEvaluateField(target, conditionName, ref shouldShow))
                return shouldShow;

            if (TryEvaluateProperty(target, conditionName, ref shouldShow))
                return shouldShow;

            // Log a warning if the condition is not found
            Debug.LogWarning($"Condition '{conditionName}' not found in {target.GetType()}");
            return shouldShow;
        }

        private bool InvertCondition (bool invertCondition, bool result) {
            return invertCondition ? !result : result;
        }

        private bool TryEvaluateMethod(ConditionalAttribute attribute, SerializedProperty property, object target, string conditionName, ref bool shouldShow)
        {
            MethodInfo methodInfo = CacheHelper<MethodInfo>.GetOrAdd(
                target, conditionName,
                () => ReflectionHelper.FindMethod(target, conditionName)
            );

            if (methodInfo == null)
                return false;

            var passedParams = attribute?.Parameters ?? new object[0];
            bool result = InvokeMethod(target, methodInfo, passedParams, property);
            shouldShow = result;
            return true;
        }

        private bool TryEvaluateProperty(object target, string conditionName, ref bool shouldShow)
        {
            PropertyInfo propertyInfo = CacheHelper<PropertyInfo>.GetOrAdd(
                target, conditionName,
                () => ReflectionHelper.FindProperty(target, conditionName)
            );

            if (propertyInfo == null)
                return false;

            bool result = IsPropertyBoolean(target, propertyInfo);
            shouldShow = result;
            return true;
        }

        private bool TryEvaluateField(object target, string conditionName, ref bool shouldShow)
        {
            Debug.Log($"Looking for name: {conditionName}");
            FieldInfo fieldInfo = CacheHelper<FieldInfo>.GetOrAdd(
                target, conditionName,
                () => ReflectionHelper.FindField(target, conditionName)
            );

            if (fieldInfo == null)
                return false;

            Debug.Log($"{conditionName} was found.");

            bool result = IsFieldBoolean(target, fieldInfo);
            shouldShow = result;
            return true;
        }

        // Check if a field is boolean or non-null
        public bool IsFieldBoolean(object target, FieldInfo fieldInfo)
        {
            var fieldValue = fieldInfo.GetValue(target);
            return fieldInfo.FieldType == typeof(bool) ? (bool)fieldValue : fieldValue != null;
        }

        // Check if a property is boolean or non-null
        public bool IsPropertyBoolean(object target, PropertyInfo propertyInfo)
        {
            var propertyValue = propertyInfo.GetValue(target);
            return propertyInfo.PropertyType == typeof(bool) ? (bool)propertyValue : propertyValue != null;
        }

        private bool InvokeMethod(object target, MethodInfo methodInfo, object[] passedParams, SerializedProperty property)
        {
            try
            {
                var parameters = methodInfo.GetParameters();
                object[] parameterValues = new object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    ParameterInfo parameter = parameters[i];

                    if (i < passedParams.Length)
                    {
                        // Handle the passed parameter if it's a field name
                        if (passedParams[i] is string fieldName)
                        {
                            FieldInfo fieldInfo = CacheHelper<FieldInfo>.GetOrAdd(
                                target, fieldName,
                                () => ReflectionHelper.FindField(target, fieldName)
                            );

                            if (fieldInfo == null)
                            {
                                Debug.LogWarning($"Field '{fieldName}' not found in {target.GetType()}");
                                return true; // Default to showing the property on error
                            }

                            parameterValues[i] = fieldInfo.GetValue(target);
                        }
                        else
                        {
                            parameterValues[i] = passedParams[i];
                        }
                    }
                    else if (parameter.HasDefaultValue)
                    {
                        parameterValues[i] = parameter.DefaultValue;
                    }
                    else
                    {
                        Debug.LogWarning($"Method {methodInfo.Name} parameter {parameter.Name} is missing and has no default value.");
                        return true; // Default to showing the property if missing parameters
                    }
                }

                return (bool)methodInfo.Invoke(target, parameterValues);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error invoking method '{methodInfo.Name}' on {target.GetType()}: {ex.Message}");
                return true; // Default to showing the property on error
            }
        }

        private void DrawWarningForStructClass(Rect position, SerializedProperty property, GUIContent label)
        {
            string warningString = CreateStructClassWarningString();

            float helpBoxHeight = EditorStyles.helpBox.CalcHeight(new GUIContent(warningString), EditorGUIUtility.currentViewWidth);

            Rect helpBoxRect = new Rect(position.x, position.y, position.width, helpBoxHeight);
            EditorGUI.HelpBox(helpBoxRect, warningString, MessageType.Warning);

            Rect propertyRect = new Rect(position.x, position.y + helpBoxHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUI.GetPropertyHeight(property, true));
            EditorGUI.PropertyField(propertyRect, property, label, true);
        }

        private string CreateStructClassWarningString()
        {
            string warningString = " is not compatible with custom class or struct null checks. Use a separate bool condition or method instead.";
            if (attribute is HideIfAttribute)
                warningString = "[HideIf]" + warningString;
            else if (attribute is ShowIfAttribute)
                warningString = "[ShowIf]" + warningString;
            else
                warningString = "This system" + warningString;
            return warningString;
        }

        private float CalculateHelpBoxHeight(SerializedProperty property)
        {
            string helpBoxText = CreateStructClassWarningString();
            float helpBoxHeight = EditorStyles.helpBox.CalcHeight(new GUIContent(helpBoxText), EditorGUIUtility.currentViewWidth);
            return helpBoxHeight + EditorGUI.GetPropertyHeight(property, true);
        }
    }
}
