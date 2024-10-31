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
            var methods = targetObject.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                // Check if the method has the MethodButtonAttribute
                var attribute = (MethodButtonAttribute)method.GetCustomAttribute(typeof(MethodButtonAttribute), true);
                if (attribute != null)
                {
                    object[] methodParameterValues = methodResolver.InvokeMethod(target, attribute.Parameters, method);
                    // Draw a button with the attribute’s specified button name
                    if (GUILayout.Button(method.Name))
                    {
                        // Invoke the method when the button is clicked
                        method.Invoke(targetObject, methodParameterValues); // Pass 'null' if method has no parameters
                    }
                }
            }
        }
    }
}