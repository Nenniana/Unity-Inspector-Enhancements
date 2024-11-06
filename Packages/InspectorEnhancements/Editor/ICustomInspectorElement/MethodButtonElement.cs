using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorEnhancements
{
    public class MethodButtonElement : ICustomInspectorElement
    {
        private readonly IDefaultValueProvider defaultValueProvider;
        private readonly IParameterProvider parameterProvider;
        private readonly IFieldDrawer fieldDrawer;

        public MethodButtonElement(
            IDefaultValueProvider defaultValueProvider,
            IParameterProvider parameterProvider,
            IFieldDrawer fieldDrawer)
        {
            this.defaultValueProvider = defaultValueProvider;
            this.parameterProvider = parameterProvider;
            this.fieldDrawer = fieldDrawer;
        }

        public bool IsApplicable(MemberInfo member)
        {
            return member is MethodInfo method && method.GetCustomAttribute(typeof(MethodButtonAttribute), true) != null;
        }

        public void DrawElement(MemberInfo member, object targetObject)
        {
            var method = member as MethodInfo;
            var attribute = (MethodButtonAttribute)method.GetCustomAttribute(typeof(MethodButtonAttribute), true);
            bool parametersFoldedOut = OverwriteableStringCache<bool>.GetOrAdd(targetObject.GetType(), method.Name, () => attribute.ExpandParameters);
            bool hasParameters = AnyParameters(method);

            if (hasParameters) GUILayout.BeginVertical("box");

            GUILayout.BeginHorizontal();

            // Draw the button and handle the click event
            if (GUILayout.Button(method.Name, GUILayout.ExpandWidth(true)))
            {
                var parameterValues = DrawMethodParameterFields(method, attribute, targetObject);
                method.Invoke(targetObject, parameterValues);
            }

            if (hasParameters)
            {
                DrawParameterFoldout(method, parametersFoldedOut, targetObject);
            }

            GUILayout.EndHorizontal();

            if (parametersFoldedOut && hasParameters)
            {
                DrawMethodParameterFields(method, attribute, targetObject);
            }

            if (hasParameters) GUILayout.EndVertical();
        }

        private void DrawParameterFoldout(MethodInfo method, bool parametersFoldedOut, object targetObject)
        {
            GUILayout.Space(10); 
            GUILayout.BeginHorizontal(GUILayout.Width(60));

            OverwriteableStringCache<bool>.OverwriteOrAdd(targetObject.GetType(), method.Name, GUILayout.Toggle(
                parametersFoldedOut,
                "Show",
                EditorStyles.foldout
            ));

            GUILayout.EndHorizontal();
        }

        private bool AnyParameters(MethodInfo method)
        {
            return method.GetParameters().Length > 0; 
        }

        private object[] DrawMethodParameterFields(MethodInfo method, MethodButtonAttribute attribute, object targetObject)
        {
            GUILayout.Space(2);
            ParameterInfo[] parameters = method.GetParameters();
            object[] parameterValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                Func<object> ValueDelegate = GetValueDelegate(attribute, targetObject, parameters, i);

                object value = parameterProvider.GetOrAdd(method.Name, parameters[i], ValueDelegate);

                fieldDrawer.DrawField(parameters[i].Name, ref value, parameters[i].ParameterType, true);

                parameterValues[i] = parameterProvider.OverwriteOrAdd(method.Name, parameters[i], value);
            }

            return parameterValues;
        }

        private Func<object> GetValueDelegate(MethodButtonAttribute attribute, object targetObject, ParameterInfo[] parameters, int i)
        {
            Func<object> ValueDelegate = () => defaultValueProvider.GetDefaultValue(parameters[i].ParameterType);

            if (parameters[i].HasDefaultValue)
            {
                ValueDelegate = () => parameters[i].DefaultValue;
            }

            if (attribute.Parameters != null)
            {
                if (parameters[i].ParameterType == attribute.Parameters[i].GetType())
                {
                    ValueDelegate = () => attribute.Parameters[i];
                }
                else if (attribute.Parameters[i].GetType() == typeof(string))
                {
                    FieldInfo fieldInfo = ReflectionHelper.FindMemberInfo<FieldInfo>(targetObject.GetType(), attribute.Parameters[i] as string);

                    if (fieldInfo != null)
                    {
                        ValueDelegate = () => fieldInfo.GetValue(targetObject);
                    }
                }
            }

            return ValueDelegate;
        }
    }
}
