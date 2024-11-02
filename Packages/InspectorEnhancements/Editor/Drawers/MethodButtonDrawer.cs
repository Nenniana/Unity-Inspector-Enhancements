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
        private readonly IMemberInfoProvider memberInfoProvider = new CacheMemberInfoProvider();
        private readonly IMethodResolver methodResolver = new DefaultMethodResolver();
        public override void OnInspectorGUI()
        {
            // Draw the default inspector first
            DrawDefaultInspector();

            // Get the target object (the MonoBehaviour instance) as the current script
            var targetObject = target;

            // Retrieve all methods in the target object’s class
            var methods = memberInfoProvider.TryGetAllMemberInfo<MethodInfo>(target.GetType());
            NewMethod(targetObject, methods);
        }

        private void NewMethod(Object targetObject, List<MethodInfo> methods)
        {
            foreach (var method in methods)
            {
                // Check if the method has the MethodButtonAttribute
                var attribute = (MethodButtonAttribute)method.GetCustomAttribute(typeof(MethodButtonAttribute), true);

                if (attribute == null)
                {
                    continue;
                }

                object[] parameters = method.GetParameters();

                if (attribute.Parameters != null && attribute.Parameters.Length > 0)
                {
                    parameters = attribute.Parameters;
                }

                DrawMethodButton(targetObject, method, parameters);
            }
        }

        private void DrawMethodButton(Object targetObject, MethodInfo method, object[] parameters)
        {
            object[] methodParameterValues = methodResolver.InvokeMethod(target, parameters, method);
            // Draw a button with the attribute’s specified button name
            if (GUILayout.Button(method.Name))
            {
                // Invoke the method when the button is clicked
                method.Invoke(targetObject, methodParameterValues); // Pass 'null' if method has no parameters
            }
        }
    }
}