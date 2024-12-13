using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using Nenn.InspectorEnhancements.Runtime.Attributes;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberInfoProvider;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMemberInfoProvider.Base;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMethodInvoker;
using Nenn.InspectorEnhancements.Runtime.Helpers.Interfaces.IMethodInvoker.Base;
using UnityEditor;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(CollectionDropdownAttribute))]
    public class ArrayDropdownDrawer : PropertyDrawer
    {
        private IMemberInfoProvider memberInfoProvider = new CacheMemberInfoProvider();
        private IMethodResolver methodResolver = new DefaultMethodResolver();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            object target = property.serializedObject.targetObject;
            FieldInfo propertyFieldInfo = memberInfoProvider.TryGetMemberInfo<FieldInfo>(target.GetType(), property.name);

            if (propertyFieldInfo == null)
            {
                Debug.LogWarning("No Property FieldInfo is found.");
                return;
            }

            CollectionDropdownAttribute dropdownAttribute = (CollectionDropdownAttribute)attribute;

            if (string.IsNullOrEmpty(dropdownAttribute.MemberName))
            {
                Debug.LogWarning("Parameter is either null or empty.");
                return;
            }

            IEnumerable values = GetDropdownValues(dropdownAttribute, target, propertyFieldInfo);

            CreateDropdown(position, property, label, values, propertyFieldInfo, target);

            property.serializedObject.ApplyModifiedProperties();
        }

        private void CreateDropdown(Rect position, SerializedProperty property, GUIContent label, IEnumerable options, FieldInfo propertyFieldInfo, object target)
        {
            if (options != null)
            {
                var optionsArray = options.Cast<object>().ToArray();

                if (optionsArray.Length > 0)
                {
                    string[] optionStrings = optionsArray.Select(option => option?.ToString() ?? "null").ToArray();

                    int currentIndex = Array.IndexOf(optionsArray, propertyFieldInfo.GetValue(target));
                    if (currentIndex == -1) currentIndex = 0;

                    int selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, optionStrings);

                    object selectedOption = optionsArray[selectedIndex];
                    propertyFieldInfo.SetValue(target, selectedOption);
                    property.serializedObject.ApplyModifiedProperties();
                }
                else
                {
                    EditorGUI.LabelField(position, label.text, "List is null or empty.");
                }
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "List is null or empty.");
            }
        }

        private IEnumerable GetDropdownValues(CollectionDropdownAttribute dropdownAttribute, object target, FieldInfo propertyFieldInfo) 
        {
            FieldInfo fieldInfo = memberInfoProvider.TryGetMemberInfo<FieldInfo>(target.GetType(), dropdownAttribute.MemberName);
            if (fieldInfo != null)
            {
                return TryGetFieldValueList(fieldInfo, propertyFieldInfo, target);
            }

            PropertyInfo propertyInfo = memberInfoProvider.TryGetMemberInfo<PropertyInfo>(target.GetType(), dropdownAttribute.MemberName);
            if (propertyInfo != null)
            {
                return TryGetPropertyValueList(propertyInfo, propertyFieldInfo, target);
            }

            MethodInfo methodInfo = memberInfoProvider.TryGetMemberInfo<MethodInfo>(target.GetType(), dropdownAttribute.MemberName);
            if (methodInfo != null)
            {
                return TryGetMethodValueList(dropdownAttribute.Parameters, methodInfo, propertyFieldInfo, target);
            }
            
            Debug.LogWarning("No Dropdown values were found.");
            return null;
        }

        private IEnumerable TryGetMethodValueList(object[] parameters, MethodInfo methodInfo, FieldInfo propertyFieldInfo, object target)
        {
            object[] methodParameters = methodResolver.InvokeMethod(target, parameters, methodInfo);
            object methodResult = methodInfo.Invoke(target, methodParameters);

            if (methodResult == null)
            {
                Debug.LogError("Method returns null.");
                return null;
            }

            if (methodResult is IEnumerable resultEnumerable)
            {
                return VerifyCollectionType(resultEnumerable, propertyFieldInfo.FieldType);
            }

            Debug.LogError("Parameter is not a collection.");
            return null;
        }

        private IEnumerable TryGetPropertyValueList(PropertyInfo propertyInfo, FieldInfo propertyFieldInfo, object target)
        {
            object propertyValue = propertyInfo.GetValue(target);

            if (propertyValue is IEnumerable resultEnumerable)
            {
                return VerifyCollectionType(resultEnumerable, propertyFieldInfo.FieldType);
            }

            Debug.LogError("Parameter is not a collection.");
            return null;
        }

        private IEnumerable TryGetFieldValueList(FieldInfo fieldInfo, FieldInfo propertyFieldInfo, object target)
        {
            object fieldValue = fieldInfo.GetValue(target);

            if (fieldValue is IEnumerable resultEnumerable)
            {
                return VerifyCollectionType(resultEnumerable, propertyFieldInfo.FieldType);
            }

            Debug.LogError("Parameter is not a collection.");
            return null;
        }

        private IEnumerable VerifyCollectionType(IEnumerable collection, Type targetType)
        {
            Type elementType = collection.GetType().IsArray
                ? collection.GetType().GetElementType()
                : collection.GetType().GetGenericArguments().FirstOrDefault();

            if (elementType != targetType)
            {
                DebugIncompatibleTypes(elementType, targetType);
                return null;
            }

            return collection;
        }

        private void DebugIncompatibleTypes(Type elementType, Type propertyType)
        {
            Debug.LogError($"Parameter element type '{elementType}' and property type '{propertyType}' are not equal.");
        }
    }
}
