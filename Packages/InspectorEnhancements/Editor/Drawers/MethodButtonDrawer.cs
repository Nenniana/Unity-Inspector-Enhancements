using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorEnhancements
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class MethodButtonDrawer : Editor
    {
        private readonly IDefaultValueProvider defaultValueProvider = new DefaultValueProvider();
        private readonly IParameterProvider parameterProvider = new OverwriteableParameterProvider();
        private readonly IMemberInfoProvider memberInfoProvider = new CacheMemberInfoProvider();
        private IFieldDrawer fieldDrawer;

        private void OnEnable() {
            fieldDrawer = FieldDrawerFactory.CreateDefaultFieldDrawer(memberInfoProvider, new EditorGUILayoutMethodProvider());
        }

        public override void OnInspectorGUI()
        {
            // Draw the default inspector first
            DrawDefaultInspector();

            DrawMethod();
        }

        private void DrawMethod()
        {
            // Retrieve target object and all methods with MethodButtonAttribute
            var methods = memberInfoProvider.TryGetAllMemberInfo<MethodInfo>(target.GetType());

            foreach (var method in methods)
            {
                var attribute = (MethodButtonAttribute)method.GetCustomAttribute(typeof(MethodButtonAttribute), true);

                if (attribute == null)
                {
                    continue;
                }

                DrawMethodGUI(method, target, attribute);
            }
        }

        private void DrawMethodGUI(MethodInfo method, object targetObject, MethodButtonAttribute attribute)
        {
            bool parametersFoldedOut = OverwriteableStringCache<bool>.GetOrAdd(target.GetType(), method.Name, () => attribute.ExpandParameters);
            bool hasParameters = AnyParameters(method);
            
            if (hasParameters) GUILayout.BeginVertical("box");

            GUILayout.BeginHorizontal();

            // Draw the button and handle the click event
            if (GUILayout.Button(method.Name, GUILayout.ExpandWidth(true)))
            {
                var parameterValues = DrawMethodParameterFields(method);
                method.Invoke(targetObject, parameterValues);
            }

            // Toggle foldout for parameters if there are any
            if (hasParameters)
            {
                DrawParameterFoldout(method, parametersFoldedOut);
            }

            GUILayout.EndHorizontal();

            // Show parameters if foldout is open
            if (parametersFoldedOut && hasParameters)
            {
                GUILayout.Space(2);
                DrawMethodParameterFields(method);
            }

            if (hasParameters) GUILayout.EndVertical();
        }

        private void DrawParameterFoldout(MethodInfo method, bool parametersFoldedOut)
        {
            GUILayout.Space(10); 
            GUILayout.BeginHorizontal(GUILayout.Width(60));

            OverwriteableStringCache<bool>.OverwriteOrAdd(target.GetType(), method.Name, GUILayout.Toggle(
                parametersFoldedOut,
                "Show",
                EditorStyles.foldout
            ));

            GUILayout.EndHorizontal();
        }

        private static bool AnyParameters(MethodInfo method)
        {
            return method.GetParameters().Length > 0;
        }

        public object[] DrawMethodParameterFields(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            object[] parameterValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                object value;

                if (parameters[i].HasDefaultValue)
                {
                    value = parameters[i].DefaultValue;
                }
                else
                {
                    value = parameterProvider.GetOrAdd(method.Name, parameters[i], 
                        () => defaultValueProvider.GetDefaultValue(parameters[i].ParameterType));
                }

                fieldDrawer.DrawField(parameters[i].Name, ref value, parameters[i].ParameterType, true);

                parameterValues[i] = parameterProvider.OverwriteOrAdd(method.Name, parameters[i], value);
            }

            return parameterValues;
        }
    }
}