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
        private Dictionary<MethodInfo, bool> methodFoldoutStates = new Dictionary<MethodInfo, bool>();
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
                if (!(method.GetCustomAttribute(typeof(MethodButtonAttribute), true) is MethodButtonAttribute attribute))
                {
                    continue;
                }

                // Initialize foldout state for the method if it doesn't exist
                if (!methodFoldoutStates.ContainsKey(method))
                {
                    methodFoldoutStates[method] = true;
                }

                bool hasParameters = AnyParameters(method);
                DrawMethodGUI(method, hasParameters, target);
            }
        }

        private void DrawMethodGUI(MethodInfo method, bool hasParameters, object targetObject)
        {
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
                DrawParameterFoldout(method);
            }

            GUILayout.EndHorizontal();

            // Show parameters if foldout is open
            if (methodFoldoutStates[method] && hasParameters)
            {
                DrawMethodParameterFields(method);
            }

            if (hasParameters) GUILayout.EndVertical();
        }

        private void DrawParameterFoldout(MethodInfo method)
        {
            GUILayout.Space(10);
            GUILayout.BeginHorizontal(GUILayout.Width(60));

            methodFoldoutStates[method] = GUILayout.Toggle(
                methodFoldoutStates[method],
                "Show",
                EditorStyles.foldout
            );

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