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
            // Get the target object (the MonoBehaviour instance) as the current script
            var targetObject = target;

            // Retrieve all methods in the target object’s class
            var methods = memberInfoProvider.TryGetAllMemberInfo<MethodInfo>(target.GetType());

            foreach (var method in methods)
            {
                // Check if the method has the MethodButtonAttribute
                var attribute = (MethodButtonAttribute)method.GetCustomAttribute(typeof(MethodButtonAttribute), true);

                if (attribute == null)
                {
                    continue;
                }

                if (DrawMethodButton(method))
                {
                    // If button is clicked, invoke the method with parameters
                    object[] parameterValues = DrawMethodParameterFields(method);
                    method.Invoke(targetObject, parameterValues); // Pass 'null' if method has no parameters
                }
                else
                {
                    // If the button is not clicked, just draw parameter fields
                    DrawMethodParameterFields(method);
                }
            }
        }

        private bool DrawMethodButton(MethodInfo method)
        {
            // Return true if the button was clicked
            return GUILayout.Button(method.Name);
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