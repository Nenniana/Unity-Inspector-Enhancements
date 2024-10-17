using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using System;
using System.Linq;

namespace InspectorEnhancements
{
    [CustomPropertyDrawer(typeof(ConditionalAttribute), true)]
    public class ConditionalDrawer : PropertyDrawer
    {
        // Cache for member lookups to avoid repetitive reflection calls
        private Dictionary<string, MemberInfo> cachedMembers = new Dictionary<string, MemberInfo>();
        private Dictionary<string, MethodInfo> cachedMethods = new Dictionary<string, MethodInfo>();
        private Dictionary<string, ParameterInfo[]> cachedMethodParameters = new Dictionary<string, ParameterInfo[]>();
        private Dictionary<string, FieldInfo> cachedFields = new Dictionary<string, FieldInfo>();
    
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string controlName = property.propertyPath;
            GUI.SetNextControlName(controlName);
    
            EditorGUI.BeginChangeCheck();
            if (IsCustomClassOrStruct(property))
            {
                DrawWarningForStructClass(position, property, label);
            }
            else if (ShouldShow(property))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
    
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }
    
            // Reapply focus if it was lost during a repaint
            if (GUI.GetNameOfFocusedControl() == controlName)
            {
                EditorGUI.FocusTextInControl(controlName);
            }
        }

        private void DrawWarningForStructClass(Rect position, SerializedProperty property, GUIContent label)
        {
            string warningString = CreateStructClassWarningString();

            float helpBoxHeight = EditorStyles.helpBox.CalcHeight(new GUIContent(warningString), EditorGUIUtility.currentViewWidth);

            // Draw the HelpBox at the current position
            Rect helpBoxRect = new Rect(position.x, position.y, position.width, helpBoxHeight);
            EditorGUI.HelpBox(helpBoxRect, warningString, MessageType.Warning);

            // Move the position down for the PropertyField
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

        private bool IsCustomClassOrStruct(SerializedProperty property)
        {
            return property.propertyType == SerializedPropertyType.Generic;
        }

    
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (IsCustomClassOrStruct(property))
            {
                string helpBoxText = CreateStructClassWarningString();
                float helpBoxHeight = EditorStyles.helpBox.CalcHeight(new GUIContent(helpBoxText), EditorGUIUtility.currentViewWidth);
                return helpBoxHeight + EditorGUI.GetPropertyHeight(property, true);
            }

            if (!ShouldShow(property))
                return 0f;

            return EditorGUI.GetPropertyHeight(property, true);
        }
    
        private bool ShouldShow(SerializedProperty property)
        {
            var attribute = this.attribute;
            bool invertCondition = attribute is HideIfAttribute;
    
            object target = property.serializedObject.targetObject;
            string conditionName = (attribute as ConditionalAttribute).Condition;
    
            if (string.IsNullOrEmpty(conditionName)) return true;
    
            MemberInfo memberInfo = GetOrCacheMember(target, conditionName);
    
            if (memberInfo == null) return true;
    
            bool result = EvaluateCondition(target, memberInfo, property);
    
            return invertCondition ? !result : result;
        }

        // Ensure unique cache key
        private string GetCacheKey(object target, string conditionName)
        {
            return $"{target.GetType().Name}.{conditionName}";
        }
    
        private MemberInfo GetOrCacheMember(object target, string conditionName)
        {
            var cacheKey = GetCacheKey(target, conditionName);
            if (!cachedMembers.TryGetValue(cacheKey, out var memberInfo))
            {
                memberInfo = FindMember(target, conditionName);
                cachedMembers[cacheKey] = memberInfo;
            }
            return memberInfo;
        }
    
        // Find a method, field, or property based on the condition name
        private MemberInfo FindMember(object target, string conditionName)
        {
            var type = target.GetType();
    
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                if (method.Name == conditionName && method.ReturnType == typeof(bool))
                {
                    return method;
                }
            }
    
            var field = type.GetField(conditionName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (field != null)
            {
                return field;
            }
    
            var property = type.GetProperty(conditionName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (property != null)
            {
                return property;
            }
    
            Debug.LogWarning($"Condition '{conditionName}' not found or invalid in {target.GetType()}");
            return null;
        }
    
    
        // Evaluate the condition based on the resolved member (method, field, or property)
        private bool EvaluateCondition(object target, MemberInfo memberInfo, SerializedProperty property)
        {
            try
            {
                switch (memberInfo)
                {
                    case MethodInfo methodInfo:
                        return InvokeMethod(target, methodInfo, property);
                    case FieldInfo fieldInfo:
                        return IsField(target, fieldInfo);
                    case PropertyInfo propertyInfo:
                        return IsProperty(target, propertyInfo);
                    default:
                        Debug.LogError($"Unsupported member type for {memberInfo.Name} in {target.GetType()}");
                        return true; // Default to show field on error
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error evaluating condition {memberInfo.Name} on {target.GetType()}: {ex.Message}");
                return true; // Default to show field on error
            }
        }

    
        private bool IsProperty(object target, PropertyInfo propertyInfo)
        {
            var propertyValue = propertyInfo.GetValue(target);
            if (propertyInfo.PropertyType == typeof(bool))
            {
                return (bool)propertyValue;
            }
            return propertyValue != null; // For object properties, true if not null, false if null
        }
    
        private bool IsField(object target, FieldInfo fieldInfo)
        {
            var fieldValue = fieldInfo.GetValue(target);
            if (fieldInfo.FieldType == typeof(bool))
            {
                return (bool)fieldValue;
            }
            return fieldValue != null; // For object fields, true if not null, false if null
        }

        private bool InvokeMethod(object target, MethodInfo methodInfo, SerializedProperty property)
        {
            string methodCacheKey = methodInfo.Name;
            MethodInfo cachedMethod = CacheMethodInfo(methodInfo, methodCacheKey);
            ParameterInfo[] parameters = CacheParameterInfo(methodCacheKey, cachedMethod);

            // If the method doesn't take any parameters, invoke directly
            if (parameters.Length == 0)
            {
                return (bool)cachedMethod.Invoke(target, null);
            }

            // If parameters are required, resolve them from the attribute or serialized object
            var passedParams = (attribute as ConditionalAttribute)?.Parameters ?? new object[0];

            // Prepare the final parameter values array
            object[] parameterValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameter = parameters[i];

                // If the passedParams array contains a value for this parameter, use it
                if (i < passedParams.Length)
                {
                    if (passedParams[i] is string fieldName)
                    {
                        // Check if the FieldInfo is already cached
                        if (!cachedFields.TryGetValue(fieldName, out FieldInfo fieldInfo))
                        {
                            CacheFieldInfo(target, fieldName, out fieldInfo);
                            if (IsFieldInfoNull(target, fieldName, fieldInfo))
                                return true;
                        }

                        // Use the cached FieldInfo to get the value
                        parameterValues[i] = fieldInfo.GetValue(target);
                    }
                    else
                    {
                        // Directly assign the literal value without type checking
                        parameterValues[i] = passedParams[i];
                    }
                }
                else
                {
                    // Use default value if no passed parameter
                    if (parameter.HasDefaultValue)
                    {
                        parameterValues[i] = parameter.DefaultValue;
                    }
                    else
                    {
                        Debug.LogWarning($"Method {methodInfo.Name} parameter {parameter.Name} is missing and has no default value.");
                        return true; // Default to showing the property if required parameters are missing
                    }
                }
            }

            // Invoke the method with the resolved parameters
            return (bool)cachedMethod.Invoke(target, parameterValues);
        }

        private void CacheFieldInfo(object target, string fieldName, out FieldInfo fieldInfo)
        {
            fieldInfo = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            cachedFields[fieldName] = fieldInfo; // Cache null if not found
        }

        private bool IsFieldInfoNull(object target, string fieldName, FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
            {
                Debug.LogWarning($"Field '{fieldName}' not found on {target.GetType()}.");
                return true; // Default to showing the property
            }

            return false;
        }

        private ParameterInfo[] CacheParameterInfo(string methodCacheKey, MethodInfo cachedMethod)
        {
            if (!cachedMethodParameters.TryGetValue(methodCacheKey, out ParameterInfo[] parameters))
            {
                parameters = cachedMethod.GetParameters();
                cachedMethodParameters[methodCacheKey] = parameters;
            }

            return parameters;
        }

        private MethodInfo CacheMethodInfo(MethodInfo methodInfo, string methodCacheKey)
        {
            if (!cachedMethods.TryGetValue(methodCacheKey, out MethodInfo cachedMethod))
            {
                cachedMethod = methodInfo;
                cachedMethods[methodCacheKey] = cachedMethod;
            }

            return cachedMethod;
        }
    }
}